using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class LObject : BObject
    {
        public virtual string Dereference() => throw new Exception();

        public abstract new bool Equals(object o);
    }
}
