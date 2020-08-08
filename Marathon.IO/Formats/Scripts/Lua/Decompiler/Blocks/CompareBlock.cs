using System;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class CompareBlock : Block
    {
        public int target;
        public Branch branch;

        public CompareBlock(LFunction function, int begin, int end, int target, Branch branch) : base(function, begin, end)
        {
            this.target = target;
            this.branch = branch;
        }

        public override bool isContainer() => false;

        public override bool breakable() => false;

        public override void addStatement(Statement statement) { /* Nothing to see here... */ }

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out) => @out.print("-- unhandled compare assign");

        public override Operation process(Decompiler d) => new OperationAnonymousInnerClass(this);

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly CompareBlock outerInstance;

            public OperationAnonymousInnerClass(CompareBlock outerInstance) : base(outerInstance.end - 1) => this.outerInstance = outerInstance;

            public override Statement process(Registers r, Block block)
                => new RegisterSet(outerInstance.end - 1, outerInstance.target, outerInstance.branch.asExpression(r)).process(r, block);
        }
    }
}
