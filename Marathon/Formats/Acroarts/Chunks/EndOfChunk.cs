using Marathon.Exceptions;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class EndOfChunk : IBinarySerializableEx
    {
        public const string ID = "EOFC"; // "End OF Chunk"

        public EndOfChunk() { }

        public EndOfChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<ChunkHeader>();

            if (header.ID.Equals(ID))
                return;

            throw new InvalidSignatureException(ID, header.ID);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            new ChunkHeader(in_writer, ID).FinishWrite(in_writer, 0, ChunkHeader.DefaultHeaderSize);
        }
    }
}
