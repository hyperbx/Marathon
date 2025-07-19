// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class UnaryExpression(string in_operator, Expression in_expression, Precedence in_precedence) : Expression(in_precedence)
    {
        public override int GetConstantIndex()
        {
            return in_expression.GetConstantIndex();
        }

        public override void Write(Output in_output)
        {
            in_output.Write(in_operator);

            if (Precedence > in_expression.Precedence)
                in_output.Write("(");

            in_expression.Write(in_output);

            if (Precedence > in_expression.Precedence)
                in_output.Write(")");
        }
    }
}
