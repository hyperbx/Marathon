using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class IfThenElseBlock : Block, IComparable<Block>
    {
        private readonly Branch _branch;
        private readonly int _loopback;
        private readonly Registers _r;
        private readonly List<Statement> _statements;
        private readonly bool _emptyElse;
        public ElseEndBlock Partner;

        public IfThenElseBlock(LFunction function, Branch branch, int loopback, bool emptyElse, Registers r) : base(function, branch.Begin, branch.End)
        {
            _branch = branch;
            _loopback = loopback;
            _emptyElse = emptyElse;
            _r = r;
            _statements = new List<Statement>(branch.End - branch.Begin + 1);
        }

        public new int CompareTo(Block block)
        {
            if (block == Partner)
            {
                return -1;
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

        public override int ScopeEnd() => End - 2;

        public override bool IsUnprotected() => true;

        public override int GetLoopback() => _loopback;

        public override void Write(Output @out)
        {
            @out.Write("if ");

            _branch.AsExpression(_r).Write(@out);

            @out.Write(" then");
            @out.WriteLine();
            @out.Indent();

            /* Handle the case where the "then" is empty in if-then-else.
               The jump over the else block is falsely detected as a break. */
            if (_statements.Count == 1 && _statements[0] is Break @break)
            {
                Break b = @break;

                if (b.Target == _loopback)
                {
                    @out.Dedent();
                    return;
                }
            }

            WriteSequence(@out, _statements);

            @out.Dedent();

            if (_emptyElse)
            {
                @out.WriteLine("else");
                @out.WriteLine("end");
            }
        }
    }
}
