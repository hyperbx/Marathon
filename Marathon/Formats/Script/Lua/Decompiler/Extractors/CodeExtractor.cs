using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Decompiler.Extractors
{
    public class CodeExtractor(LFunction in_function)
    {
        private readonly ICodeExtractor _extractor = in_function.Header.Extractor;
        private readonly OpcodeMap _map = in_function.Header.Version.GetOpcodeMap();
        private readonly int[] _code = in_function.Code;

        public virtual Opcode Op(int in_line)
        {
            return _map.Get(_code[in_line - 1] & 0x0000003F);
        }

        public int A(int in_line)
        {
            return _extractor.ExtractA(_code[in_line - 1]);
        }

        public int B(int in_line)
        {
            return _extractor.ExtractB(_code[in_line - 1]);
        }

        public int Bx(int in_line)
        {
            return _extractor.ExtractBx(_code[in_line - 1]);
        }

        public int C(int in_line)
        {
            return _extractor.ExtractC(_code[in_line - 1]);
        }

        public int sBx(int in_line)
        {
            return _extractor.ExtractsBx(_code[in_line - 1]);
        }

        public int Codepoint(int in_line)
        {
            return _code[in_line - 1];
        }
    }
}
