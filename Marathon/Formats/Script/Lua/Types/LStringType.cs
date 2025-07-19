using Marathon.IO;
using System.Text;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LStringType : BObjectType<LString>
    {
        public override LString Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var sb = new StringBuilder();
            var sizeT = in_header.SizeT.Parse(in_reader, in_header);

            sizeT.Iterate(() => sb.Append((char)in_reader.Read<byte>()));

            return new LString(sizeT, sb.ToString());
        }
    }
}
