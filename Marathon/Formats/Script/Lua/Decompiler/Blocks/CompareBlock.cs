using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class CompareBlock : Block
    {
        public int Target;
        public Branch Branch;

        public CompareBlock(LFunction function, int begin, int end, int target, Branch branch) : base(function, begin, end)
        {
            Target = target;
            Branch = branch;
        }

        public override bool IsContainer() => false;

        public override bool Breakable() => false;

        public override void AddStatement(Statement statement) { }

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
            => @out.Write("-- Unhandled compare assign...");

        public override Operation Process(Decompiler d) => new OperationAnonymousInnerClass(this);

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly CompareBlock _outerInstance;

            public OperationAnonymousInnerClass(CompareBlock outerInstance) : base(outerInstance.End - 1)
                => _outerInstance = outerInstance;

            public override Statement Process(Registers r, Block block)
                => new RegisterSet(_outerInstance.End - 1, _outerInstance.Target, _outerInstance.Branch.AsExpression(r)).Process(r, block);
        }
    }
}
