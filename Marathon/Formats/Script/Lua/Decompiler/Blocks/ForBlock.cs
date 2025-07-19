using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System.Collections.Generic;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class ForBlock(LFunction in_function, int in_begin, int in_end, int in_register, Registers in_registers) : Block(in_function, in_begin, in_end)
    {
        private readonly List<Statement> _statements = new(in_end - in_begin + 1);

        public override int ScopeEnd()
        {
            return End - 2;
        }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }

        public override bool Breakable()
        {
            return true;
        }

        public override bool IsContainer()
        {
            return true;
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
            in_output.Write("for ");

            in_registers.GetTarget(in_register, Begin - 1).Write(in_output);

            in_output.Write(" = ");

            in_registers.GetValue(in_register, Begin - 2).Write(in_output);

            in_output.Write(", ");

            in_registers.GetValue(in_register + 1, Begin - 1).Write(in_output);

            var step = in_registers.GetValue(in_register + 2, Begin - 1);

            if (!step.IsInteger() || step.AsInteger() != 1)
            {
                in_output.Write(", ");
                step.Write(in_output);
            }

            in_output.Write(" do");
            in_output.WriteLine();
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("end");
        }
    }
}
