/*//////////////////////////////////////////////////////////////////////////////////*/
/*                                                                                  */
/*  MIT License                                                                     */
/*                                                                                  */
/*  Copyright (c) 2020 David Korth <gerbilsoft@gerbilsoft.com>                      */
/*  Copyright (c) 2025 Hyper                                                        */
/*                                                                                  */
/*  Permission is hereby granted, free of charge, to any person obtaining a copy    */
/*  of this software and associated documentation files (the "Software"), to deal   */
/*  in the Software without restriction, including without limitation the rights    */
/*  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell       */
/*  copies of the Software, and to permit persons to whom the Software is           */
/*  furnished to do so, subject to the following conditions:                        */
/*                                                                                  */
/*  The above copyright notice and this permission notice shall be included in all  */
/*  copies or substantial portions of the Software.                                 */
/*                                                                                  */
/*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR      */
/*  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,        */
/*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE     */
/*  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER          */
/*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,   */
/*  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE   */
/*  SOFTWARE.                                                                       */
/*                                                                                  */
/*//////////////////////////////////////////////////////////////////////////////////*/

using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace Marathon.IO.Compression
{
    public class ZLibStream : Stream
    {
        protected Stream BaseStream = null;
        protected Stream DeflateStream = null;
        protected CompressionMode CompressionMode;
        protected bool LeaveOpen = true;
        protected bool IsDisposed = false;

        // Adler-32 checksum.
        protected const uint A32Mod = 65521;
        protected uint S1 = 1;
        protected uint S2 = 0;

        protected long BytesProcessed = 0;

        public override bool CanRead
        {
            get
            {
                if (BaseStream == null)
                    return false;

                return CompressionMode == CompressionMode.Decompress && BaseStream.CanRead;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (BaseStream == null)
                    return false;

                return CompressionMode == CompressionMode.Compress && BaseStream.CanWrite;
            }
        }

        public override bool CanSeek { get => false; }

        public override long Length
        {
            get => throw new NotSupportedException("ZLibStream does not support getting the stream length.");
        }

        public override long Position
        {
            get => throw new NotSupportedException("ZLibStream does not support getting the stream position.");
            set => throw new NotSupportedException("ZLibStream does not support setting the stream position.");
        }

        public ZLibStream(Stream in_stream, CompressionLevel in_compressionLevel, bool in_leaveOpen = false)
        {
            if (in_stream == null)
                throw new ArgumentNullException(nameof(in_stream));

            if (!in_stream.CanWrite)
                throw new ArgumentException("The specified stream is not writable.", nameof(in_stream));

            BaseStream = in_stream;
            CompressionMode = CompressionMode.Compress;
            LeaveOpen = in_leaveOpen;

            WriteZLibHeader(in_compressionLevel);

            DeflateStream = new DeflateStream(in_stream, in_compressionLevel, true);
        }

        public ZLibStream(Stream in_stream, CompressionMode in_compressionMode, bool in_leaveOpen = false)
        {
            if (in_stream == null)
                throw new ArgumentNullException(nameof(in_stream));

            if (in_compressionMode == CompressionMode.Compress && !in_stream.CanWrite)
            {
                throw new ArgumentException("The specified stream is not writable.", nameof(in_stream));
            }
            else if (in_compressionMode == CompressionMode.Decompress && !in_stream.CanRead)
            {
                throw new ArgumentException("The specified stream is not readable.", nameof(in_stream));
            }
            else if (in_compressionMode != CompressionMode.Compress && in_compressionMode != CompressionMode.Decompress)
            {
                throw new ArgumentException("Invalid compression mode.", nameof(in_compressionMode));
            }

            BaseStream = in_stream;
            CompressionMode = in_compressionMode;
            LeaveOpen = in_leaveOpen;

            if (in_compressionMode == CompressionMode.Compress)
            {
                WriteZLibHeader(CompressionLevel.Optimal);
            }
            else if (!VerifyZLibHeader(BaseStream))
            {
                throw new IOException("Invalid ZLib header.");
            }

            // TODO: Adler-32 checksum handling.
            DeflateStream = new DeflateStream(in_stream, in_compressionMode, true);
        }

        protected void WriteZLibHeader(CompressionLevel in_compressionLevel)
        {
            // NOTE: There doesn't appear to be a "maximum" compression option.
            //       This would be ZLib level 9 (0xDA).
            //       "Optimal" is level 6, ZLib default is level 5.
            //
            // References:
            // - https://stackoverflow.com/questions/9050260/what-does-a-zlib-header-look-like
            // - https://stackoverflow.com/a/17176881

            byte[] zlibHeader = [0x78, 0x00];

            zlibHeader[1] = in_compressionLevel switch
            {
                CompressionLevel.Optimal => 0x9C,
                _ => 0x01,
            };

            BaseStream.Write(zlibHeader, 0, zlibHeader.Length);
        }

        public static bool VerifyZLibHeader(Stream in_stream)
        {
            // Verify the ZLib header.
            //
            // Reference: https://tools.ietf.org/html/rfc1950
            //
            // Two bytes: CMF FLG
            //
            // CMF: Compression Method
            //      Bits 0-3 = CM: compression method (8 for Deflate)
            //      Bits 4-7 = CINFO: window size (7 for 2^(7+8) == 2^15 == 32 KB)
            //      ZLib is *always* 0x78. (Deflate, 32 KB)
            //
            // FLG: Flags
            //      Bits 0-4 = FCHECK: check bits for CMF and FLG
            //      Bit    5 = FDICT: preset dictionary
            //      Bits 6-7 = FLEVEL: compression level
            //
            // FCHECK must be set such that when CMF/FLG is viewed as a 16-bit BE unsigned int, CMFFLG % 31 == 0.

            var zlibHeader = new byte[2];

            in_stream.Read(zlibHeader, 0, zlibHeader.Length);

            // Check CMF - throw if not Deflate with 32 KB window.
            if (zlibHeader[0] != 0x78)
                return false;

            if (BitConverter.IsLittleEndian)
                Array.Reverse(zlibHeader);

            // Check FCHECK.
            if (BitConverter.ToUInt16(zlibHeader, 0) % 31 != 0)
                return false;

            return true;
        }

        protected void ProcessAdler32(byte[] in_buffer, int in_offset, int in_count)
        {
            var end = in_offset + in_count;

            for (int i = in_offset; i < end; i++)
            {
                S1 = (S1 + in_buffer[i]) % A32Mod;
                S2 = (S2 + S1) % A32Mod;
            }
        }

        public override int Read(byte[] in_buffer, int in_offset, int in_count)
        {
            EnsureDecompressionMode();
            ValidateParameters(in_buffer, in_offset, in_count);
            EnsureNotDisposed();

            // TODO: if we reach the end of the stream, verify the Adler-32 checksum.
            var bytesRead = DeflateStream.Read(in_buffer, in_offset, in_count);

            ProcessAdler32(in_buffer, in_offset, bytesRead);

            BytesProcessed += bytesRead;

            return bytesRead;
        }

        public override void Write(byte[] in_buffer, int in_offset, int in_count)
        {
            EnsureCompressionMode();
            ValidateParameters(in_buffer, in_offset, in_count);
            EnsureNotDisposed();
            ProcessAdler32(in_buffer, in_offset, in_count);

            DeflateStream.Write(in_buffer, in_offset, in_count);

            BytesProcessed += in_count;
        }

        private void ValidateParameters(byte[] in_array, int in_offset, int in_count)
        {
            if (in_array == null)
                throw new ArgumentNullException(nameof(in_array));

            if (in_offset < 0)
                throw new ArgumentOutOfRangeException(nameof(in_offset));

            if (in_count < 0)
                throw new ArgumentOutOfRangeException(nameof(in_count));

            if (in_array.Length - in_offset < in_count)
                throw new ArgumentException($"Invalid values for {nameof(in_offset)} and {nameof(in_count)}.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (BaseStream == null)
                throw new ObjectDisposedException(null, $"{nameof(BaseStream)} has been disposed.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureCompressionMode()
        {
            if (CompressionMode != CompressionMode.Compress)
                throw new InvalidOperationException("Cannot write to a stream opened for decompression.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureDecompressionMode()
        {
            if (CompressionMode != CompressionMode.Decompress)
                throw new InvalidOperationException("Cannot read from a stream opened for compression.");
        }

        public override void Flush()
        {
            EnsureNotDisposed();
        }

        public override long Seek(long in_offset, SeekOrigin in_origin)
        {
            throw new NotSupportedException("ZLibStream does not support seeking.");
        }

        public override void SetLength(long in_value)
        {
            throw new NotSupportedException("ZLibStream does not support setting the stream length.");
        }

        protected override void Dispose(bool in_isDisposing)
        {
            if (!IsDisposed && in_isDisposing)
            {
                DeflateStream?.Close();

                if (CompressionMode == CompressionMode.Compress)
                {
                    // Special case: zero-length file needs "\x03\x00"
                    // in order to not be misdetected as uncompressed.
                    if (BytesProcessed <= 0)
                    {
                        byte[] emptyMarker = [0x03, 0x00];
                        ProcessAdler32(emptyMarker, 0, emptyMarker.Length);
                        BaseStream.Write(emptyMarker, 0, emptyMarker.Length);
                    }

                    // Write the Adler-32 checksum.
                    var adler32 = unchecked((S2 << 16) | S1);
                    var adler32Bytes = BitConverter.GetBytes(adler32);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(adler32Bytes);

                    BaseStream.Write(adler32Bytes, 0, adler32Bytes.Length);
                    BaseStream.Flush();
                }

                if (!LeaveOpen)
                    BaseStream.Close();

                IsDisposed = true;
            }
        }
    }
}