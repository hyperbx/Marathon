using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System.IO;

namespace Marathon.IO.Types.BINA
{
    public class BINAReader : BinaryObjectReaderEx
    {
        public BINAHeader Header { get; private set; }

        public long Offset { get; private set; }

        public BINAReader(Stream in_stream, long in_offset = 0, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            Offset = in_offset;

            JumpTo(Offset);

            Header = new BINAHeader(this);
        }
    }
}
