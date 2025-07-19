using Amicitia.IO.Binary;
using Marathon.IO;
using System.Numerics;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class BIntegerType(int in_intSize) : BObjectType<BInteger>
    {
        public int Size => in_intSize;

        public BInteger RawParse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            BInteger value;

            switch (Size)
            {
                case 0:
                    value = new BInteger(0);
                    break;

                case 1:
                    value = new BInteger(in_reader.Read<byte>());
                    break;

                case 2:
                    value = new BInteger(in_reader.Read<short>());
                    break;

                case 4:
                    value = new BInteger(in_reader.Read<int>());
                    break;

                default:
                {
                    var bytes = new byte[Size];
                    var start = 0;
                    var delta = 1;

                    if (in_reader.Endianness != Endianness.Big)
                    {
                        start = Size - 1;
                        delta = -1;
                    }

                    for (int i = start; i >= 0 && i < Size; i += delta)
                        bytes[i] = in_reader.Read<byte>();

                    value = new BInteger(new BigInteger(bytes));

                    break;
                }
            }

            return value;
        }

        public override BInteger Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            return RawParse(in_reader, in_header);
        }
    }
}
