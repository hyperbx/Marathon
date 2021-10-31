using Marathon.Exceptions;
using System;
using System.IO;
using System.IO.Compression;

namespace Marathon.IO
{
    public class ZlibStream : Stream
    {
        protected Stream Stream = null;            // Stream we're reading from or writing to.
        protected Stream DeflateStream = null;     // Internal DeflateStream for compression.
        protected CompressionMode CompressionMode; // Compression mode.
        protected bool LeaveOpen = true;           // If true, don't close _baseStream on Dispose.
        protected bool IsDisposed = false;

        // Adler-32 checksum stuff.
        protected const uint A32Mod = 65521;
        protected uint S1 = 1;
        protected uint S2 = 0;

        // Number of bytes processed.
        protected long BytesProcessed = 0;

        // Implies mode is Compress.
        public ZlibStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            // Can't write to this stream.
            if (!stream.CanWrite)
                throw new ArgumentException("Specified stream is not writable.", nameof(stream));

            Stream = stream;
            CompressionMode = CompressionMode.Compress;
            LeaveOpen = leaveOpen;

            WriteZlibHeader(compressionLevel);

            DeflateStream = new DeflateStream(stream, compressionLevel, true);
        }

        // Implies mode is Compress.
        public ZlibStream(Stream stream, CompressionLevel compressionLevel) : this(stream, compressionLevel, false) { }

        // Implies compressionLevel is Optimal.
        public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            // Can't write to this stream.
            if (mode == CompressionMode.Compress && !stream.CanWrite)
                throw new ArgumentException("Specified stream is not writable.", nameof(stream));

            // Can't read from this stream.
            else if (mode == CompressionMode.Decompress && !stream.CanRead)
                throw new ArgumentException("Specified stream is not writable.", nameof(stream));

            // Invalid CompressionMode.
            else if (mode != CompressionMode.Compress && mode != CompressionMode.Decompress)
                throw new ArgumentException("Invalid CompressionMode.", nameof(mode));

            Stream = stream;
            CompressionMode = mode;
            LeaveOpen = leaveOpen;

            if (mode == CompressionMode.Compress)
            {
                WriteZlibHeader(CompressionLevel.Optimal);
            }
            else
            {
                if (!VerifyZlibHeader(Stream))
                    throw new IOException("Zlib header has incorrect data.");
            }

            // TODO: Adler-32 checksum handling. (Check GZipStream?)
            DeflateStream = new DeflateStream(stream, mode, true);
        }

        /// <summary>
        /// Compresses an array of bytes.
        /// </summary>
        /// <param name="data">Data to compress.</param>
        public static byte[] Compress(byte[] data, CompressionLevel compressionLevel)
        {
            // Compress the data.
            using var resultStream = new MemoryStream();
            var zlibStream = new ZlibStream(resultStream, compressionLevel, true);
            
            // Write compressed data to result.
            zlibStream.Write(data, 0, data.Length);
            zlibStream.Dispose();

            // Return compressed data
            return resultStream.ToArray();
        }

        /// <summary>
        /// Decompresses an array of Zlib data.
        /// </summary>
        /// <param name="data">Compressed Zlib data.</param>
        public static byte[] Decompress(byte[] compressedData)
        {
            // Decompress the Zlib-compressed data.
            using (var compressedStream = new MemoryStream(compressedData))
            {
                using (var zlibStream = new ZlibStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        // Copy decompressed data to result.
                        zlibStream.CopyTo(resultStream);

                        // Return decompressed data.
                        return resultStream.ToArray();
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                if (DeflateStream != null) DeflateStream.Close();

                if (CompressionMode == CompressionMode.Compress)
                {
                    if (BytesProcessed <= 0)
                    {
                        // Special case: zero-length file needs "\x03\x00" in order to not be misdetected as uncompressed.
                        byte[] b_extra03 = new byte[] { 0x03, 0x00 };
                        ProcessAdler32(b_extra03, 0, b_extra03.Length);
                        Stream.Write(b_extra03, 0, b_extra03.Length);
                    }

                    // Write the Adler-32 checksum.
                    uint adler32 = unchecked((S2 << 16) | S1);
                    byte[] b_adler32 = BitConverter.GetBytes(adler32);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(b_adler32);

                    Stream.Write(b_adler32, 0, b_adler32.Length);
                    Stream.Flush();
                }

                if (!LeaveOpen) Stream.Close();
                IsDisposed = true;
            }
        }

        // Implies compressionLevel is Optimal.
        public ZlibStream(Stream stream, CompressionMode mode) : this(stream, mode, false) { }

        /// <summary>
        /// Write the Zlib header to the stream.
        /// </summary>
        /// <param name="compressionLevel">Compression level.</param>
        protected void WriteZlibHeader(CompressionLevel compressionLevel)
        {
            /* NOTE: There doesn't appear to be a "maximum" compression option.
                     This would be zlib level 9, or byte 0xDA.
                     Also, "Optimal" is level 6; zlib default is level 5.

               References:
               - https://stackoverflow.com/questions/9050260/what-does-a-zlib-header-look-like
               - https://stackoverflow.com/a/17176881 */
            byte[] zlibHeader = new byte[] { 0x78, 0x00 };

            zlibHeader[1] = compressionLevel switch
            {
                CompressionLevel.Optimal => 0x9C,
                _ => 0x01,
            };

            Stream.Write(zlibHeader, 0, zlibHeader.Length);
        }

        /// <summary>
        /// Verifies the Zlib header from the input stream.
        /// </summary>
        /// <param name="stream">Stream to read header from.</param>
        public static bool VerifyZlibHeader(Stream stream)
        {
            /* Verify the zlib header.

               Reference: https://tools.ietf.org/html/rfc1950

               Two bytes: CMF FLG

               CMF: Compression method
                    bits 0-3 = CM: compression method (8 for Deflate)
                    bits 4-7 = CINFO: window size (7 for 2^(7+8) == 2^15 == 32 KB)
                    zlib is *always* 0x78. (Deflate, 32 KB)

               FLG: Flags
                    bits 0-4 = FCHECK: check bits for CMF and FLG
                    bit    5 = FDICT: preset dictionary
                    bits 6-7 = FLEVEL: compression level

               FCHECK must be set such that when CMF/FLG is viewed as a 16-bit BE unsigned int, CMFFLG % 31 == 0. */
            byte[] zlibHeader = new byte[2];

            stream.Read(zlibHeader, 0, zlibHeader.Length);

            // Check CMF - throw if not Deflate with 32 KB window.
            if (zlibHeader[0] != 0x78)
                return false;

            // Check FCHECK.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(zlibHeader);

            // Checksum error.
            if (BitConverter.ToUInt16(zlibHeader, 0) % 31 != 0)
                return false;

            return true;
        }

        /* The following properties are from the reference source for DeflateStream.
           https://referencesource.microsoft.com/#system/sys/System/IO/compression/DeflateStream.cs */

        private void ValidateParameters(byte[] array, int offset, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (array.Length - offset < count)
                throw new ArgumentException("Invalid values for offset and count.");
        }

        private void EnsureNotDisposed()
        {
            if (Stream == null)
                throw new ObjectDisposedException(null, "Object has been disposed.");
        }

        private void EnsureDecompressionMode()
        {
            if (CompressionMode != CompressionMode.Decompress)
                throw new InvalidOperationException("Cannot read from a stream opened for compression.");
        }

        private void EnsureCompressionMode()
        {
            if (CompressionMode != CompressionMode.Compress)
                throw new InvalidOperationException("Cannot write to a stream opened for decompression.");
        }

        public override bool CanRead
        {
            get
            {
                if (Stream == null)
                    return false;

                return CompressionMode == CompressionMode.Decompress && Stream.CanRead;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (Stream == null)
                    return false;

                return CompressionMode == CompressionMode.Compress && Stream.CanWrite;
            }
        }

        public override bool CanSeek { get => false; }

        public override long Length
        {
            get => throw new NotSupportedException("ZlibStream does not support getting the stream length.");
        }

        public override long Position
        {
            get => throw new NotSupportedException("ZlibStream does not support getting the stream position.");
            set => throw new NotSupportedException("ZlibStream does not support setting the stream position.");
        }

        public override void Flush() => EnsureNotDisposed();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException("ZlibStream does not support seeking.");

        public override void SetLength(long value)
            => throw new NotSupportedException("ZlibStream does not support setting the stream length.");

        /// <summary>
        /// Process the Adler-32 checksum on a chunk of data.
        /// </summary>
        /// <param name="buffer">Data buffer.</param>
        /// <param name="offset">Offset within buffer.</param>
        /// <param name="count">Number of bytes to process.</param>
        protected void ProcessAdler32(byte[] buffer, int offset, int count)
        {
            int end = offset + count;

            for (int i = offset; i < end; i++)
            {
                S1 = (S1 + buffer[i]) % A32Mod;
                S2 = (S2 + S1) % A32Mod;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            EnsureDecompressionMode();
            ValidateParameters(buffer, offset, count);
            EnsureNotDisposed();

            // TODO: If we reach the end of the stream, verify the Adler-32 checksum.
            int bytesRead = DeflateStream.Read(buffer, offset, count);

            ProcessAdler32(buffer, offset, bytesRead);

            BytesProcessed += bytesRead;

            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            EnsureCompressionMode();
            ValidateParameters(buffer, offset, count);
            EnsureNotDisposed();

            ProcessAdler32(buffer, offset, count);

            DeflateStream.Write(buffer, offset, count);

            BytesProcessed += count;
        }
    }
}