using Marathon.IO.Formats.Scripts.Lua.Helpers;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Code
    {
        private readonly ICodeExtract extractor;
        private readonly OpcodeMap map;
        private readonly int[] code;

        public Code(LFunction function)
        {
            code = function.code;
            map = function.header.version.getOpcodeMap();
            extractor = function.header.extractor;
        }

        public virtual Op.Opcode op(int line) => map.get(code[line - 1] & 0x0000003F);

        public int A(int line) => extractor.extract_A(code[line - 1]);

        public int C(int line) => extractor.extract_C(code[line - 1]);

        public int B(int line) => extractor.extract_B(code[line - 1]);

        public int Bx(int line) => extractor.extract_Bx(code[line - 1]);

        public int sBx(int line) => extractor.extract_sBx(code[line - 1]);

        public int codepoint(int line) => code[line - 1];
    }

    public class Code50 : ICodeExtract
    {
        private int shiftA,
                    shiftC,
                    shiftB,
                    shiftBx,
                    maskOp,
                    maskA,
                    maskB,
                    maskBx,
                    maskC,
                    excessK;

        public Code50(int sizeOp, int sizeA, int sizeB, int sizeC)
        {
            shiftA = sizeB + sizeC + sizeOp;
            shiftC = sizeOp;
            shiftB = sizeC + sizeOp;
            shiftBx = sizeOp;
            
            maskOp = (1 << sizeOp) - 1;
            maskA = (1 << sizeA) - 1;
            maskC = (1 << sizeC) - 1;
            maskB = (1 << sizeB) - 1;
            maskBx = (1 << (sizeB + sizeC)) - 1;

            excessK = maskBx / 2;
        }

        public int extract_A(int codepoint) => (codepoint >> shiftA) & maskA;

        public int extract_C(int codepoint) => (codepoint >> shiftC) & maskC;

        public int extract_B(int codepoint) => (codepoint >> shiftB) & maskB;

        public int extract_Bx(int codepoint) => (codepoint >> shiftBx) & maskBx;

        public int extract_sBx(int codepoint) => ((codepoint >> shiftBx) & maskBx) - excessK;

        public int extract_op(int codepoint) => codepoint & maskOp;
    }

    public class Code51 : ICodeExtract
    {
        public int extract_A(int codepoint) => (codepoint >> 6) & 0x0000000FF;

        public int extract_C(int codepoint) => (codepoint >> 14) & 0x000001FF;

        public int extract_B(int codepoint) => (int)((uint)codepoint >> 23);

        public int extract_Bx(int codepoint) => (int)((uint)codepoint >> 14);

        public int extract_sBx(int codepoint) => (int)((uint)codepoint >> 14) - 131071;

        public int extract_op(int codepoint) => codepoint & 0x0000003F;
    }
}
