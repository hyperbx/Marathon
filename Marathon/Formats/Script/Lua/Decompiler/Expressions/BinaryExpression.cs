using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class BinaryExpression(string in_operator, Expression in_left, Expression in_right, Precedence in_precedence, Associativity in_associativity) : Expression(in_precedence)
    {
        public string Operator => in_operator;

        public Expression Left => in_left;

        public Expression Right => in_right;

        public Associativity Associativity => in_associativity;

        public override int GetConstantIndex()
        {
            return Math.Max(Left.GetConstantIndex(), Right.GetConstantIndex());
        }

        public override bool BeginsWithParen()
        {
            return IsLeftGroup() || Left.BeginsWithParen();
        }

        private bool IsLeftGroup()
        {
            return Precedence > Left.Precedence || (Precedence == Left.Precedence && Associativity == Associativity.Right);
        }

        private bool IsRightGroup()
        {
            return Precedence > Right.Precedence || (Precedence == Right.Precedence && Associativity == Associativity.Left);
        }

        public override void Write(Output in_output)
        {
            var isLeftGroup = IsLeftGroup();
            var isRightGroup = IsRightGroup();

            if (isLeftGroup)
                in_output.Write("(");

            Left.Write(in_output);

            if (isLeftGroup)
                in_output.Write(")");

            in_output.Write(" ");
            in_output.Write(Operator);
            in_output.Write(" ");

            if (isRightGroup)
                in_output.Write("(");

            Right.Write(in_output);

            if (isRightGroup)
                in_output.Write(")");
        }
    }
}
