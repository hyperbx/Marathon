using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TestSetNode : Branch
    {
        public int Register { get; }

        public bool IsInverted { get; }

        public TestSetNode(int in_target, int in_testRegister, bool in_isInverted, int in_line, int in_begin, int in_end) : base(in_line, in_begin, in_end)
        {
            Register = in_testRegister;
            IsInverted = in_isInverted;
            SetTarget = in_target;
        }

        public override Branch Invert()
        {
            return new TestSetNode(SetTarget, Register, !IsInverted, Line, End, Begin);
        }

        public override int GetRegister()
        {
            return SetTarget;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            return in_registers.GetExpression(Register, Line);
        }

        public override void UseExpression(Expression in_expression) { }

        public override string ToString()
        {
            return $"TestSetNode[target={SetTarget};test={Register};invert={IsInverted};line={Line};begin={Begin};end={End}]";
        }
    }
}
