using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class WhileBlock(LFunction in_function, Branch in_branch, int in_loopback, Registers in_registers) : Block(in_function, in_branch.Begin, in_branch.End)
    {
        private readonly List<Statement> _statements = new(in_branch.End - in_branch.Begin + 1);

        public override int ScopeEnd()
        {
            return End - 2;
        }

        public override bool Breakable()
        {
            return true;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
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
            in_output.Write("while ");

            in_branch.AsExpression(in_registers).Write(in_output);

            in_output.Write(" do");
            in_output.WriteLine();
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("end");
        }
    }
}
