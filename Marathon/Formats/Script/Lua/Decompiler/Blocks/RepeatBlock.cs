using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Types;
using System;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class RepeatBlock(LFunction in_function, Branch in_branch, Registers in_registers) : Block(in_function, in_branch.End, in_branch.Begin)
    {
        private readonly List<Statement> _statements = new(in_branch.Begin - in_branch.End + 1);

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
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            in_output.Write("repeat");
            in_output.WriteLine();
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("until ");

            in_branch.AsExpression(in_registers).Write(in_output);
        }
    }
}
