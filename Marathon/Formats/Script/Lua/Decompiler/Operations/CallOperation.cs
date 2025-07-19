using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class CallOperation(int in_line, FunctionCall in_call) : Operation(in_line)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new FunctionCallStatement(in_call);
        }
    }
}
