// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LString(BSizeT in_size, string in_value) : LObject
    {
        public BSizeT Size => in_size;

        public string Value => in_value.Length == 0 ? string.Empty : in_value[0..^1];

        public override string Dereference()
        {
            return Value;
        }

        public override bool Equals(object o)
        {
            if (o is LString out_string)
                return out_string.Value.Equals(Value);

            return false;
        }

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }
}
