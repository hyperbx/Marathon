using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class UpvalueSet(int in_line, string in_upvalue, Expression in_value) : Operation(in_line)
    {
        private readonly UpvalueTarget _target = new UpvalueTarget(in_upvalue);

        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new Assignment(_target, in_value);
        }
    }
}
