using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class TestNode : Branch
    {
        public readonly int test;
        public readonly bool _invert;

        public TestNode(int test, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            this.test = test;
            _invert = invert;
            isTest = true;
        }

        public override Branch invert() => new TestNode(test, !_invert, line, end, begin);

        public override int getRegister() => test;

        public override Expression asExpression(Registers r)
        {
            if (_invert)
                return new NotBranch(invert()).asExpression(r);

            else
                return r.getExpression(test, line);
        }

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }

        public override string ToString() => $"TestNode[test={test};invert={_invert};line={line};begin={begin};end={end}]";
    }
}
