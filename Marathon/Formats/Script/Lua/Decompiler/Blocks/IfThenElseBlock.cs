using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class IfThenElseBlock(LFunction in_function, Branch in_branch, int in_loopback, bool in_isEmptyElse, Registers in_registers)
        : Block(in_function, in_branch.Begin, in_branch.End), IComparable<Block>
    {
        private readonly List<Statement> _statements = new(in_branch.End - in_branch.Begin + 1);

        public ElseEndBlock Partner { get; set; }

        public override int CompareTo(Block in_block)
        {
            if (in_block == Partner)
                return -1;

            return base.CompareTo(in_block);
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }

        public override int ScopeEnd()
        {
            return End - 2;
        }

        public override bool IsUnprotected()
        {
            return true;
        }

        public override int GetLoopback()
        {
            return in_loopback;
        }

        public override void Write(Output in_output)
        {
            in_output.Write("if ");

            in_branch.AsExpression(in_registers).Write(in_output);

            in_output.Write(" then");
            in_output.WriteLine();
            in_output.Indent();

            /* Handle the case where the "then" is empty in if-then-else.
               The jump over the else block is falsely detected as a break. */
            if (_statements.Count == 1 && _statements[0] is Break out_break)
            {
                if (out_break.Target == in_loopback)
                {
                    in_output.Dedent();
                    return;
                }
            }

            WriteSequence(in_output, _statements);

            in_output.Dedent();

            if (in_isEmptyElse)
            {
                // FIX (Hyper): don't write empty else block.
                // in_output.WriteLine("else");

                in_output.WriteLine("end");
            }
        }
    }
}
