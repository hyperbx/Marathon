using System;
using System.Numerics;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class BIntegerType : BObjectType<BInteger>
    {
        public readonly int intSize;

        public BIntegerType(int intSize) => this.intSize = intSize;

        public BInteger raw_parse(ExtendedBinaryReader buffer, BHeader header)
        {
            BInteger value;

            switch (intSize)
            {
                case 0:
                {
                    value = new BInteger(0);
                    break;
                }

                case 1:
                {
                    value = new BInteger(buffer.ReadByte());
                    break;
                }

                case 2:
                {
                    value = new BInteger(buffer.ReadInt16());
                    break;
                }

                case 4:
                {
                    value = new BInteger(buffer.ReadInt32());
                    break;
                }

                default:
                {
                    byte[] bytes = new byte[intSize];

                    int start = 0, delta = 1;

                    if (!buffer.IsBigEndian)
                    {
                        start = intSize - 1;
                        delta = -1;
                    }

                    for (int i = start; i >= 0 && i < intSize; i += delta)
                        bytes[i] = buffer.ReadByte();

                    value = new BInteger(new BigInteger(bytes));
                    break;
                }
            }

            return value;
        }

        public override BInteger parse(ExtendedBinaryReader buffer, BHeader header)
        {
            BInteger value = raw_parse(buffer, header);

            if (header.debug)
                Console.WriteLine($"-- parsed <integer> {value.asInt()}");

            return value;
        }
    }
}
