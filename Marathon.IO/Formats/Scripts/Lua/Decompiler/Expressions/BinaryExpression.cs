using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class BinaryExpression : Expression
    {
        private readonly string op;
        private readonly Expression left, right;
        private readonly int associativity;

        public BinaryExpression(string op, Expression left, Expression right, int precedence, int associativity) : base(precedence)
        {
            this.op = op;
            this.left = left;
            this.right = right;
            this.associativity = associativity;
        }

        public override int getConstantIndex() => Math.Max(left.getConstantIndex(), right.getConstantIndex());

        public bool beginsWithParen() => leftGroup() || left.beginsWithParen();

        public override void print(Output @out)
        {
            bool _leftGroup = leftGroup();
            bool _rightGroup = rightGroup();

            if (_leftGroup)
                @out.print("(");

            left.print(@out);

            if (_leftGroup)
                @out.print(")");

            @out.print(" ");
            @out.print(op);
            @out.print(" ");

            if (_rightGroup)
                @out.print("(");

            right.print(@out);

            if (_rightGroup)
                @out.print(")");
        }

        private bool leftGroup()
            => precedence > left.precedence || (precedence == left.precedence && associativity == ASSOCIATIVITY_RIGHT);

        private bool rightGroup()
            => precedence > right.precedence || (precedence == right.precedence && associativity == ASSOCIATIVITY_LEFT);
    }
}
