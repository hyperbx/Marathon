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
            if (in_file.CompressionMethod == null || in_file.UncompressedLength > 0)
                return in_file;

            var destStream = new MemoryStream();

            if (!in_file.CompressionMethod(in_file.BaseStream, destStream, in_compressionLevel))
                throw new IOException($"Failed to compress file: {in_file.Path}");

            return new T()
            {
                Name = in_file.Name,
                Parent = in_file.Parent,
                UncompressedLength = in_file.BaseStream.Length,
                CompressionMethod = in_file.CompressionMethod,
                DecompressionMethod = in_file.DecompressionMethod,
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
            if (in_file.DecompressionMethod == null || in_file.UncompressedLength <= 0)
                return in_file;

            var destStream = new MemoryStream();

            if (!in_file.DecompressionMethod(in_file.Open(), destStream) || destStream.Length != in_file.UncompressedLength)
                throw new IOException($"Failed to decompress file: {in_file.Path}");

            return new T()
            {
                Name = in_file.Name,
                Parent = in_file.Parent,
                UncompressedLength = 0,
                CompressionMethod = in_file.CompressionMethod,
                DecompressionMethod = in_file.DecompressionMethod,
                BaseStream = destStream
            };
        }
    }
}
