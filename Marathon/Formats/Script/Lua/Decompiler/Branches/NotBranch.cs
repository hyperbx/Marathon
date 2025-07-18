using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class NotBranch(Branch in_branch) : Branch(in_branch.Line, in_branch.Begin, in_branch.End)
    {
        public override Branch Invert()
        {
            return in_branch;
        }

        public override int GetRegister()
        {
            return in_branch.GetRegister();
        }

        public override Expression AsExpression(Registers in_registers)
        {
            return new UnaryExpression("not ", in_branch.AsExpression(in_registers), Precedence.Unary);
        }

        public override void UseExpression(Expression in_expression) { }
    }
}
