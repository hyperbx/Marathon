using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class GlobalSet(int in_line, string in_global, Expression in_value) : Operation(in_line)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new Assignment(new GlobalTarget(in_global), in_value);
        }
    }
}
