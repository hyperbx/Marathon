using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LNumberType : BObjectType<LNumber>
    {
        public readonly int Size;
        public readonly bool Integral;

        public LNumberType(int size, bool integral)
        {
            Size = size;
            Integral = integral;

            if (!(size == 4 || size == 8))
                throw new Exception($"The input chunk has an unsupported Lua number size: {size}");
        }

        public override LNumber Parse(BinaryReaderEx reader, BHeader header)
        {
            LNumber value = null;

            if (Integral)
            {
                switch (Size)
                {
                    case 4:
                        value = new LIntNumber(reader.ReadInt32());
                        break;

                    case 8:
                        value = new LLongNumber(reader.ReadInt64());
                        break;
                }
            }
            else
            {
                switch (Size)
                {
                    case 4:
                        value = new LFloatNumber(reader.ReadInt32());
                        break;

                    case 8:
                        value = new LDoubleNumber(reader.ReadInt64());
                        break;
                }
            }

            if (value == null)
                throw new Exception("The input chunk has an unsupported Lua number format");

            Console.WriteLine($"Parsed number: {value}");

            return value;
        }
    }
}
