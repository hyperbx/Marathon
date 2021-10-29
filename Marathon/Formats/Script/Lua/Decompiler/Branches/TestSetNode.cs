using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TestSetNode : Branch
    {
        public readonly int Test;
        public readonly bool _Invert;

        public TestSetNode(int target, int test, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            Test = test;
            _Invert = invert;
            SetTarget = target;
        }

        public override Branch Invert() => new TestSetNode(SetTarget, Test, !_Invert, Line, End, Begin);

        public override int GetRegister() => SetTarget;

        public override Expression AsExpression(Registers r) => r.GetExpression(Test, Line);

        public override void UseExpression(Expression expression) { }

        public override string ToString() => $"TestSetNode[target={SetTarget};test={Test};invert={_Invert};line={Line};begin={Begin};end={End}]";
    }
}
