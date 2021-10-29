using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BSizeTType : BObjectType<BSizeT>
    {
        public readonly int SizeTSize;

        private BIntegerType _integerType;

        public BSizeTType(int sizeTSize)
        {
            SizeTSize = sizeTSize;
            _integerType = new BIntegerType(sizeTSize);
        }

        public override BSizeT Parse(BinaryReaderEx reader, BHeader header)
        {
            BSizeT value = new(_integerType.RawParse(reader, header));
            Console.WriteLine($"Parsed size_t: {value.AsInt()}");

            return value;
        }
    }
}
