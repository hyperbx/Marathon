using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class LTNode : Branch
    {
        private readonly int left, right;
        private readonly bool _invert;

        public LTNode(int left, int right, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            this.left = left;
            this.right = right;
            _invert = invert;
        }

        public override Branch invert() => new LTNode(left, right, !_invert, line, end, begin);

        public override int getRegister() => -1;

        public override Expression asExpression(Registers r)
        {
            bool transpose = false;
            Expression leftExpression = r.getKExpression(left, line),
                       rightExpression = r.getKExpression(right, line);

            if (!leftExpression.isConstant() && !rightExpression.isConstant())
                transpose = r.getUpdated(left, line) > r.getUpdated(right, line);

            else
                transpose = rightExpression.getConstantIndex() < leftExpression.getConstantIndex();

            string op = !transpose ? "<" : ">";
            Expression rtn = new BinaryExpression(op,
                                                  !transpose ? leftExpression : rightExpression,
                                                  !transpose ? rightExpression : leftExpression,
                                                  Expression.PRECEDENCE_COMPARE,
                                                  Expression.ASSOCIATIVITY_LEFT);

            return rtn;
        }

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }
    }
}
