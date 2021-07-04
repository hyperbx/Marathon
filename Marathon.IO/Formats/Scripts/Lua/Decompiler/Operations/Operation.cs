using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public abstract class Operation
    {
        public readonly int line;

        public Operation(int line) => this.line = line;

        public abstract Statement process(Registers r, Block block);
    }
}
