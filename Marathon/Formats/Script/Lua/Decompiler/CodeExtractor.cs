namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class CodeExtractor
    {
        private readonly int _shiftA;
        private readonly int _shiftC;
        private readonly int _shiftB;
        private readonly int _shiftBx;

        private readonly int _maskOp;
        private readonly int _maskA;
        private readonly int _maskB;
        private readonly int _maskBx;
        private readonly int _maskC;

        private readonly int _excessK;

        public CodeExtractor(int in_sizeOp, int in_sizeA, int in_sizeB, int in_sizeC)
        {
            _shiftA = in_sizeB + in_sizeC + in_sizeOp;
            _shiftB = in_sizeC + in_sizeOp;
            _shiftBx = in_sizeOp;
            _shiftC = in_sizeOp;

            _maskOp = (1 << in_sizeOp) - 1;
            _maskA = (1 << in_sizeA) - 1;
            _maskB = (1 << in_sizeB) - 1;
            _maskBx = (1 << in_sizeB + in_sizeC) - 1;
            _maskC = (1 << in_sizeC) - 1;

            _excessK = _maskBx / 2;
        }

        public int ExtractA(int in_codepoint)
        {
            return in_codepoint >> _shiftA & _maskA;
        }

        public int ExtractB(int in_codepoint)
        {
            return in_codepoint >> _shiftB & _maskB;
        }

        public int ExtractBx(int in_codepoint)
        {
            return in_codepoint >> _shiftBx & _maskBx;
        }

        public int ExtractC(int in_codepoint)
        {
            return in_codepoint >> _shiftC & _maskC;
        }

        public int ExtractsBx(int in_codepoint)
        {
            return (in_codepoint >> _shiftBx & _maskBx) - _excessK;
        }

        public int ExtractOp(int in_codepoint)
        {
            return in_codepoint & _maskOp;
        }
    }
}
