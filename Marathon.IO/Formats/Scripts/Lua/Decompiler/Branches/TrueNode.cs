using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public class TrueNode : Branch
    {
        public readonly int register;
        public readonly bool _invert;

        public TrueNode(int register, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            this.register = register;
            _invert = invert;
            setTarget = register;
        }

        public override Branch invert() => new TrueNode(register, !_invert, line, end, begin);

        public override int getRegister() => register;

        public override Expression asExpression(Registers r) => new ConstantExpression(new Constant(_invert ? LBoolean.LTRUE : LBoolean.LFALSE), -1);

        public override void useExpression(Expression expression) { /* Nothing to see here... */ }

        public override string ToString() => $"TrueNode[invert={_invert};line={line};begin={begin};end={end}]";
    }
}
