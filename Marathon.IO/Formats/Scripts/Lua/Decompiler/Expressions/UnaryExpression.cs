namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class UnaryExpression : Expression
    {
        private readonly string op;
        private readonly Expression expression;

        public UnaryExpression(string op, Expression expression, int precedence) : base(precedence)
        {
            this.op = op;
            this.expression = expression;
        }

        public override int getConstantIndex() => expression.getConstantIndex();

        public override void print(Output @out)
        {
            @out.print(op);

            if (precedence > expression.precedence)
                @out.print("(");

            expression.print(@out);

            if (precedence > expression.precedence)
                @out.print(")");
        }
    }
}
