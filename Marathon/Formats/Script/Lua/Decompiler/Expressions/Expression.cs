using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Targets;

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public abstract class Expression
    {
        public readonly Precedence Precedence;
        public static readonly Expression NIL = new ConstantExpression(new Constant(LNil.NIL), -1);

        public static BinaryExpression MakeCONCAT(Expression left, Expression right)
            => new BinaryExpression("..", left, right, Precedence.CONCAT, Associativity.RIGHT);

        public static BinaryExpression MakeADD(Expression left, Expression right)
            => new BinaryExpression("+", left, right, Precedence.ADD, Associativity.LEFT);

        public static BinaryExpression MakeSUB(Expression left, Expression right)
            => new BinaryExpression("-", left, right, Precedence.ADD, Associativity.LEFT);

        public static BinaryExpression MakeMUL(Expression left, Expression right)
            => new BinaryExpression("*", left, right, Precedence.MUL, Associativity.LEFT);

        public static BinaryExpression MakeDIV(Expression left, Expression right)
            => new BinaryExpression("/", left, right, Precedence.MUL, Associativity.LEFT);

        public static BinaryExpression MakeMOD(Expression left, Expression right)
            => new BinaryExpression("%", left, right, Precedence.MUL, Associativity.LEFT);

        public static UnaryExpression MakeUNM(Expression expression)
            => new UnaryExpression("-", expression, Precedence.UNARY);

        public static UnaryExpression MakeNOT(Expression expression)
            => new UnaryExpression("not ", expression, Precedence.UNARY);

        public static UnaryExpression MakeLEN(Expression expression)
            => new UnaryExpression("#", expression, Precedence.UNARY);

        public static BinaryExpression MakePOW(Expression left, Expression right)
            => new BinaryExpression("^", left, right, Precedence.POW, Associativity.RIGHT);

        /// <summary>
        /// Prints out a sequences of expressions with commas, and optionally handling multiple expressions and return value adjustment.
        /// </summary>
        public static void WriteSequence(Output @out, List<Expression> exprs, bool lineBreak, bool multiple)
        {
            int n = exprs.Count,
                i = 1;

            foreach (Expression expr in exprs)
            {
                bool last = i == n;

                if (expr.IsMultiple())
                    last = true;

                if (last)
                {
                    if (multiple)
                    {
                        expr.WriteMultiple(@out);
                    }
                    else
                    {
                        expr.Write(@out);
                    }

                    break;
                }
                else
                {
                    expr.Write(@out);

                    @out.Write(",");

                    if (lineBreak)
                    {
                        @out.WriteLine();
                    }
                    else
                    {
                        @out.Write(" ");
                    }
                }

                i++;
            }
        }

        public Expression(Precedence precedence) => Precedence = precedence;

        protected static void WriteUnary(Output @out, string op, Expression expression)
        {
            @out.Write(op);
            expression.Write(@out);
        }

        protected static void WriteBinary(Output @out, string op, Expression left, Expression right)
        {
            left.Write(@out);
            @out.Write(" ");
            @out.Write(op);
            @out.Write(" ");
            right.Write(@out);
        }

        public abstract void Write(Output @out);

        /// <summary>
        /// Prints the expression in a context that accepts multiple values.
        /// Thus, if an expression that normally could return multiple values doesn't, it should use parens to adjust to 1.
        /// </summary>
        public void WriteMultiple(Output @out)
            => Write(@out);

        /// <summary>
        /// Determines the index of the last-declared constant in this expression. If there is no constant in the expression, return -1.
        /// </summary>
        public abstract int GetConstantIndex();

        public virtual bool BeginsWithParent() => false;

        public virtual bool IsNil() => false;

        public virtual bool IsClosure() => false;

        public virtual bool IsConstant() => false;

        /// <summary>
        /// Only supported for closures.
        /// </summary>
        public virtual bool IsUpvalueOf(int register) => throw new Exception();

        public virtual bool IsBoolean() => false;

        public virtual bool IsInteger() => false;

        public virtual int AsInteger() => throw new Exception();

        public virtual bool IsString() => false;

        public virtual bool IsIdentifier() => false;

        /// <summary>
        /// Determines if this can be part of a function name. Is it of the form: {Name. } Name
        /// </summary>
        public virtual bool IsDotChain() => false;

        public virtual int ClosureUpvalueLine() => throw new Exception();

        public virtual void PrintClosure(Output @out, Target name)
            => throw new Exception();

        public virtual string AsName() => throw new Exception();

        public virtual bool IsTableLiteral() => false;

        public virtual void AddEntry(TableLiteral.Entry entry)
            => throw new Exception();

        /// <summary>
        /// Whether the expression has more than one return stored into registers.
        /// </summary>
        public virtual bool IsMultiple() => false;

        public virtual bool IsMemberAccess() => false;

        public virtual Expression GetTable() => throw new Exception();

        public virtual string GetField() => throw new Exception();

        public virtual bool IsBrief() => false;
    }
}
