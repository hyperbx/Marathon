// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LBoolean(bool in_value) : LObject
    {
        private readonly bool _value = in_value;

        public static LBoolean True => new(true);

        public static LBoolean False => new(false);

        public override bool Equals(object in_obj)
        {
            return this == in_obj;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
