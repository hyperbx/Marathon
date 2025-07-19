using Marathon.IO;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class BSizeTType(int in_sizeTSize) : BObjectType<BSizeT>
    {
        private readonly BIntegerType _integerType = new(in_sizeTSize);

        public int SizeTSize => in_sizeTSize;

        public override BSizeT Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            return new(_integerType.RawParse(in_reader, in_header));
        }
    }
}
