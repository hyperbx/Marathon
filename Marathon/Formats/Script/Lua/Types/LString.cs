namespace Marathon.Formats.Script.Lua.Types
{
    public class LString : LObject
    {
        public readonly BSizeT Size;
        public readonly string Value;

        public LString(BSizeT size, string value)
        {
            Size = size;
            Value = value.Length == 0 ? "" : value[0..^1];
        }

        public override string Dereference() => Value;

        public override string ToString() => $"\"{Value}\"";

        public override bool Equals(object o)
        {
            if (o is LString lString)
                return lString.Value.Equals(Value);

            return false;
        }
    }
}
