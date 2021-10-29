using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class AlwaysLoop : Block
    {
        private readonly List<Statement> _statements;

        public AlwaysLoop(LFunction function, int begin, int end) : base(function, begin, end) => _statements = new List<Statement>();

        public override int ScopeEnd() => End - 2;

        public override bool Breakable() => true;

        public override bool IsContainer() => true;

        public override bool IsUnprotected() => true;

        public override int GetLoopback() => Begin;

        public override void Write(Output @out)
        {
            @out.WriteLine("while true do");
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("end");
        }

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);
    }
}
