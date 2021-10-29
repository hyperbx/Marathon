using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class AndBranch : Branch
    {
        private readonly Branch _left, _right;

        public AndBranch(Branch left, Branch right) : base(right.Line, right.Begin, right.End)
        {
            _left = left;
            _right = right;
        }

        public override Branch Invert() => new OrBranch(_left.Invert(), _right.Invert());

        public override int GetRegister()
        {
            int rleft = _left.GetRegister(),
                rright = _right.GetRegister();

            return rleft == rright ? rleft : -1;
        }

        public override Expression AsExpression(Registers r)
            => new BinaryExpression("and", _left.AsExpression(r), _right.AsExpression(r), Precedence.AND, Associativity.NONE);

        public override void UseExpression(Expression expression)
        {
            _left.UseExpression(expression);
            _right.UseExpression(expression);
        }
    }
}
