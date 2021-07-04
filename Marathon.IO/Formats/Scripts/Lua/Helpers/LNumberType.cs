using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LNumberType : BObjectType<LNumber>
    {
        public readonly int size;
        public readonly bool integral;

        public LNumberType(int size, bool integral)
        {
            this.size = size;
            this.integral = integral;

            if (!(size == 4 || size == 8))
                throw new Exception($"The input chunk has an unsupported Lua number size: {size}");
        }

        public override LNumber parse(ExtendedBinaryReader buffer, BHeader header)
        {
            LNumber value = null;

            if (integral)
            {
                switch (size)
                {
                    case 4:
                    {
                        value = new LIntNumber(buffer.ReadInt32());
                        break;
                    }

                    case 8:
                    {
                        value = new LLongNumber(buffer.ReadInt64());
                        break;
                    }
                }
            }
            else
            {
                switch (size)
                {
                    case 4:
                    {
                        value = new LFloatNumber(buffer.ReadInt32());
                        break;
                    }

                    case 8:
                    {
                        value = new LDoubleNumber(buffer.ReadInt64());
                        break;
                    }
                }
            }

            if (value == null)
                throw new Exception("The input chunk has an unsupported Lua number format");

            if (header.debug)
                Console.WriteLine($"-- parsed <number> {value}");

            return value;
        }
    }
}
