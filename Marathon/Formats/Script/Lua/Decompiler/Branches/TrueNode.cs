using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TrueNode : Branch
    {
        public readonly int Register;
        private readonly bool _invert;

        public TrueNode(int register, bool invert, int line, int begin, int end) : base(line, begin, end)
        {
            Register = register;
            _invert = invert;
            SetTarget = register;
        }

        public override Branch Invert() => new TrueNode(Register, !_invert, Line, End, Begin);

        public override int GetRegister() => Register;

        public override Expression AsExpression(Registers r) => new ConstantExpression(new Constant(_invert ? LBoolean.LTRUE : LBoolean.LFALSE), -1);

        public override void UseExpression(Expression expression) { }

        public override string ToString() => $"TrueNode[invert={_invert};line={Line};begin={Begin};end={End}]";
    }
}
