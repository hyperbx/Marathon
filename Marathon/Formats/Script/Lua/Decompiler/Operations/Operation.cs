using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public abstract class Operation
    {
        public readonly int Line;

        public Operation(int line) => Line = line;

        public abstract Statement Process(Registers r, Block block);
    }
}
