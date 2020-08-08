using System;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class Break : Block
    {
        public readonly int target;

        public Break(LFunction function, int line, int target) : base(function, line, line) => this.target = target;

        public override void addStatement(Statement statement) => throw new Exception();

        public override bool isContainer() => false;

        public override bool breakable() => false;

        /// <summary>
        /// This *is* unprotected, but isn't really a block.
        /// </summary>
        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out) => @out.print("do break end");

        public void printTail(Output @out) => @out.print("break");
    }
}
