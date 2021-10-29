namespace Marathon.IO.Interfaces
{
    public interface IArchiveFile
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public int Offset { get; set; }

        public int CompressedSize { get; set; }

        public int DecompressedSize { get; set; }

        public bool IsDecompressed { get; set; }

        public byte[] Data { get; set; }
    }
}
