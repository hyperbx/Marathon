using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TrueNode : Branch
    {
        public int Register { get; }

        public bool IsInverted { get; }

        public TrueNode(int in_register, bool in_isInverted, int in_line, int in_begin, int in_end) : base(in_line, in_begin, in_end)
        {
            Register = in_register;
            IsInverted = in_isInverted;
            SetTarget = in_register;
        }

        public override Branch Invert()
        {
            return new TrueNode(Register, !IsInverted, Line, End, Begin);
        }

        public override int GetRegister()
        {
            return Register;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            return new ConstantExpression(new Constant(IsInverted ? LBoolean.True : LBoolean.False), -1);
        }

        public override void UseExpression(Expression in_expression) { }

        public override string ToString()
        {
            return $"TrueNode[invert={IsInverted};line={Line};begin={Begin};end={End}]";
        }
    }
}
