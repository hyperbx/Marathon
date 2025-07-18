using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public abstract class Branch(int in_line, int in_begin, int in_end)
    {
        public int Line => in_line;

        public int Begin { get; set; } = in_begin;

        public int End { get; set; } = in_end;

        public int SetTarget { get; set; } = -1;

        public bool IsSet { get; set; } = false;

        public bool IsCompareSet { get; set; } = false;

        public bool IsTest { get; set; } = false;

        public abstract Branch Invert();

        public abstract int GetRegister();

        public abstract Expression AsExpression(Registers in_registers);

        public abstract void UseExpression(Expression in_expression);
    }
}
