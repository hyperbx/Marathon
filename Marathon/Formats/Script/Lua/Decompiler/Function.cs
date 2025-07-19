using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Function
    {
        private const int _constantsOffset = 250;

        private readonly Constant[] _constants;

        public Function(LFunction in_function)
        {
            _constants = new Constant[in_function.Constants.Length];

            for (int i = 0; i < _constants.Length; i++)
                _constants[i] = new(in_function.Constants[i]);
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
