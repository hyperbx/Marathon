namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class UnaryExpression : Expression
    {
        private readonly string _op;
        private readonly Expression _expression;

        public UnaryExpression(string op, Expression expression, Precedence precedence) : base(precedence)
        {
            _op = op;
            _expression = expression;
        }

        public override int GetConstantIndex() => _expression.GetConstantIndex();

        public override void Write(Output @out)
        {
            @out.Write(_op);

            if (Precedence > _expression.Precedence)
                @out.Write("(");

            _expression.Write(@out);

            if (Precedence > _expression.Precedence)
                @out.Write(")");
        }
    }
}
