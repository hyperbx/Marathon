using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches
{
    public abstract class Branch
    {
        public readonly int line;
        public int begin,
                   end, // Might be modified to undo redirect.
                   setTarget = -1;

        public bool isSet = false,
                    isCompareSet = false,
                    isTest = false;

        public Branch(int line, int begin, int end)
        {
            this.line = line;
            this.begin = begin;
            this.end = end;
        }

        public abstract Branch invert();

        public abstract int getRegister();

        public abstract Expression asExpression(Registers r);

        public abstract void useExpression(Expression expression);
    }
}
