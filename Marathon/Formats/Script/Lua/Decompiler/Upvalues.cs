using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Upvalues(LUpvalue[] in_upvalues)
    {
        public string GetName(int in_index)
        {
            if (in_index < in_upvalues.Length && in_upvalues[in_index].Name != null)
            {
                return in_upvalues[in_index].Name;
            }
            else
            {
                return SymbolResolver.ResolveUpvalueSymbol(in_index);
            }
        }

        public UpvalueExpression GetExpression(int in_index)
        {
            return new(GetName(in_index));
        }
    }
}
