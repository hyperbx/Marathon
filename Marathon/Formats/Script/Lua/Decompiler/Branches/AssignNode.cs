using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class AssignNode(int in_line, int in_begin, int in_end) : Branch(in_line, in_begin, in_end)
    {
        private Expression _expression;

        public override Branch Invert()
        {
            throw new NotSupportedException();
        }

        public override int GetRegister()
        {
            throw new NotSupportedException();
        }

        public override Expression AsExpression(Registers in_registers)
        {
            return _expression;
        }

        public override void UseExpression(Expression in_expression)
        {
            _expression = in_expression;
        }
    }
}
