using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class GlobalSet : Operation
    {
        private string _global;
        private Expression _value;

        public GlobalSet(int line, string global, Expression value) : base(line)
        {
            _global = global;
            _value = value;
        }

        public override Statement Process(Registers r, Block block) => new Assignment(new GlobalTarget(_global), _value);
    }
}
