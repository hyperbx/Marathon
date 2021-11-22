namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class FunctionCall : Expression
    {
        private readonly Expression _function;
        private readonly Expression[] _arguments;
        private readonly bool _multiple;

        public FunctionCall(Expression function, Expression[] arguments, bool multiple) : base(Precedence.ATOMIC)
        {
            _function = function;
            _arguments = arguments;
            _multiple = multiple;
        }

        public override int GetConstantIndex()
        {
            int index = _function.GetConstantIndex();

            foreach (Expression argument in _arguments)
                index = Math.Max(argument.GetConstantIndex(), index);

            return index;
        }

        public override bool IsMultiple() => _multiple;

        public void PrintMultiple(Output @out)
        {
            if (!_multiple)
                @out.Write("(");

            Write(@out);

            if (!_multiple)
                @out.Write(")");
        }

        private bool IsMethodCall() => _function.IsMemberAccess() && _arguments.Length > 0 && _function.GetTable() == _arguments[0];

        public override bool BeginsWithParent()
        {
            if (IsMethodCall())
            {
                Expression obj = _function.GetTable();

                return obj.IsClosure() || obj.IsConstant() || obj.BeginsWithParent();
            }
            else
            {
                return _function.IsClosure() || _function.IsConstant() || _function.BeginsWithParent();
            }
        }

        public override void Write(Output @out)
        {
            List<Expression> args = new(_arguments.Length);

            if (IsMethodCall())
            {
                Expression obj = _function.GetTable();

                if (obj.IsClosure() || obj.IsConstant())
                {
                    @out.Write("(");
                    obj.Write(@out);
                    @out.Write(")");
                }
                else
                {
                    obj.Write(@out);
                }

                @out.Write(":");
                @out.Write(_function.GetField());

                for (int i = 1; i < _arguments.Length; i++)
                    args.Add(_arguments[i]);
            }
            else
            {
                if (_function.IsClosure() || _function.IsConstant())
                {
                    @out.Write("(");
                    _function.Write(@out);
                    @out.Write(")");
                }
                else
                {
                    _function.Write(@out);
                }

                for (int i = 0; i < _arguments.Length; i++)
                    args.Add(_arguments[i]);
            }

            @out.Write("(");

            WriteSequence(@out, args, false, true);

            @out.Write(")");
        }
    }
}
