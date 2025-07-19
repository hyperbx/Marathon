using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class RegisterSet(int in_line, int in_register, Expression in_value) : Operation(in_line)
    {
        public int Register => in_register;

        public Expression Value => in_value;

        public override Statement Process(Registers in_registers, Block in_block)
        {
            in_registers.SetValue(Register, Line, Value);

            if (!in_registers.IsAssignable(Register, Line))
                return null;

            return new Assignment(in_registers.GetTarget(Register, Line), Value);
        }
    }
}
