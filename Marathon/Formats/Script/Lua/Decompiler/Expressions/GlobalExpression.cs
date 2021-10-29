namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class GlobalExpression : Expression
    {
        private readonly string _name;
        private readonly int _index;

        public GlobalExpression(string name, int index) : base(Precedence.ATOMIC)
        {
            _name = name;
            _index = index;
        }

        public override int GetConstantIndex() => _index;

        public override bool IsDotChain() => true;

        public override void Write(Output @out)
            => @out.Write(_name);

        public override bool IsBrief() => true;
    }
}
