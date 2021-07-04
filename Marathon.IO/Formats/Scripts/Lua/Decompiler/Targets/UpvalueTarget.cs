using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets
{
    public class UpvalueTarget : Target
    {
        private readonly string name;

        public UpvalueTarget(string name) => this.name = name;

        public override void print(Output @out) => @out.print(name);

        public override void printMethod(Output @out) => throw new Exception();
    }
}
