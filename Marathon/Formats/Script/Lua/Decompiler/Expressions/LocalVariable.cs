// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class LocalVariable(Declaration in_declaration) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex()
        {
            return -1;
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
            in_output.Write(in_declaration.Name);
        }
    }
}
