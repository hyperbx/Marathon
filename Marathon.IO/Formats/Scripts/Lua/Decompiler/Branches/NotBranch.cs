using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class NotBranch : Branch
    {
        private readonly Branch branch;

        public NotBranch(Branch branch) : base(branch.line, branch.begin, branch.end) => this.branch = branch;

        public override Branch invert() => branch;

        public override int getRegister() => branch.getRegister();

        public override Expression asExpression(Registers r) => new UnaryExpression("not ", branch.asExpression(r), Expression.PRECEDENCE_UNARY);

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }
    }
}
