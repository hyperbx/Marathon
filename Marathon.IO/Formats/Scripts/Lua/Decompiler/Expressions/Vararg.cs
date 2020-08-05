namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class Vararg : Expression
    {
        public readonly int length;
        public readonly bool multiple;

        public Vararg(int length, bool multiple) : base(PRECEDENCE_ATOMIC)
        {
            this.length = length;
            this.multiple = multiple;
        }

        public override int getConstantIndex() => -1;

        public override void print(Output @out) => @out.print(multiple ? "..." : "(...)");

        public void printMultiple(Output @out) => @out.print(multiple ? "..." : "(...)");

        public bool isMultiple() => multiple;
    }
}
