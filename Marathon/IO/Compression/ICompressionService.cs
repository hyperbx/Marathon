using Marathon.IO.Compression.Events;
using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Compression
{
    public interface ICompressionService
    {
        event CompressionProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Compresses the input stream and copies the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The stream of uncompressed data.</param>
        /// <param name="in_destStream">The destination stream to copy compressed data to.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>The length of the compressed data.</returns>
        long Compress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal);

        /// <summary>
        /// Decompresses the input stream and copies the result to the destination stream.
        /// </summary>
        /// <param name="in_srcStream">The stream of compressed data.</param>
        /// <param name="in_destStream">The destination stream to copy uncompressed data to.</param>
        /// <param name="in_uncompressedLength">The uncompressed length of the file. Usually used for validation depending on the compression format, but is optional.</param>
        /// <returns>The length of the uncompressed data.</returns>
        long Decompress(Stream in_srcStream, Stream in_destStream, long in_uncompressedLength = -1);

        void OnCompress(CompressionProgressChangedEventArgs in_args);

        void OnDecompress(CompressionProgressChangedEventArgs in_args);
    }
}
