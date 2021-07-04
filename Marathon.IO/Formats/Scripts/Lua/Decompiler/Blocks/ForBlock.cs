using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class ForBlock : Block
    {
        private readonly int register;
        private readonly Registers r;
        private readonly List<Statement> statements;

        public ForBlock(LFunction function, int begin, int end, int register, Registers r) : base(function, begin, end)
        {
            this.register = register;
            this.r = r;

            statements = new List<Statement>(end - begin + 1);
        }

        public int scopeEnd() => end - 2;

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool breakable() => true;

        public override bool isContainer() => true;

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            @out.print("for ");

            if (function.header.version == Version.LUA50)
                r.getTarget(register, begin - 1).print(@out);

            else
                r.getTarget(register + 3, begin - 1).print(@out);

            @out.print(" = ");

            if (function.header.version == Version.LUA50)
                r.getValue(register, begin - 2).print(@out);

            else
                r.getValue(register, begin - 1).print(@out);

            @out.print(", ");

            r.getValue(register + 1, begin - 1).print(@out);

            Expression step = r.getValue(register + 2, begin - 1);

            if (!step.isInteger() || step.asInteger() != 1)
            {
                @out.print(", ");
                step.print(@out);
            }

            @out.print(" do");
            @out.println();
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("end");
        }
    }
}
