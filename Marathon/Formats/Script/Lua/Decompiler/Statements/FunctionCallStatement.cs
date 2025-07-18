using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class FunctionCallStatement(FunctionCall in_call) : Statement
    {
        public override void Write(Output in_output)
        {
            in_call.Write(in_output);
        }

        public override bool BeginsWithParen()
        {
            return in_call.BeginsWithParen();
        }
    }
}
