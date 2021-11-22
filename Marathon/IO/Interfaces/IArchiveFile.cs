namespace Marathon.IO.Interfaces
{
    public interface IArchiveFile : IArchiveData
    {
        uint Offset { get; set; }

        uint Length { get; set; }

        uint UncompressedSize { get; set; }

        byte[] Data { get; set; }

        void Compress(CompressionLevel compressionLevel);

        void Decompress();

        bool IsCompressed();
    }
}
