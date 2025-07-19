using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class LTNode(int in_left, int in_right, bool in_invert, int in_line, int in_begin, int in_end) : Branch(in_line, in_begin, in_end)
    {
        public override Branch Invert()
        {
            return new LTNode(in_left, in_right, !in_invert, Line, End, Begin);
        }

        public override int GetRegister()
        {
            return -1;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            var transpose = false;
            var leftExpression = in_registers.GetConstantExpression(in_left, Line);
            var rightExpression = in_registers.GetConstantExpression(in_right, Line);

            if (!leftExpression.IsConstant() && !rightExpression.IsConstant())
            {
                transpose = in_registers.GetUpdated(in_left, Line) > in_registers.GetUpdated(in_right, Line);
            }
            else
            {
                transpose = rightExpression.GetConstantIndex() < leftExpression.GetConstantIndex();
            }

            return new BinaryExpression
            (
                !transpose ? "<" : ">",
                !transpose ? leftExpression : rightExpression,
                !transpose ? rightExpression : leftExpression,
                Precedence.Compare,
                Associativity.Left
            );
        }

        public override void UseExpression(Expression in_expression) { }
    }
}
