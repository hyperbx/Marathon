using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public abstract class Operation(int in_line)
    {
        public int Line => in_line;

        public abstract Statement Process(Registers in_registers, Block in_block);
    }
}
