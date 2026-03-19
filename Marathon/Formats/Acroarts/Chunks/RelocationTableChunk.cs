using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class RelocationTableChunk : IBinarySerializableEx
    {
        public const string ID = "POF0";

        public BINARelocationTable RelocationTable { get; set; }

        public RelocationTableChunk() { }

        public RelocationTableChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkHeader = in_reader.ReadObject<ChunkHeader>();

            if (!chunkHeader.ID.Equals(ID))
                throw new InvalidSignatureException(ID, chunkHeader.ID);

            var length = in_reader.Read<uint>();

            RelocationTable = new BINARelocationTable(in_reader.OffsetOrigin - BINAHeader.Size);
            RelocationTable.Read(in_reader, length - sizeof(uint));

            in_reader.Align(16);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var chunkHeader = new ChunkHeader(in_writer, ID);
            var length = in_writer.Reserve<uint>(true);

            RelocationTable = new BINARelocationTable(in_writer.OffsetOrigin - BINAHeader.Size);
            RelocationTable.AddOffsets(in_writer.Offsets.Values);
            RelocationTable.Write(in_writer);

            in_writer.WriteReserved(length, RelocationTable.Length <= 0 ? 0U : (uint)RelocationTable.Length + sizeof(uint));
            in_writer.Align(16);

            chunkHeader.FinishWrite(in_writer);
        }
    }
}
