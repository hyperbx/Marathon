using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements
{
    public class FunctionCallStatement : Statement
    {
        private FunctionCall call;

        public FunctionCallStatement(FunctionCall call) => this.call = call;

        public override void print(Output @out) => call.print(@out);

        public bool beginsWithParen() => call.beginsWithParen();
    }
}
