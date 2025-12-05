using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class EndOfChunk : IChunk
    {
        public const string ID = "EOFC"; // "End OF Chunk"

        public EndOfChunk() { }

        public EndOfChunk(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var header = in_reader.ReadObject<ChunkHeader>();

            if (header.ID.Equals(GetChunkID()))
                return;

            throw new InvalidSignatureException(GetChunkID(), header.ID);
        }

        public void Write(BINAWriter in_writer)
        {
            new ChunkHeader(in_writer, GetChunkID()).FinishWrite(in_writer);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
