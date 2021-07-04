using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class RepeatBlock : Block
    {
        private readonly Branch branch;
        private readonly Registers r;
        private readonly List<Statement> statements;

        public RepeatBlock(LFunction function, Branch branch, Registers r) : base(function, branch.end, branch.begin)
        {
            this.branch = branch;
            this.r = r;

            statements = new List<Statement>(branch.begin - branch.end + 1);
        }

        public override bool breakable() => true;

        public override bool isContainer() => true;

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            @out.print("repeat");
            @out.println();
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("until ");

            branch.asExpression(r).print(@out);
        }
    }
}
