using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class IfThenElseBlock : Block
    {
        private readonly Branch branch;
        private readonly int loopback;
        private readonly Registers r;
        private readonly List<Statement> statements;
        private readonly bool emptyElse;
        public ElseEndBlock partner;

        public IfThenElseBlock(LFunction function, Branch branch, int loopback, bool emptyElse, Registers r) : base(function, branch.begin, branch.end)
        {
            this.branch = branch;
            this.loopback = loopback;
            this.emptyElse = emptyElse;
            this.r = r;

            statements = new List<Statement>(branch.end - branch.begin + 1);
        }

        public int compareTo(Block block)
        {
            if (block == partner)
                return -1;

            else
                return base.compareTo(block);
        }

        public override bool breakable() => false;

        public override bool isContainer() => true;

        public override void addStatement(Statement statement) => statements.Add(statement);

        public int scopeEnd() => end - 2;

        public override bool isUnprotected() => true;

        public override int getLoopback() => loopback;

        public override void print(Output @out)
        {
            @out.print("if ");

            branch.asExpression(r).print(@out);

            @out.print(" then");
            @out.println();
            @out.indent();

            // Handle the case where the "then" is empty in if-then-else.
            // The jump over the else block is falsely detected as a break.
            if (statements.Count == 1 && statements[0] is Break)
            {
                Break b = (Break)statements[0];

                if (b.target == loopback)
                {
                    @out.dedent();
                    return;
                }
            }

            printSequence(@out, statements);

            @out.dedent();

            if (emptyElse)
            {
                @out.println("else");
                @out.println("end");
            }
        }
    }
}
