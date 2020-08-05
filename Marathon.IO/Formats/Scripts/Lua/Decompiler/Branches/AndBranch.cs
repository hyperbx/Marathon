using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class AndBranch : Branch
    {
        private readonly Branch left, right;

        public AndBranch(Branch left, Branch right) : base(right.line, right.begin, right.end)
        {
            this.left = left;
            this.right = right;
        }

        public override Branch invert() => new OrBranch(left.invert(), right.invert());

        public override int getRegister()
        {
            int rleft = left.getRegister(),
                rright = right.getRegister();

            return rleft == rright ? rleft : -1;
        }

        public override Expression asExpression(Registers r)
            => new BinaryExpression("and", left.asExpression(r), right.asExpression(r), Expression.PRECEDENCE_AND, Expression.ASSOCIATIVITY_NONE);

        public override void useExpression(Expression expression)
        {
            left.useExpression(expression);
            right.useExpression(expression);
        }
    }
}
