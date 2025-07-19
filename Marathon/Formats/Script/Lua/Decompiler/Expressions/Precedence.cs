// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public enum Precedence
    {
        Or = 1,
        And,
        Compare,
        Concat,
        Add,
        Mul,
        Unary,
        Pow,
        Atomic
    }
}
