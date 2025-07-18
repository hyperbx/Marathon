using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using System.Collections.Generic;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Return : Statement
    {
        private readonly Expression[] _values;

        public Return()
        {
            _values = [];
        }

        public Return(Expression in_value)
        {
            _values = new Expression[1];
            _values[0] = in_value;
        }

        public Return(Expression[] in_values)
        {
            _values = in_values;
        }

        public override void Write(Output in_output)
        {
            in_output.Write("do ");

            WriteTail(in_output);

            in_output.Write(" end");
        }

        public override void WriteTail(Output in_output)
        {
            in_output.Write("return");

            if (_values.Length <= 0)
                return;

            in_output.Write(" ");

            var returns = new List<Expression>(_values.Length);

            foreach (var value in _values)
                returns.Add(value);

            Expression.WriteSequence(in_output, returns, false, true);
        }
    }
}
