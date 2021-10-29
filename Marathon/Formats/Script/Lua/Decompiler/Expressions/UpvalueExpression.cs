namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class UpvalueExpression : Expression
    {
        private readonly string _name;

        public UpvalueExpression(string name) : base(Precedence.ATOMIC) => _name = name;

        public override int GetConstantIndex() => -1;

        public override bool IsDotChain() => true;

        public override void Write(Output @out)
            => @out.Write(_name);

        public override bool IsBrief() => true;
    }
}
