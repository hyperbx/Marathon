using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Compression
{
    public class ZLib
    {
        public static Stream Compress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            var zlibStream = new ZLibStream(in_destStream, in_compressionLevel, true);

            in_srcStream.CopyTo(zlibStream);
            zlibStream.Dispose();

            return in_destStream;
        }

        public static Stream Compress(Stream in_stream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            return Compress(in_stream, new MemoryStream(), in_compressionLevel);
        }

        public static byte[] Compress(byte[] in_buffer, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            using var uncompressedStream = new MemoryStream();
            var zlibStream = new ZLibStream(uncompressedStream, in_compressionLevel);

            zlibStream.Write(in_buffer, 0, in_buffer.Length);
            zlibStream.Dispose();

            return uncompressedStream.ToArray();
        }

        public static bool TryCompress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel, out Stream out_compressedStream)
        {
            try
            {
                out_compressedStream = Compress(in_srcStream, in_destStream, in_compressionLevel);
                return true;
            }
            catch
            {
                out_compressedStream = in_srcStream;
                return false;
            }
        }

        public static bool TryCompress(Stream in_stream, CompressionLevel in_compressionLevel, out Stream out_compressedStream)
        {
            try
            {
                out_compressedStream = Compress(in_stream, in_compressionLevel);
                return true;
            }
            catch
            {
                out_compressedStream = in_stream;
                return false;
            }
        }

        public static Stream Decompress(Stream in_srcStream, Stream in_destStream)
        {
            var zlibStream = new ZLibStream(in_srcStream, CompressionMode.Decompress, true);

            zlibStream.CopyTo(in_destStream);
            zlibStream.Dispose();

            return in_destStream;
        }

        public static Stream Decompress(Stream in_stream)
        {
            return Decompress(in_stream, new MemoryStream());
        }

        public static byte[] Decompress(byte[] in_buffer)
        {
            using var compressedStream = new MemoryStream(in_buffer);
            var zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();

            zlibStream.CopyTo(resultStream);
            zlibStream.Dispose();

            return resultStream.ToArray();
        }

        public static bool TryDecompress(Stream in_srcStream, Stream in_destStream, out Stream out_uncompressedStream)
        {
            try
            {
                out_uncompressedStream = Decompress(in_srcStream, in_destStream);
                return true;
            }
            catch
            {
                out_uncompressedStream = in_srcStream;
                return false;
            }
        }

        public static bool TryDecompress(Stream in_stream, out Stream out_uncompressedStream)
        {
            try
            {
                out_uncompressedStream = Decompress(in_stream);
                return true;
            }
            catch
            {
                out_uncompressedStream = in_stream;
                return false;
            }
        }
    }
}
