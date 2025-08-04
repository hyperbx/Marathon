using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public interface IChunk
    {
        public string ChunkID { get; set; }

        void Read(BinaryObjectReaderEx in_reader);

        void Write(BinaryObjectWriterEx in_writer);
    }
}
