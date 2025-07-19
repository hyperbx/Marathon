// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class VariadicArgs(int in_length, bool in_isMultiple) : Expression(Precedence.Atomic)
    {
        private readonly int _length = in_length;
        private readonly bool _isMultiple = in_isMultiple;

        public override int GetConstantIndex()
        {
            return -1;
        }

        public override void Write(Output in_output)
        {
            in_output.Write(_isMultiple ? "..." : "(...)");
        }

        public void PrintMultiple(Output in_output)
        {
            in_output.Write(_isMultiple ? "..." : "(...)");
        }

        public override bool IsMultiple()
        {
            return _isMultiple;
        }
    }
}
