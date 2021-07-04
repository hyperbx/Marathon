using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class RegisterSet : Operation
    {
        public readonly int register;
        public readonly Expression value;

        public RegisterSet(int line, int register, Expression value) : base(line)
        {
            this.register = register;
            this.value = value;
        }

        public override Statement process(Registers r, Block block)
        {
            r.setValue(register, line, value);

            if (r.isAssignable(register, line))
                return new Assignment(r.getTarget(register, line), value);

            else
                return null;
        }
    }
}
