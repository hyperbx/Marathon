using System;
using System.IO;
using System.IO.Compression;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Marathon.IO.Types.FileSystem
{
    public interface IFile : INode, IDisposable
    {
        bool IsDirectory => false;

        long Length { get; set; }

        long UncompressedLength { get; set; }

        Func<IFile, CompressionLevel, bool> Compress { get; set; }

        Func<IFile, bool> Decompress { get; set; }

        Stream BaseStream { get; set; }

        Stream Open(FileAccess in_access = FileAccess.Read);
    }
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
