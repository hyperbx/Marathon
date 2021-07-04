using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class ReturnOperation : Operation
    {
        private Expression[] values;

        public ReturnOperation(int line, Expression value) : base(line)
        {
            values = new Expression[1];
            values[0] = value;
        }

        public ReturnOperation(int line, Expression[] values) : base(line) => this.values = values;

        public override Statement process(Registers r, Block block) => new Return(values);
    }
}
