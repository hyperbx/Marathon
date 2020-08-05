using System;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class AssignNode : Branch
    {
        private Expression expression;

        public AssignNode(int line, int begin, int end) : base(line, begin, end) { }

        public override Branch invert() => throw new Exception();

        public override int getRegister() => throw new Exception();

        public override Expression asExpression(Registers r) => expression;

        public override void useExpression(Expression expression) => this.expression = expression;
    }
}
