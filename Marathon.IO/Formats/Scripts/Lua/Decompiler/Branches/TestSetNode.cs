using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class TestSetNode : Branch
    {
        public readonly int test;
        public readonly bool _invert;

        public TestSetNode(int target, int test, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            this.test = test;
            _invert = invert;
            setTarget = target;
        }

        public override Branch invert() => new TestSetNode(setTarget, test, !_invert, line, end, begin);

        public override int getRegister() => setTarget;

        public override Expression asExpression(Registers r) => r.getExpression(test, line);

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }

        public override string ToString() => $"TestSetNode[target={setTarget};test={test};invert={_invert};line={line};begin={begin};end={end}]";
    }
}
