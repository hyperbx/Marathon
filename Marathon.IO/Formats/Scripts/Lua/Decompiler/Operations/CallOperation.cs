using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class CallOperation : Operation
    {
        private FunctionCall call;

        public CallOperation(int line, FunctionCall call) : base(line) => this.call = call;

        public override Statement process(Registers r, Block block) => new FunctionCallStatement(call);
    }
}
