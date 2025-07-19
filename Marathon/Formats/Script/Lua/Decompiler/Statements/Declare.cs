using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Declare(List<Declaration> in_declarations) : Statement
    {
        public override void Write(Output in_output)
        {
            in_output.Write("local ");
            in_output.Write(in_declarations[0].Name);

            for (int i = 1; i < in_declarations.Count; i++)
            {
                in_output.Write(", ");
                in_output.Write(in_declarations[i].Name);
            }
        }
    }
}
