using System.IO.Compression;

namespace Marathon.IO.Interfaces
{
    public interface IArchive
    {
        IArchiveDirectory Root { get; }

        CompressionLevel CompressionLevel { get; set; }

        void Extract(string location);
    }
}
