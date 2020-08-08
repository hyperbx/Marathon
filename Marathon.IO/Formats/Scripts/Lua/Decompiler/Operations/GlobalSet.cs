using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class GlobalSet : Operation
    {
        private string global;
        private Expression value;

        public GlobalSet(int line, string global, Expression value) : base(line)
        {
            this.global = global;
            this.value = value;
        }

        public override Statement process(Registers r, Block block) => new Assignment(new GlobalTarget(global), value);
    }
}
