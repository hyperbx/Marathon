using Marathon.IO.Compression;
using System;
using System.IO;
using System.IO.Compression;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Marathon.IO.Types.FileSystem
{
    public interface IFile : INode, IDisposable
    {
        /// <summary>
        /// This node is a file.
        /// </summary>
        bool IsDirectory => false;

        /// <summary>
        /// The length of this file.
        /// </summary>
        long Length { get; set; }

        /// <summary>
        /// The original uncompressed length of this file.
        /// </summary>
        long UncompressedLength { get; set; }

        /// <summary>
        /// The service used for compression and decompression of this file.
        /// </summary>
        ICompressionService CompressionService { get; set; }

        /// <summary>
        /// The underlying stream to this file.
        /// </summary>
        Stream BaseStream { get; set; }

        /// <summary>
        /// Clones this file.
        /// </summary>
        /// <returns>A new file with the same properties as this file.</returns>
        IFile Clone();

        /// <summary>
        /// Opens this file.
        /// </summary>
        /// <param name="in_access">The access level for opening this file.</param>
        /// <returns>A stream for this file's data.</returns>
        Stream Open(FileAccess in_access = FileAccess.Read);

        /// <summary>
        /// Exports this file.
        /// </summary>
        /// <param name="in_path">The path on disk to export to.</param>
        /// <param name="in_overwrite">Determines whether to overwrite an existing file of the same name.</param>
        void Export(string in_path, bool in_overwrite = true);

        /// <summary>
        /// Replaces this file's data with another file's data.
        /// </summary>
        /// <param name="in_file">The source file.</param>
        void ReplaceWith(IFile in_file);

        /// <summary>
        /// Clones this file and returns a new instance of it with compressed data.
        /// </summary>
        /// <param name="in_compressionLevel">The level of compression to use.</param>
        /// <returns>A new file with compressed data.</returns>
        IFile Compress(CompressionLevel in_compressionLevel = CompressionLevel.Optimal);

        /// <summary>
        /// Clones this file and returns a new instance of it with uncompressed data.
        /// </summary>
        /// <returns>A new file with uncompressed data.</returns>
        IFile Decompress();
    }
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
