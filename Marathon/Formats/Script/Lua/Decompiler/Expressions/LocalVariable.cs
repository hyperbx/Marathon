namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class LocalVariable : Expression
    {
        private readonly Declaration _decl;

        public LocalVariable(Declaration decl) : base(Precedence.ATOMIC) => _decl = decl;

        public override int GetConstantIndex() => -1;

        public override bool IsDotChain() => true;

        public override void Write(Output @out)
            => @out.Write(_decl.Name);

        public override bool IsBrief() => true;
    }
}
