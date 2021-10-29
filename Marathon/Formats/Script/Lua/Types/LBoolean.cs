namespace Marathon.Formats.Script.Lua.Types
{
    public class LBoolean : LObject
    {
        public static readonly LBoolean LTRUE = new(true);
        public static readonly LBoolean LFALSE = new(false);

        private readonly bool _value;

        private LBoolean(bool value) => _value = value;

        public override string ToString() => _value.ToString();

        public override bool Equals(object o) => this == o;
    }
}
