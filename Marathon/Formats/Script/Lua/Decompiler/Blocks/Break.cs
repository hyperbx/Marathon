using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class Break : Block
    {
        public readonly int Target;

        public Break(LFunction function, int line, int target) : base(function, line, line) => Target = target;

        public override void AddStatement(Statement statement)
            => throw new Exception();

        public override bool IsContainer() => false;

        public override bool Breakable() => false;

        /// <summary>
        /// This *is* unprotected, but isn't really a block.
        /// </summary>
        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
            => @out.Write("do break end");

        public override void WriteTail(Output @out)
            => @out.Write("break");
    }
}
