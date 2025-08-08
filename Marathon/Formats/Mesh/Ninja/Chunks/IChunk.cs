using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public interface IChunk
    {
        void Read(BinaryObjectReaderEx in_reader);

        void Write(BinaryObjectWriterEx in_writer);

        public virtual string GetChunkID()
        {
            return string.Empty;
        }
    }
}
