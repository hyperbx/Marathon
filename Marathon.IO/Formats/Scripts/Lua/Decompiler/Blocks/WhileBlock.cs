using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class WhileBlock : Block
    {
        private readonly Branch branch;
        private readonly int loopback;
        private readonly Registers r;
        private readonly List<Statement> statements;

        public WhileBlock(LFunction function, Branch branch, int loopback, Registers r) : base(function, branch.begin, branch.end)
        {
            this.branch = branch;
            this.loopback = loopback;
            this.r = r;

            statements = new List<Statement>(branch.end - branch.begin + 1);
        }

        public int scopeEnd() => end - 2;

        public override bool breakable() => true;

        public override bool isContainer() => true;

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool isUnprotected() => true;

        public override int getLoopback() => loopback;

        public override void print(Output @out)
        {
            @out.print("while ");

            branch.asExpression(r).print(@out);

            @out.print(" do");
            @out.println();
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("end");
        }
    }
}
