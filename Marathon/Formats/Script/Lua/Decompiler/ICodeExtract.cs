namespace Marathon.Formats.Script.Lua.Decompiler
{
    public interface ICodeExtract
    {
        int ExtractA(int codepoint);

        int ExtractB(int codepoint);

        int ExtractBx(int codepoint);

        int ExtractC(int codepoint);

        int ExtractsBx(int codepoint);

        int ExtractOp(int codepoint);
    }
}
