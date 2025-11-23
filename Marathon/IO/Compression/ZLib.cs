using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Compression
{
    public class ZLib
    {
        /// <summary>
        /// Compresses the source stream and copies the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The uncompressed data stream.</param>
        /// <param name="in_destStream">The destination stream to copy compressed data to.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>The destination stream containing compressed data.</returns>
        public static Stream Compress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            if (in_srcStream.CanSeek)
                in_srcStream.Position = 0;

            using (var zlibStream = new ZLibStream(in_destStream, in_compressionLevel, true))
                in_srcStream.CopyTo(zlibStream);

            return in_destStream;
        }

        /// <summary>
        /// Compresses the source stream and copies the result to a new <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="in_stream">The uncompressed data stream.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>A new <see cref="MemoryStream"/> containing compressed data.</returns>
        public static Stream Compress(Stream in_stream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            return Compress(in_stream, new MemoryStream(), in_compressionLevel);
        }

        /// <summary>
        /// Compresses the input buffer and returns the result as a new buffer.
        /// </summary>
        /// <param name="in_buffer">The uncompressed data buffer.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>A new buffer containing compressed data.</returns>
        public static byte[] Compress(byte[] in_buffer, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            using var uncompressedStream = new MemoryStream();

            using (var zlibStream = new ZLibStream(uncompressedStream, in_compressionLevel))
                zlibStream.Write(in_buffer, 0, in_buffer.Length);

            return uncompressedStream.ToArray();
        }

        /// <summary>
        /// Attempts to compress the source stream and copy the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The uncompressed data stream.</param>
        /// <param name="in_destStream">The destination stream to copy compressed data to.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <param name="out_compressedStream">If successful, the destination stream containing compressed data. Otherwise, <paramref name="in_destStream"/>.</param>
        /// <returns><b>true</b> if compression succeeded. Otherwise, <b>false</b>.</returns>
        public static bool TryCompress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel, out Stream out_compressedStream)
        {
            try
            {
                out_compressedStream = Compress(in_srcStream, in_destStream, in_compressionLevel);
                return true;
            }
            catch
            {
                out_compressedStream = in_destStream;
                return false;
            }
        }

        /// <summary>
        /// Attempts to compress the source stream and copy the result to a new <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="in_stream">The uncompressed data stream.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <param name="out_compressedStream">If successful, a new <see cref="MemoryStream"/> stream containing compressed data. Otherwise, an empty <see cref="MemoryStream"/>.</param>
        /// <returns><b>true</b> if compression succeeded. Otherwise, <b>false</b>.</returns>
        public static bool TryCompress(Stream in_stream, CompressionLevel in_compressionLevel, out Stream out_compressedStream)
        {
            try
            {
                out_compressedStream = Compress(in_stream, in_compressionLevel);
                return true;
            }
            catch
            {
                out_compressedStream = new MemoryStream();
                return false;
            }
        }

        /// <summary>
        /// Decompresses the source stream and copies the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The compressed data stream.</param>
        /// <param name="in_destStream">The destination stream to copy uncompressed data to.</param>
        /// <returns>The destination stream containing uncompressed data.</returns>
        public static Stream Decompress(Stream in_srcStream, Stream in_destStream)
        {
            if (in_srcStream.CanSeek)
                in_srcStream.Position = 0;

            using (var zlibStream = new ZLibStream(in_srcStream, CompressionMode.Decompress, true))
                zlibStream.CopyTo(in_destStream);

            return in_destStream;
        }

        /// <summary>
        /// Decompresses the source stream and copies the result to a new <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="in_stream">The compressed data stream.</param>
        /// <returns>A new <see cref="MemoryStream"/> containing uncompressed data.</returns>
        public static Stream Decompress(Stream in_stream)
        {
            return Decompress(in_stream, new MemoryStream());
        }

        /// <summary>
        /// Decompresses the input buffer and returns the result as a new buffer.
        /// </summary>
        /// <param name="in_buffer">The compressed data buffer.</param>
        /// <returns>A new buffer containing uncompressed data.</returns>
        public static byte[] Decompress(byte[] in_buffer)
        {
            using var compressedStream = new MemoryStream(in_buffer);
            var zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();

            zlibStream.CopyTo(resultStream);
            zlibStream.Dispose();

            return resultStream.ToArray();
        }

        /// <summary>
        /// Attempts to decompress the source stream and copy the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The compressed data stream.</param>
        /// <param name="in_destStream">The destination stream to copy uncompressed data to.</param>
        /// <param name="out_uncompressedStream">If successful, the destination stream containing uncompressed data. Otherwise, <paramref name="in_destStream"/>.</param>
        /// <returns><b>true</b> if decompression succeeded. Otherwise, <b>false</b>.</returns>
        public static bool TryDecompress(Stream in_srcStream, Stream in_destStream, out Stream out_uncompressedStream)
        {
            try
            {
                out_uncompressedStream = Decompress(in_srcStream, in_destStream);
                return true;
            }
            catch
            {
                out_uncompressedStream = in_destStream;
                return false;
            }
        }

        /// <summary>
        /// Attempts to decompress the source stream and copy the result to a new <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="in_stream">The compressed data stream.</param>
        /// <param name="out_uncompressedStream">If successful, a new <see cref="MemoryStream"/> stream containing uncompressed data. Otherwise, an empty <see cref="MemoryStream"/>.</param>
        /// <returns><b>true</b> if decompression succeeded. Otherwise, <b>false</b>.</returns>
        public static bool TryDecompress(Stream in_stream, out Stream out_uncompressedStream)
        {
            try
            {
                out_uncompressedStream = Decompress(in_stream);
                return true;
            }
            catch
            {
                out_uncompressedStream = new MemoryStream();
                return false;
            }
        }
    }
}
