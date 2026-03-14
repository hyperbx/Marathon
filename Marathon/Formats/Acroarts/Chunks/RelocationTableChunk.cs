using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class RelocationTableChunk : IChunk
    {
        public const string ID = "POF0";

        public long Offset { get; set; }

        public BINARelocationTable RelocationTable { get; set; }

        public RelocationTableChunk() { }

        public RelocationTableChunk(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            var chunkHeader = in_reader.ReadObject<ChunkHeader>();

            if (!chunkHeader.ID.Equals(ID))
                throw new InvalidSignatureException(ID, chunkHeader.ID);

            var length = in_reader.Read<uint>();

            RelocationTable = new BINARelocationTable(in_parentChunk.Offset - BINAHeader.Size);
            RelocationTable.Read(in_reader, length);

            in_reader.JumpAhead(length);
            in_reader.Align(16);
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            var chunkHeader = new ChunkHeader(in_writer, ID);
            var length = in_writer.Reserve<uint>(true);

            RelocationTable = new BINARelocationTable(in_parentChunk.Offset - BINAHeader.Size);
            RelocationTable.AddOffsets(in_writer.Offsets.Values);
            RelocationTable.Write(in_writer);

            in_writer.WriteReserved(length, (uint)RelocationTable.Length + 4);
            in_writer.Align(16);

            chunkHeader.FinishWrite(in_writer);
        }
    }
}
