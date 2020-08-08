using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class UpvalueSet : Operation
    {
        private UpvalueTarget target;
        private Expression value;

        public UpvalueSet(int line, string upvalue, Expression value) : base(line)
        {
            target = new UpvalueTarget(upvalue);
            this.value = value;
        }

        public override Statement process(Registers r, Block block) => new Assignment(target, value);
    }
}
