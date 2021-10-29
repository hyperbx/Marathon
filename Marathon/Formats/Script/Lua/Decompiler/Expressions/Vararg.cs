namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class Vararg : Expression
    {
        public readonly int Length;
        public readonly bool Multiple;

        public Vararg(int length, bool multiple) : base(Precedence.ATOMIC)
        {
            Length = length;
            Multiple = multiple;
        }

        public override int GetConstantIndex() => -1;

        public override void Write(Output @out)
            => @out.Write(Multiple ? "..." : "(...)");

        public void PrintMultiple(Output @out)
            => @out.Write(Multiple ? "..." : "(...)");

        public override bool IsMultiple() => Multiple;
    }
}
