using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System.IO;

namespace Marathon.IO.Types.BINA
{
    public class BINAReader : BinaryObjectReaderEx
    {
        public BINAHeader Header { get; private set; }

        public BINAReader(Stream in_stream, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            Header = new BINAHeader(this);
        }
    }
}
