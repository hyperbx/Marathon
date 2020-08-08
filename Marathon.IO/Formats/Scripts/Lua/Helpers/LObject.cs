using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public abstract class LObject : BObject
    {
        public virtual string deref() => throw new Exception();

        public abstract bool equals(object o);
    }
}
