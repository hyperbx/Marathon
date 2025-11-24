using Marathon.IO.Types.FileSystem;
using System.IO;
using System.IO.Compression;

namespace Marathon.Extensions
{
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Clones a file and returns a new instance of it with compressed data.
        /// </summary>
        /// <typeparam name="T">The file type.</typeparam>
        /// <param name="in_file">The file to compress.</param>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>A new file with compressed data.</returns>
        /// <exception cref="IOException"/>
        public static IFile Compress<T>(this IFile in_file, CompressionLevel in_compressionLevel = CompressionLevel.Optimal) where T : IFile, new()
        {
            if (in_file.CompressionService == null || in_file.UncompressedLength > 0)
                return in_file;

            var destStream = new MemoryStream();

            in_file.CompressionService.Compress(in_file.BaseStream, destStream, in_compressionLevel);

            return new T()
            {
                Name = in_file.Name,
                Parent = in_file.Parent,
                UncompressedLength = in_file.BaseStream.Length,
                CompressionService = in_file.CompressionService,
                BaseStream = destStream
            };
        }

        /// <summary>
        /// Clones a file and returns a new instance of it with uncompressed data.
        /// </summary>
        /// <typeparam name="T">The file type.</typeparam>
        /// <param name="in_file">The file to decompress.</param>
        /// <returns>A new file with uncompressed data.</returns>
        /// <exception cref="IOException"/>
        public static IFile Decompress<T>(this IFile in_file) where T : IFile, new()
        {
            if (in_file.CompressionService == null || in_file.UncompressedLength <= 0)
                return in_file;

            var destStream = new MemoryStream();

            in_file.CompressionService.Decompress(in_file.Open(), destStream, in_file.UncompressedLength);

            return new T()
            {
                Name = in_file.Name,
                Parent = in_file.Parent,
                UncompressedLength = 0,
                CompressionService = in_file.CompressionService,
                BaseStream = destStream
            };
        }
    }
}
