namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public enum Precedence
    {
        OR = 1,
        AND = 2,
        COMPARE = 3,
        CONCAT = 4,
        ADD = 5,
        MUL = 6,
        UNARY = 7,
        POW = 8,
        ATOMIC = 9
    }
}
