// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LLocal(LString in_name, BInteger in_start, BInteger in_end) : BObject
    {
        public LString Name => in_name;

        public int Start => in_start.AsInt();

        public int End => in_end.AsInt();

        public override string ToString()
        {
            return Name.Dereference();
        }
    }
}
