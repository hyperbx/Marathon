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
        /// The method used to compress data in <see cref="Compress(CompressionLevel)"/>.
        /// </summary>
        CompressionDelegate CompressionMethod { get; set; }

        /// <summary>
        /// The method used to decompress data in <see cref="Decompress"/>.
        /// </summary>
        DecompressionDelegate DecompressionMethod { get; set; }

        /// <summary>
        /// The stream pertaining to this file's data.
        /// </summary>
        Stream BaseStream { get; set; }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="in_access">The access level for opening this file.</param>
        /// <returns>A stream for this file's data.</returns>
        Stream Open(FileAccess in_access = FileAccess.Read);

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

    /// <summary>
    /// The delegate for compressing files.
    /// </summary>
    /// <param name="in_srcStream">The uncompressed data stream.</param>
    /// <param name="in_destStream">The stream to copy compressed data to.</param>
    /// <param name="in_compressionLevel">The level of compression to use.</param>
    /// <returns><b>true</b> if compression succeeded. Otherwise, <b>false</b>.</returns>
    public delegate bool CompressionDelegate(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal);

    /// <summary>
    /// The delegate for decompressing files.
    /// </summary>
    /// <param name="in_srcStream">The compressed data stream.</param>
    /// <param name="in_destStream">The stream to copy uncompressed data to.</param>
    /// <returns><b>true</b> if decompression succeeded. Otherwise, <b>false</b>.</returns>
    public delegate bool DecompressionDelegate(Stream in_srcStream, Stream in_destStream);
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
