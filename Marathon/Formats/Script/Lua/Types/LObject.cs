using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class LObject : BObject
    {
        public virtual string Dereference()
        {
            throw new NotSupportedException();
        }

        public abstract new bool Equals(object in_obj);
    }
}
