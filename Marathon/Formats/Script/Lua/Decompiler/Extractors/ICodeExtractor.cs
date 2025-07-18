namespace Marathon.Formats.Script.Lua.Decompiler.Extractors
{
    public interface ICodeExtractor
    {
        public int ExtractA(int in_codepoint);

        public int ExtractB(int in_codepoint);

        public int ExtractBx(int in_codepoint);

        public int ExtractC(int in_codepoint);

        public int ExtractsBx(int in_codepoint);

        public int ExtractOp(int in_codepoint);
    }
}
