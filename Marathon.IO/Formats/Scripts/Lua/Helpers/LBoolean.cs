namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LBoolean : LObject
    {
        public static readonly LBoolean LTRUE = new LBoolean(true);
        public static readonly LBoolean LFALSE = new LBoolean(false);

        private readonly bool value;

        private LBoolean(bool value) => this.value = value;

        public string toString() => value.ToString();

        public override bool equals(object o) => this == o;
    }
}
