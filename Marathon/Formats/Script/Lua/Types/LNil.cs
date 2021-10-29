using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LNil : LObject
    {
        public static readonly LNil NIL = new();

        private LNil() { }

        public override string Dereference() => throw new NotImplementedException();

        public override bool Equals(object o) => this == o;
    }
}
