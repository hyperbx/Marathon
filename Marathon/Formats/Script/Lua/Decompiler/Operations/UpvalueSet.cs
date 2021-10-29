using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class UpvalueSet : Operation
    {
        private UpvalueTarget _target;
        private Expression _value;

        public UpvalueSet(int line, string upvalue, Expression value) : base(line)
        {
            _target = new UpvalueTarget(upvalue);
            _value = value;
        }

        public override Statement Process(Registers r, Block block) => new Assignment(_target, _value);
    }
}
