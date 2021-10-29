using System.Collections.Generic;
using System.IO.Compression;

namespace Marathon.IO.Interfaces
{
    public interface IArchive
    {
        public CompressionLevel CompressionLevel { get; set; }

        public IList<IArchiveFile> Files { get; set; }

        void Index(BinaryReaderEx reader);

        byte[] Compress(byte[] data, CompressionLevel compressionLevel);

        byte[] Decompress(BinaryReaderEx reader, int offset, int compressedSize, int decompressedSize);
    }
}
