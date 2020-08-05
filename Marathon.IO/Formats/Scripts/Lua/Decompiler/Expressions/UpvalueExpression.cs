namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class UpvalueExpression : Expression
    {
        private readonly string name;

        public UpvalueExpression(string name) : base(PRECEDENCE_ATOMIC) => this.name = name;

        public override int getConstantIndex() => -1;

        public bool isDotChain() => true;

        public override void print(Output @out) => @out.print(name);

        public bool isBrief() => true;
    }
}
