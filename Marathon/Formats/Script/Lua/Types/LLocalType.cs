using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LLocalType : BObjectType<LLocal>
    {
        public override LLocal Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var name = in_header.String.Parse(in_reader, in_header);
            var start = in_header.Integer.Parse(in_reader, in_header);
            var end = in_header.Integer.Parse(in_reader, in_header);

            return new LLocal(name, start, end);
        }
    }
}
