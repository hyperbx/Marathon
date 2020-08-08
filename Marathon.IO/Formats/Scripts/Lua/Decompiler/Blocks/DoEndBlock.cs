using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class DoEndBlock : Block
    {
        private readonly List<Statement> statements;

        public DoEndBlock(LFunction function, int begin, int end) : base(function, begin, end) => statements = new List<Statement>();

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool breakable() => false;

        public override bool isContainer() => true;

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            @out.println("do");
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("end");
        }
    }
}
