using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class AlwaysLoop : Block
    {
        private readonly List<Statement> statements;

        public AlwaysLoop(LFunction function, int begin, int end) : base(function, begin, end) => statements = new List<Statement>();

        public int scopeEnd() => end - 2;

        public override bool breakable() => true;

        public override bool isContainer() => true;

        public override bool isUnprotected() => true;

        public override int getLoopback() => begin;

        public override void print(Output @out)
        {
            @out.println("while true do");
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("end");
        }

        public override void addStatement(Statement statement) => statements.Add(statement);
    }
}
