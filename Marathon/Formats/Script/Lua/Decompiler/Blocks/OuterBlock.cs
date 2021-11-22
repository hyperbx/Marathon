using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class OuterBlock : Block
    {
        private readonly List<Statement> _statements;

        public OuterBlock(LFunction function, int length) : base(function, 0, length + 1) => _statements = new List<Statement>(length);

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool Breakable() => false;

        public override bool IsContainer() => true;

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override int ScopeEnd() => (End - 1) + _function.Header.Version.GetOuterBlockScopeAdjustment();

        public override void Write(Output @out)
        {
            // Extra return statement.
            int last = _statements.Count - 1;

            if (last < 0 || _statements[last] is not Return)
                throw new Exception(_statements[last].ToString());

            _statements.RemoveAt(last);

            WriteSequence(@out, _statements);
        }
    }
}
