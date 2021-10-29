using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class EQNode : Branch
    {
        private readonly int _left, _right;
        private readonly bool _invert;

        public EQNode(int left, int right, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            _left = left;
            _right = right;
            _invert = invert;
        }

        public override Branch Invert() => new EQNode(_left, _right, !_invert, Line, End, Begin);

        public override int GetRegister() => -1;

        public override Expression AsExpression(Registers r)
        {
            bool transpose = false;

            return new BinaryExpression
            (
                _invert ? "~=" : "==",
                r.GetConstantExpression(!transpose ? _left : _right, Line),
                r.GetConstantExpression(!transpose ? _right : _left, Line),
                Precedence.COMPARE,
                Associativity.LEFT
            );
        }

        public override void UseExpression(Expression expression) { }
    }
}
