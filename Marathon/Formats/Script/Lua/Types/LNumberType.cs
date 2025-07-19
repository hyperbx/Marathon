using Marathon.IO;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LNumberType : BObjectType<LNumber>
    {
        public int Size { get; }

        public bool IsIntegral { get; }

        public LNumberType(int in_size, bool in_integral)
        {
            Size = in_size;
            IsIntegral = in_integral;

            if (in_size == 4 || in_size == 8)
                return;

            throw new NotSupportedException($"Unsupported Lua number size: {in_size}");
        }

        public override LNumber Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            LNumber value = null;

            if (IsIntegral)
            {
                switch (Size)
                {
                    case 4:
                        value = new LIntNumber(in_reader.Read<int>());
                        break;

                    case 8:
                        value = new LLongNumber(in_reader.Read<long>());
                        break;
                }
            }
            else
            {
                switch (Size)
                {
                    case 4:
                        value = new LFloatNumber(in_reader.Read<float>());
                        break;

                    case 8:
                        value = new LDoubleNumber(in_reader.Read<double>());
                        break;
                }
            }

            if (value == null)
                throw new NotSupportedException("Unsupported Lua number format.");

            return value;
        }
    }
}
