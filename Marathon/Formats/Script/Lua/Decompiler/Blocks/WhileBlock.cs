using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class WhileBlock : Block
    {
        private readonly Branch _branch;
        private readonly int _loopback;
        private readonly Registers _r;
        private readonly List<Statement> _statements;

        public WhileBlock(LFunction function, Branch branch, int loopback, Registers r) : base(function, branch.Begin, branch.End)
        {
            _branch = branch;
            _loopback = loopback;
            _r = r;
            _statements = new List<Statement>(branch.End - branch.Begin + 1);
        }

        public override int ScopeEnd() => End - 2;

        public override bool Breakable() => true;

        public override bool IsContainer() => true;

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool IsUnprotected() => true;

        public override int GetLoopback() => _loopback;

        public override void Write(Output @out)
        {
            @out.Write("while ");

            _branch.AsExpression(_r).Write(@out);

            @out.Write(" do");
            @out.WriteLine();
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("end");
        }
    }
}
