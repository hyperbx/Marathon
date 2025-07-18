using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Version;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Function
    {
        private readonly Constant[] _constants;
        private readonly int _constantsOffset;

        public Function(LFunction in_function)
        {
            _constants = new Constant[in_function.Constants.Length];

            for (int i = 0; i < _constants.Length; i++)
                _constants[i] = new(in_function.Constants[i]);

            if (in_function.Header.Version.Version == LuaVersion.Lua50)
            {
                _constantsOffset = 250;
            }
            else
            {
                _constantsOffset = 256;
            }
        }

        public bool IsConstant(int in_register)
        {
            return in_register >= _constantsOffset;
        }

        public int ConstantIndex(int in_register)
        {
            return in_register - _constantsOffset;
        }

        public string GetGlobalName(int in_index)
        {
            return _constants[in_index].AsName();
        }

        public ConstantExpression GetConstantExpression(int in_index)
        {
            return new(_constants[in_index], in_index);
        }

        public GlobalExpression GetGlobalExpression(int in_index)
        {
            return new(GetGlobalName(in_index), in_index);
        }
    }
}
