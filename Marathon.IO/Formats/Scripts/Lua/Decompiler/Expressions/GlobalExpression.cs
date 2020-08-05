namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class GlobalExpression : Expression
    {
        private readonly string name;
        private readonly int index;

        public GlobalExpression(string name, int index) : base(PRECEDENCE_ATOMIC)
        {
            this.name = name;
            this.index = index;
        }

        public override int getConstantIndex() => index;

        public bool isDotChain() => true;

        public override void print(Output @out) => @out.print(name);

        public bool isBrief() => true;
    }
}
