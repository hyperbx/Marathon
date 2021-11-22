using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Return : Statement
    {
        private Expression[] _values;

        public Return() => _values = Array.Empty<Expression>();

        public Return(Expression value)
        {
            _values = new Expression[1];
            _values[0] = value;
        }

        public Return(Expression[] values) => _values = values;

        public override void Write(Output @out)
        {
            @out.Write("do ");
            WriteTail(@out);
            @out.Write(" end");
        }

        public override void WriteTail(Output @out)
        {
            @out.Write("return");

            if (_values.Length > 0)
            {
                @out.Write(" ");

                List<Expression> returns = new(_values.Length);

                foreach (Expression value in _values)
                    returns.Add(value);

                Expression.WriteSequence(@out, returns, false, true);
            }
        }
    }
}
