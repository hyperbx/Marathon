using Amicitia.IO;
using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System.IO;

namespace Marathon.IO.Types.BINA
{
    public class BINAReader : BinaryObjectReaderEx
    {
        public BINAHeader Header { get; private set; }

        public BINAReader(Stream in_stream, bool in_isBigEndian = true)
            : base(in_stream, StreamOwnership.Retain, in_isBigEndian ? Endianness.Big : Endianness.Little, EncodingFactory.ShiftJIS)
        {
            Header = new BINAHeader(this);
        }

        public void Align(int in_alignment)
        {
            Seek(AlignmentHelper.Align(Position, in_alignment), SeekOrigin.Begin);
        }
    }
}
