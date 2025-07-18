using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class EQNode(int in_left, int in_right, bool in_invert, int in_line, int in_begin, int in_end) : Branch(in_line, in_begin, in_end)
    {
        public override Branch Invert()
        {
            return new EQNode(in_left, in_right, !in_invert, Line, End, Begin);
        }

        public override int GetRegister()
        {
            return -1;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            var transpose = false;

            return new BinaryExpression
            (
                in_invert ? "~=" : "==",
                in_registers.GetConstantExpression(!transpose ? in_left : in_right, Line),
                in_registers.GetConstantExpression(!transpose ? in_right : in_left, Line),
                Precedence.Compare,
                Associativity.Left
            );
        }

        public override void UseExpression(Expression in_expression) { }
    }
}
