namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class GlobalExpression(string in_name, int in_index) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex()
        {
            return in_index;
        }

        public override bool IsDotChain()
        {
            return true;
        }

        public override bool IsBrief()
        {
            return true;
        }

        public override void Write(Output in_output)
        {
            in_output.Write(in_name);
        }
    }
}
