namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LString : LObject
    {
        public readonly BSizeT size;
        public readonly string value;

        public LString(BSizeT size, string value)
        {
            this.size = size;
            this.value = value.Length == 0 ? "" : value.Substring(0, value.Length - 1);
        }

        public override string deref() => value;

        public string toString() => $"\"{value}\"";

        public override bool equals(object o)
        {
            if (o.GetType().Equals(typeof(LString)))
            {
                LString os = (LString)o;

                return os.value.Equals(value);
            }

            return false;
        }
    }
}
