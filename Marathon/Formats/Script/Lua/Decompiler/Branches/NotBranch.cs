using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class NotBranch : Branch
    {
        private readonly Branch _branch;

        public NotBranch(Branch branch) : base(branch.Line, branch.Begin, branch.End) => _branch = branch;

        public override Branch Invert() => _branch;

        public override int GetRegister() => _branch.GetRegister();

        public override Expression AsExpression(Registers r) => new UnaryExpression("not ", _branch.AsExpression(r), Precedence.UNARY);

        public override void UseExpression(Expression expression) { }
    }
}
