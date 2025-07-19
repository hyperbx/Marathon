using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public class TestNode : Branch
    {
        public int Register { get; }

        public bool IsInverted { get; }

        public TestNode(int in_testRegister, bool in_isInverted, int in_line, int in_begin, int in_end) : base(in_line, in_begin, in_end)
        {
            Register = in_testRegister;
            IsInverted = in_isInverted;
            IsTest = true;
        }

        public override Branch Invert()
        {
            return new TestNode(Register, !IsInverted, Line, End, Begin);
        }

        public override int GetRegister()
        {
            return Register;
        }

        public override Expression AsExpression(Registers in_registers)
        {
            if (IsInverted)
                return new NotBranch(Invert()).AsExpression(in_registers);

            return in_registers.GetExpression(Register, Line);
        }

        public override void UseExpression(Expression in_expression) { }

        public override string ToString()
        {
            return $"TestNode[test={Register};invert={IsInverted};line={Line};begin={Begin};end={End}]";
        }
    }
}
