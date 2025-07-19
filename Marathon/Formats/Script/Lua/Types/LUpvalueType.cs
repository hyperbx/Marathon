using Marathon.IO;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LUpvalueType : BObjectType<LUpvalue>
    {
        public override LUpvalue Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            return new LUpvalue()
            {
                IsInStack = in_reader.Read<byte>() != 0,
                Index = in_reader.Read<byte>()
            };
        }
    }
}
