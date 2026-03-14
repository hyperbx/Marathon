using Marathon.Exceptions;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class EndOfChunk : IChunk
    {
        public const string ID = "EOFC"; // "End OF Chunk"

        public long Offset { get; set; }

        public EndOfChunk() { }

        public EndOfChunk(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            var header = in_reader.ReadObject<ChunkHeader>();

            if (header.ID.Equals(ID))
                return;

            throw new InvalidSignatureException(ID, header.ID);
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            new ChunkHeader(in_writer, ID).FinishWrite(in_writer, 0, ChunkHeader.DefaultHeaderSize);
        }
    }
}
