namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class BinaryExpression : Expression
    {
        private readonly string _op;
        private readonly Expression _left, _right;
        private readonly Associativity _associativity;

        public BinaryExpression(string op, Expression left, Expression right, Precedence precedence, Associativity associativity) : base(precedence)
        {
            _op = op;
            _left = left;
            _right = right;
            _associativity = associativity;
        }

        public override int GetConstantIndex() => Math.Max(_left.GetConstantIndex(), _right.GetConstantIndex());

        public override bool BeginsWithParent() => LeftGroup() || _left.BeginsWithParent();

        public override void Write(Output @out)
        {
            bool leftGroup = LeftGroup();
            bool rightGroup = RightGroup();

            if (leftGroup)
                @out.Write("(");

            _left.Write(@out);

            if (leftGroup)
                @out.Write(")");

            @out.Write(" ");
            @out.Write(_op);
            @out.Write(" ");

            if (rightGroup)
                @out.Write("(");

            _right.Write(@out);

            if (rightGroup)
                @out.Write(")");
        }

        private bool LeftGroup()
            => Precedence > _left.Precedence || (Precedence == _left.Precedence && _associativity == Associativity.RIGHT);

        private bool RightGroup()
            => Precedence > _right.Precedence || (Precedence == _right.Precedence && _associativity == Associativity.LEFT);
    }
}
