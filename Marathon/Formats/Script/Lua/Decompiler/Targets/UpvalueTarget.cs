using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class UpvalueTarget : Target
    {
        private readonly string _name;

        public UpvalueTarget(string name) => _name = name;

        public override void Write(Output @out)
            => @out.Write(_name);

        public override void WriteMethod(Output @out)
            => throw new Exception();
    }
}
