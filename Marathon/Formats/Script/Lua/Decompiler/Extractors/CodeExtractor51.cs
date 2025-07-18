namespace Marathon.Formats.Script.Lua.Decompiler.Extractors
{
    public class CodeExtractor51 : ICodeExtractor
    {
        public int ExtractA(int in_codepoint)
        {
            return in_codepoint >> 6 & 0x0000000FF;
        }

        public int ExtractB(int in_codepoint)
        {
            return (int)((uint)in_codepoint >> 23);
        }

        public int ExtractBx(int in_codepoint)
        {
            return (int)((uint)in_codepoint >> 14);
        }

        public int ExtractC(int in_codepoint)
        {
            return in_codepoint >> 14 & 0x000001FF;
        }

        public int ExtractsBx(int in_codepoint)
        {
            return (int)((uint)in_codepoint >> 14) - 131071;
        }

        public int ExtractOp(int in_codepoint)
        {
            return in_codepoint & 0x0000003F;
        }
    }
}
