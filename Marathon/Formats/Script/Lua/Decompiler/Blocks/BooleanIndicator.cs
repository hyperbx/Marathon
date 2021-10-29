using System;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class BooleanIndicator : Block
    {
        public BooleanIndicator(LFunction function, int line) : base(function, line, line) { }

        public override void AddStatement(Statement statement) { }

        public override bool IsContainer() => false;

        public override bool IsUnprotected() => false;

        public override bool Breakable() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
            => @out.Write("-- Unhandled boolean indicator...");
    }
}
