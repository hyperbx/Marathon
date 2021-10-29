using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Code
    {
        private readonly ICodeExtract _extractor;
        private readonly OpcodeMap _map;
        private readonly int[] _code;

        public Code(LFunction function)
        {
            _code = function.Code;
            _map = function.Header.Version.GetOpcodeMap();
            _extractor = function.Header.Extractor;
        }

        public virtual Op.Opcode Op(int line) => _map.Get(_code[line - 1] & 0x0000003F);

        public int A(int line) => _extractor.ExtractA(_code[line - 1]);

        public int B(int line) => _extractor.ExtractB(_code[line - 1]);

        public int Bx(int line) => _extractor.ExtractBx(_code[line - 1]);

        public int C(int line) => _extractor.ExtractC(_code[line - 1]);

        public int sBx(int line) => _extractor.ExtractsBx(_code[line - 1]);

        public int Codepoint(int line) => _code[line - 1];
    }

    public class Code50 : ICodeExtract
    {
        private int _shiftA,
                    _shiftC,
                    _shiftB,
                    _shiftBx,
                    _maskOp,
                    _maskA,
                    _maskB,
                    _maskBx,
                    _maskC,
                    _excessK;

        public Code50(int sizeOp, int sizeA, int sizeB, int sizeC)
        {
            _shiftA = sizeB + sizeC + sizeOp;
            _shiftB = sizeC + sizeOp;
            _shiftBx = sizeOp;
            _shiftC = sizeOp;
            
            _maskOp = (1 << sizeOp) - 1;
            _maskA = (1 << sizeA) - 1;
            _maskB = (1 << sizeB) - 1;
            _maskBx = (1 << (sizeB + sizeC)) - 1;
            _maskC = (1 << sizeC) - 1;

            _excessK = _maskBx / 2;
        }

        public int ExtractA(int codepoint) => (codepoint >> _shiftA) & _maskA;

        public int ExtractB(int codepoint) => (codepoint >> _shiftB) & _maskB;

        public int ExtractBx(int codepoint) => (codepoint >> _shiftBx) & _maskBx;

        public int ExtractC(int codepoint) => (codepoint >> _shiftC) & _maskC;

        public int ExtractsBx(int codepoint) => ((codepoint >> _shiftBx) & _maskBx) - _excessK;

        public int ExtractOp(int codepoint) => codepoint & _maskOp;
    }

    public class Code51 : ICodeExtract
    {
        public int ExtractA(int codepoint) => (codepoint >> 6) & 0x0000000FF;

        public int ExtractC(int codepoint) => (codepoint >> 14) & 0x000001FF;

        public int ExtractB(int codepoint) => (int)((uint)codepoint >> 23);

        public int ExtractBx(int codepoint) => (int)((uint)codepoint >> 14);

        public int ExtractsBx(int codepoint) => (int)((uint)codepoint >> 14) - 131071;

        public int ExtractOp(int codepoint) => codepoint & 0x0000003F;
    }
}
