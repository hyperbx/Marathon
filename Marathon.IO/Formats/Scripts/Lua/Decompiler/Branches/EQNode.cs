using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class EQNode : Branch
    {
        private readonly int left, right;
        private readonly bool _invert;

        public EQNode(int left, int right, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            this.left = left;
            this.right = right;
            _invert = invert;
        }

        public override Branch invert() => new EQNode(left, right, !_invert, line, end, begin);

        public override int getRegister() => -1;

        public override Expression asExpression(Registers r)
        {
            bool transpose = false;
            string op = _invert ? "~=" : "==";

            return new BinaryExpression(op, r.getKExpression(!transpose ? left : right, line),
                                        r.getKExpression(!transpose ? right : left, line),
                                        Expression.PRECEDENCE_COMPARE,
                                        Expression.ASSOCIATIVITY_LEFT);
        }

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }
    }
}
