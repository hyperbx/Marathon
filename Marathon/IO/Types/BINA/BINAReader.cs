using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System.IO;

namespace Marathon.IO.Types.BINA
{
    public class BINAReader : BinaryObjectReaderEx
    {
        public BINAHeader Header { get; private set; }

        public BINAReader(Stream in_stream, long in_offset = 0, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            JumpTo(in_offset);

            Header = new BINAHeader(this);

            PushOffsetOrigin(Header.Offset + BINAHeader.Size);
        }

        public BINARelocationTable ReadRelocationTable()
        {
            var relocTable = new BINARelocationTable(Header.Offset);
            var pos = Position;

            JumpTo(CalculateOffset(Header.RelocTableOffset));
            relocTable.Read(this, Header.RelocTableLength);
            JumpTo(pos);

            return relocTable;
        }
    }
}
