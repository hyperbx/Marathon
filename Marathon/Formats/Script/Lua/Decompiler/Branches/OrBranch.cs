using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class OrBranch(Branch in_left, Branch in_right) : Branch(in_right.Line, in_right.Begin, in_right.End)
    {
        public override Branch Invert()
        {
            return new AndBranch(in_left.Invert(), in_right.Invert());
        }

        public override int GetRegister()
        {
            var leftRegister = in_left.GetRegister();
            var rightRegister = in_right.GetRegister();

            return leftRegister == rightRegister ? leftRegister : -1;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            return new BinaryExpression("or", in_left.AsExpression(in_registers), in_right.AsExpression(in_registers), Precedence.Or, Associativity.None);
        }

        public override void UseExpression(Expression in_expression)
        {
            in_left.UseExpression(in_expression);
            in_right.UseExpression(in_expression);
        }
    }
}
