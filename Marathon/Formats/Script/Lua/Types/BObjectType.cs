using Marathon.IO;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class BObjectType<T> where T : BObject
    {
        public abstract T Parse(BinaryObjectReaderEx in_reader, BHeader in_header);

        public BList<T> ParseList(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var length = in_header.Integer.Parse(in_reader, in_header);
            var values = new List<T>();

            length.Iterate(() => values.Add(Parse(in_reader, in_header)));

            return new BList<T>(length, values);
        }
    }
}
