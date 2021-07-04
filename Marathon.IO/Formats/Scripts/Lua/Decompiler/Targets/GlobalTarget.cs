using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets
{
    public class GlobalTarget : Target
    {
        private readonly string name;

        public GlobalTarget(string name) => this.name = name;

        public override void print(Output @out) => @out.print(name);

        public override void printMethod(Output @out) => throw new Exception();
    }
}
