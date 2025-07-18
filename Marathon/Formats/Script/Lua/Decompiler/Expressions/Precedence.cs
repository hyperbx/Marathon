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
