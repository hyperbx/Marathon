namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class ConstantExpression : Expression
    {
        private readonly Constant constant;
        private readonly int index;

        public ConstantExpression(Constant constant, int index) : base(PRECEDENCE_ATOMIC)
        {
            this.constant = constant;
            this.index = index;
        }

        public override int getConstantIndex() => index;

        public override void print(Output @out) => constant.print(@out);

        public bool isConstant() => true;

        public bool isNil() => constant.isNil();

        public bool isBoolean() => constant.isBoolean();

        public bool isInteger() => constant.isInteger();

        public int asInteger() => constant.asInteger();

        public bool isString() => constant.isString();

        public bool isIdentifier() => constant.isIdentifier();

        public string asName() => constant.asName();

        public bool isBrief() => !constant.isString() || constant.asName().Length <= 10;
    }
}
