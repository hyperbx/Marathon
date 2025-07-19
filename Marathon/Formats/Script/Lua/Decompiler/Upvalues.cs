using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Upvalues(LUpvalue[] in_upvalues)
    {
        public string GetName(int index)
        {
            if (index < in_upvalues.Length && in_upvalues[index].Name != null)
            {
                return in_upvalues[index].Name;
            }
            else
            {
                // TODO: Set error.
                return $"v{index}";
            }
        }

        public UpvalueExpression GetExpression(int index)
        {
            return new(GetName(index));
        }
    }
}
