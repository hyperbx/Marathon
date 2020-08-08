using System;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class BooleanIndicator : Block
    {
        public BooleanIndicator(LFunction function, int line) : base(function, line, line) { }

        public override void addStatement(Statement statement) { /* Nothing to see here... */ }

        public override bool isContainer() => false;

        public override bool isUnprotected() => false;

        public override bool breakable() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out) => @out.print("-- unhandled boolean indicator");
    }
}
