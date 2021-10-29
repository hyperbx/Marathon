using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TestNode : Branch
    {
        public readonly int Test;
        public readonly bool _Invert;

        public TestNode(int test, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            Test = test;
            _Invert = invert;
            IsTest = true;
        }

        public override Branch Invert() => new TestNode(Test, !_Invert, Line, End, Begin);

        public override int GetRegister() => Test;

        public override Expression AsExpression(Registers r)
        {
            if (_Invert)
            {
                return new NotBranch(Invert()).AsExpression(r);
            }
            else
            {
                return r.GetExpression(Test, Line);
            }
        }

        public override void UseExpression(Expression expression) { }

        public override string ToString() => $"TestNode[test={Test};invert={_Invert};line={Line};begin={Begin};end={End}]";
    }
}
