using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Function
    {
        private Constant[] constants;
        private readonly int constantsOffset;

        public Function(LFunction function)
        {
            constants = new Constant[function.constants.Length];

            for (int i = 0; i < constants.Length; i++)
                constants[i] = new Constant(function.constants[i]);

            if (function.header.version == Version.LUA50)
                constantsOffset = 250;

            else
                constantsOffset = 256;
        }

        public bool isConstant(int register) => register >= constantsOffset;

        public int constantIndex(int register) => register - constantsOffset;

        public string getGlobalName(int constantIndex) => constants[constantIndex].asName();

        public ConstantExpression getConstantExpression(int constantIndex)
            => new ConstantExpression(constants[constantIndex], constantIndex);

        public GlobalExpression getGlobalExpression(int constantIndex)
            => new GlobalExpression(getGlobalName(constantIndex), constantIndex);
    }
}
