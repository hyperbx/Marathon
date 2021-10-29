using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class ReturnOperation : Operation
    {
        private Expression[] _values;

        public ReturnOperation(int line, Expression value) : base(line)
        {
            _values = new Expression[1];
            _values[0] = value;
        }

        public ReturnOperation(int line, Expression[] values) : base(line) => _values = values;

        public override Statement Process(Registers r, Block block) => new Return(_values);
    }
}
