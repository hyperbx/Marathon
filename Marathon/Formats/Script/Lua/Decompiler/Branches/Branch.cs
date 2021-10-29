using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Branches
{
    public abstract class Branch
    {
        public readonly int Line;

        public int Begin,
                   End,
                   SetTarget = -1;

        public bool IsSet = false,
                    IsCompareSet = false,
                    IsTest = false;

        public Branch(int line, int begin, int end)
        {
            Line = line;
            Begin = begin;
            End = end;
        }

        public abstract Branch Invert();

        public abstract int GetRegister();

        public abstract Expression AsExpression(Registers r);

        public abstract void UseExpression(Expression expression);
    }
}
