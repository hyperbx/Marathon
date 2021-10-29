using System;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class AssignNode : Branch
    {
        private Expression _expression;

        public AssignNode(int line, int begin, int end) : base(line, begin, end) { }

        public override Branch Invert() => throw new Exception();

        public override int GetRegister() => throw new Exception();

        public override Expression AsExpression(Registers r) => _expression;

        public override void UseExpression(Expression expression)
            => _expression = expression;
    }
}
