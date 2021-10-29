using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class DoEndBlock : Block
    {
        private readonly List<Statement> _statements;

        public DoEndBlock(LFunction function, int begin, int end) : base(function, begin, end) => _statements = new List<Statement>(end - begin + 1);

        public override void AddStatement(Statement statement) => _statements.Add(statement);

        public override bool Breakable() => false;

        public override bool IsContainer() => true;

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            @out.WriteLine("do");
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("end");
        }
    }
}
