using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public abstract class Expression
    {
        public static readonly int PRECEDENCE_OR = 1;
        public static readonly int PRECEDENCE_AND = 2;
        public static readonly int PRECEDENCE_COMPARE = 3;
        public static readonly int PRECEDENCE_CONCAT = 4;
        public static readonly int PRECEDENCE_ADD = 5;
        public static readonly int PRECEDENCE_MUL = 6;
        public static readonly int PRECEDENCE_UNARY = 7;
        public static readonly int PRECEDENCE_POW = 8;
        public static readonly int PRECEDENCE_ATOMIC = 9;

        public static readonly int ASSOCIATIVITY_NONE = 0;
        public static readonly int ASSOCIATIVITY_LEFT = 1;
        public static readonly int ASSOCIATIVITY_RIGHT = 2;

        public static readonly Expression NIL = new ConstantExpression(new Constant(LNil.NIL), -1);

        public static BinaryExpression makeCONCAT(Expression left, Expression right)
            => new BinaryExpression("..", left, right, PRECEDENCE_CONCAT, ASSOCIATIVITY_RIGHT);

        public static BinaryExpression makeADD(Expression left, Expression right)
            => new BinaryExpression("+", left, right, PRECEDENCE_ADD, ASSOCIATIVITY_LEFT);

        public static BinaryExpression makeSUB(Expression left, Expression right)
            => new BinaryExpression("-", left, right, PRECEDENCE_ADD, ASSOCIATIVITY_LEFT);

        public static BinaryExpression makeMUL(Expression left, Expression right)
            => new BinaryExpression("*", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);

        public static BinaryExpression makeDIV(Expression left, Expression right)
            => new BinaryExpression("/", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);

        public static BinaryExpression makeMOD(Expression left, Expression right)
            => new BinaryExpression("%", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);

        public static UnaryExpression makeUNM(Expression expression)
            => new UnaryExpression("-", expression, PRECEDENCE_UNARY);

        public static UnaryExpression makeNOT(Expression expression)
            => new UnaryExpression("not ", expression, PRECEDENCE_UNARY);

        public static UnaryExpression makeLEN(Expression expression)
            => new UnaryExpression("#", expression, PRECEDENCE_UNARY);

        public static BinaryExpression makePOW(Expression left, Expression right)
            => new BinaryExpression("^", left, right, PRECEDENCE_POW, ASSOCIATIVITY_RIGHT);

        /// <summary>
        /// Prints out a sequences of expressions with commas, and optionally handling multiple expressions and return value adjustment.
        /// </summary>
        public static void printSequence(Output @out, List<Expression> exprs, bool linebreak, bool multiple)
        {
            int n = exprs.Count,
                i = 1;

            foreach (Expression expr in exprs)
            {
                bool last = i == n;

                if (expr.isMultiple())
                    last = true;

                if (last)
                {
                    if (multiple)
                        expr.printMultiple(@out);

                    else
                        expr.print(@out);

                    break;
                }
                else
                {
                    expr.print(@out);

                    @out.print(",");

                    if (linebreak)
                        @out.println();

                    else
                        @out.print(" ");
                }

                i++;
            }
        }

        public readonly int precedence;

        public Expression(int precedence) => this.precedence = precedence;

        protected static void printUnary(Output @out, string op, Expression expression)
        {
            @out.print(op);
            expression.print(@out);
        }

        protected static void printBinary(Output @out, string op, Expression left, Expression right)
        {
            left.print(@out);
            @out.print(" ");
            @out.print(op);
            @out.print(" ");
            right.print(@out);
        }

        public abstract void print(Output @out);

        /// <summary>
        /// Prints the expression in a context that accepts multiple values. (Thus, if an expression that normally could return multiple values doesn't, it should use parens to adjust to 1).
        /// </summary>
        public void printMultiple(Output @out) => print(@out);

        /// <summary>
        /// Determines the index of the last-declared constant in this expression. If there is no constant in the expression, return -1.
        /// </summary>
        public abstract int getConstantIndex();

        public bool beginsWithParen() => false;

        public bool isNil() => false;

        public bool isClosure() => false;

        public bool isConstant() => false;

        /// <summary>
        /// Only supported for closures.
        /// </summary>
        public bool isUpvalueOf(int register) => throw new Exception();

        public bool isBoolean() => false;

        public bool isInteger() => false;

        public int asInteger() => throw new Exception();

        public bool isString() => false;

        public bool isIdentifier() => false;

        /// <summary>
        /// Determines if this can be part of a function name. Is it of the form: {Name. } Name
        /// </summary>
        public bool isDotChain() => false;

        public int closureUpvalueLine() => throw new Exception();

        public void printClosure(Output @out, Target name) => throw new Exception();

        public string asName() => throw new Exception();

        public bool isTableLiteral() => false;

        public void addEntry(TableLiteral.Entry entry) => throw new Exception();

        /// <summary>
        /// Whether the expression has more than one return stored into registers.
        /// </summary>
        public bool isMultiple() => false;

        public bool isMemberAccess() => false;

        public Expression getTable() => throw new Exception();

        public string getField() => throw new Exception();

        public bool isBrief() => false;
    }
}
