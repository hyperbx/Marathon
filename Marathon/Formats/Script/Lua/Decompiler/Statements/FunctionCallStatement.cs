using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class FunctionCallStatement : Statement
    {
        private FunctionCall _call;

        public FunctionCallStatement(FunctionCall call) => _call = call;

        public override void Write(Output @out)
            => _call.Write(@out);

        public override bool BeginsWithParent() => _call.BeginsWithParent();
    }
}
