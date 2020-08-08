using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class OuterBlock : Block
    {
        private readonly List<Statement> statements;

        public OuterBlock(LFunction function, int length) : base(function, 0, length + 1) => statements = new List<Statement>(length);

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool breakable() => false;

        public override bool isContainer() => true;

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public int scopeEnd() => end - 1 + function.header.version.getOuterBlockScopeAdjustment();

        public override void print(Output @out)
        {
            if (statements.Count != 0)
            {
                // Extra return statement.
                int last = statements.Count - 1;

                if (last < 0 || !(statements[last] is Return))
                    throw new Exception(statements[last].ToString());

                statements.RemoveAt(last);
            }

            printSequence(@out, statements);
        }
    }
}
