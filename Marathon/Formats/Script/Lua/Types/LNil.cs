using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LNil : LObject
    {
        public static LNil Nil => new();

        public override string Dereference()
        {
            throw new NotSupportedException();
        }

        public override bool Equals(object in_obj)
        {
            return this == in_obj;
        }
    }
}
