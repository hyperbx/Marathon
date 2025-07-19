using Marathon.IO;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LBooleanType : BObjectType<LBoolean>
    {
        public override LBoolean Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var value = in_reader.Read<byte>();

            if ((value & 0xFFFFFFFE) != 0)
            {
                throw new Exception();
            }
            else
            {
                return value == 0 ? LBoolean.False : LBoolean.True;
            }
        }
    }
}
