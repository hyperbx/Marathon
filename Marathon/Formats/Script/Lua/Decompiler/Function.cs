using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Function
    {
        private Constant[] _constants;
        private readonly int _constantsOffset;

        public Function(LFunction function)
        {
            _constants = new Constant[function.Constants.Length];

            for (int i = 0; i < _constants.Length; i++)
                _constants[i] = new Constant(function.Constants[i]);

            if (function.Header.Version == Version.LUA50)
            {
                _constantsOffset = 250;
            }
            else
            {
                _constantsOffset = 256;
            }
        }

        public bool IsConstant(int register) => register >= _constantsOffset;

        public int ConstantIndex(int register) => register - _constantsOffset;

        public string GetGlobalName(int constantIndex) => _constants[constantIndex].AsName();

        public ConstantExpression GetConstantExpression(int constantIndex) => new(_constants[constantIndex], constantIndex);

        public GlobalExpression GetGlobalExpression(int constantIndex) => new(GetGlobalName(constantIndex), constantIndex);
    }
}
