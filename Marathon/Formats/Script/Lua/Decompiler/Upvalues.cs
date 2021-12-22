using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Upvalues
    {
        private readonly LUpvalue[] _upvalues;

        public Upvalues(LUpvalue[] upvalues) => _upvalues = upvalues;

        public string GetName(int index)
        {
            if (index < _upvalues.Length && _upvalues[index].Name != null)
            {
                return _upvalues[index].Name;
            }
            else
            {
                // TODO: Set error.
                return $"v{index}";
            }
        }

        public UpvalueExpression GetExpression(int index) => new(GetName(index));
    }
}
