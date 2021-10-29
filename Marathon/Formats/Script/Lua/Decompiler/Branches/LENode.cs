using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    class LENode : Branch
    {
        private readonly int _left, _right;
        private readonly bool _invert;

        public LENode(int left, int right, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            _left = left;
            _right = right;
            _invert = invert;
        }

        public override Branch Invert() => new LENode(_left, _right, !_invert, Line, End, Begin);

        public override int GetRegister() => -1;

        public override Expression AsExpression(Registers r)
        {
            bool transpose = false;

            Expression leftExpression = r.GetConstantExpression(_left, Line),
                       rightExpression = r.GetConstantExpression(_right, Line);

            if (!leftExpression.IsConstant() && !rightExpression.IsConstant())
            {
                transpose = r.GetUpdated(_left, Line) > r.GetUpdated(_right, Line);
            }
            else
            {
                transpose = rightExpression.GetConstantIndex() < leftExpression.GetConstantIndex();
            }

            Expression result = new BinaryExpression
            (
                !transpose ? "<=" : ">=",
                !transpose ? leftExpression : rightExpression,
                !transpose ? rightExpression : leftExpression,
                Precedence.COMPARE,
                Associativity.LEFT
            );

            if (_invert)
                result = new UnaryExpression("not ", result, Precedence.UNARY);

            return result;
        }

        public override void UseExpression(Expression expression) { }
    }
}
