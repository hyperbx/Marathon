using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

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
