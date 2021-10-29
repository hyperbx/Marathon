using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class RepeatBlock : Block
    {
        private readonly Branch _branch;
        private readonly Registers _r;
        private readonly List<Statement> _statements;

        public RepeatBlock(LFunction function, Branch branch, Registers r) : base(function, branch.End, branch.Begin)
        {
            _branch = branch;
            _r = r;
            _statements = new List<Statement>(branch.Begin - branch.End + 1);
        }

        public override bool Breakable() => true;

        public override bool IsContainer() => true;

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            @out.Write("repeat");
            @out.WriteLine();
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("until ");

            _branch.AsExpression(_r).Write(@out);
        }
    }
}
