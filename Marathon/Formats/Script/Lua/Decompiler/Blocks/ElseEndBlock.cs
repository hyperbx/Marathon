using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class ElseEndBlock : Block, IComparable<Block>
    {
        private readonly List<Statement> _statements;
        public IfThenElseBlock Partner;

        public ElseEndBlock(LFunction function, int begin, int end) : base(function, begin, end) => _statements = new List<Statement>(end - begin + 1);

        public new int CompareTo(Block block)
        {
            if (block == Partner)
            {
                return 1;
            }
            else
            {
                return base.CompareTo(block);
            }
        }

        public override bool Breakable() => false;

        public override bool IsContainer() => true;

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            if (_statements.Count == 1 && _statements[0] is IfThenEndBlock)
            {
                @out.Write("else");

                _statements[0].Write(@out);
            }
            else if (_statements.Count == 2 && _statements[0] is IfThenElseBlock && _statements[1] is ElseEndBlock)
            {
                @out.Write("else");

                _statements[0].Write(@out);
                _statements[1].Write(@out);
            }
            else
            {
                @out.Write("else");
                @out.WriteLine();
                @out.Indent();

                WriteSequence(@out, _statements);

                @out.Dedent();
                @out.Write("end");
            }
        }
    }
}
