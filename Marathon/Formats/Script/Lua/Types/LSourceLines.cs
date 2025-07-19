using Marathon.IO;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LSourceLines
    {
        public static LSourceLines Parse(BinaryObjectReaderEx in_reader)
        {
            var number = in_reader.Read<int>();

            while (number-- > 0)
                in_reader.Read<int>();

            return null;
        }
    }
}
