using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class ReturnOperation : Operation
    {
        private readonly Expression[] _values;

        public ReturnOperation(int in_line, Expression in_value) : base(in_line)
        {
            _values = new Expression[1];
            _values[0] = in_value;
        }

        public ReturnOperation(int in_line, Expression[] in_values) : base(in_line)
        {
            _values = in_values;
        }

        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new Return(_values);
        }
    }
}
