using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class CallOperation : Operation
    {
        private FunctionCall _call;

        public CallOperation(int line, FunctionCall call) : base(line) => _call = call;

        public override Statement Process(Registers r, Block block) => new FunctionCallStatement(_call);
    }
}
