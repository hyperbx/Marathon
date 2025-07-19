using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class UpvalueTarget(string in_name) : Target
    {
        public override void Write(Output in_output)
        {
            in_output.Write(in_name);
        }

        public override void WriteMethod(Output in_output)
        {
            throw new NotSupportedException();
        }
    }
}
