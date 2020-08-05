namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public abstract class LObject : BObject
    {
        public abstract string deref();

        public abstract bool equals(object o);
    }
}
