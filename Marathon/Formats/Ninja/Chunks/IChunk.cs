using Marathon.IO;

namespace Marathon.Formats.Ninja.Chunks
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
