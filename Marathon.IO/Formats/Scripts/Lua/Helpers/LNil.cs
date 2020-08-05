using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LNil : LObject
    {
        public static readonly LNil NIL = new LNil();

        private LNil() { }

        public override string deref() => throw new NotImplementedException();

        public override bool equals(object o) => this == o;
    }
}
