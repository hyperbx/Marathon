using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public interface IChunk
    {
        void Read(BINAReader in_reader);

        void Write(BINAWriter in_writer);

        virtual string GetChunkID()
        {
            return string.Empty;
        }
    }
}
