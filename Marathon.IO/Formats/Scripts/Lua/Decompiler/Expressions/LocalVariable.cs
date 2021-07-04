namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class LocalVariable : Expression
    {
        private readonly Declaration decl;

        public LocalVariable(Declaration decl) : base(PRECEDENCE_ATOMIC) => this.decl = decl;

        public override int getConstantIndex() => -1;

        public bool isDotChain() => true;

        public override void print(Output @out) => @out.print(decl.name);

        public bool isBrief() => true;
    }
}
