using Marathon.IO;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LConstantType : BObjectType<LObject>
    {
        public override LObject Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var type = in_reader.Read<byte>();

            return type switch
            {
                0 => LNil.Nil,
                1 => in_header.Boolean.Parse(in_reader, in_header),
                3 => in_header.Number.Parse(in_reader, in_header),
                4 => in_header.String.Parse(in_reader, in_header),
                _ => throw new Exception($"Unexpected constant type: {type}"),
            };
        }
    }
}
