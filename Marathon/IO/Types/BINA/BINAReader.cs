using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Extensions;
using System.IO;
using System.Text;

namespace Marathon.IO.Types.BINA
{
    public class BINAReader : BinaryObjectReaderEx
    {
        public BINAHeader Header { get; private set; }

        public BINAReader(Stream in_stream, bool in_isBigEndian = true)
            : base(in_stream, StreamOwnership.Retain, in_isBigEndian ? Endianness.Big : Endianness.Little, Encoding.ShiftJIS)
        {
            Header = new BINAHeader(this);
        }
    }
}
