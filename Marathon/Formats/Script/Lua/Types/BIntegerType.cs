using System;
using System.Numerics;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BIntegerType : BObjectType<BInteger>
    {
        public readonly int IntSize;

        public BIntegerType(int intSize) => IntSize = intSize;

        public BInteger RawParse(BinaryReaderEx reader, BHeader header)
        {
            BInteger value;

            switch (IntSize)
            {
                case 0:
                    value = new BInteger(0);
                    break;

                case 1:
                    value = new BInteger(reader.ReadByte());
                    break;

                case 2:
                    value = new BInteger(reader.ReadInt16());
                    break;

                case 4:
                    value = new BInteger(reader.ReadInt32());
                    break;

                default:
                {
                    byte[] bytes = new byte[IntSize];

                    int start = 0, delta = 1;

                    if (!reader.IsBigEndian)
                    {
                        start = IntSize - 1;
                        delta = -1;
                    }

                    for (int i = start; i >= 0 && i < IntSize; i += delta)
                        bytes[i] = reader.ReadByte();

                    value = new BInteger(new BigInteger(bytes));

                    break;
                }
            }

            return value;
        }

        public override BInteger Parse(BinaryReaderEx reader, BHeader header)
        {
            BInteger value = RawParse(reader, header);
            Console.WriteLine($"Parsed int: {value.AsInt()}");

            return value;
        }
    }
}
