namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class ConstantExpression : Expression
    {
        private readonly Constant _constant;
        private readonly int _index;

        public ConstantExpression(Constant constant, int index) : base(Precedence.ATOMIC)
        {
            _constant = constant;
            _index = index;
        }

        public override int GetConstantIndex() => _index;

        public override void Write(Output @out)
            => _constant.Write(@out);

        public override bool IsConstant() => true;

        public override bool IsNil() => _constant.IsNil();

        public override bool IsBoolean() => _constant.IsBoolean();

        public override bool IsInteger() => _constant.IsInteger();

        public override int AsInteger() => _constant.AsInteger();

        public override bool IsString() => _constant.IsString();

        public override bool IsIdentifier() => _constant.IsIdentifier();

        public override string AsName() => _constant.AsName();

        public override bool IsBrief() => !_constant.IsString() || _constant.AsName().Length <= 10;
    }
}
