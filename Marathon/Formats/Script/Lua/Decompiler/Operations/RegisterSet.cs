using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class RegisterSet : Operation
    {
        public readonly int Register;
        public readonly Expression Value;

        public RegisterSet(int line, int register, Expression value) : base(line)
        {
            Register = register;
            Value = value;
        }

        public override Statement Process(Registers r, Block block)
        {
            r.SetValue(Register, Line, Value);

            if (r.IsAssignable(Register, Line))
            {
                return new Assignment(r.GetTarget(Register, Line), Value);
            }
            else
            {
                return null;
            }
        }
    }
}
