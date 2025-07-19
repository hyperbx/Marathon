using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Types;
using System;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public abstract class Expression(Precedence in_precedence)
    {
        public Precedence Precedence { get; } = in_precedence;

        public static Expression Nil => new ConstantExpression(new Constant(LNil.Nil), -1);

        public static BinaryExpression MakeConcat(Expression in_left, Expression in_right)
        {
            return new("..", in_left, in_right, Precedence.Concat, Associativity.Right);
        }

        public static BinaryExpression MakeAdd(Expression in_left, Expression in_right)
        {
            return new("+", in_left, in_right, Precedence.Add, Associativity.Left);
        }

        public static BinaryExpression MakeSub(Expression in_left, Expression in_right)
        {
            return new("-", in_left, in_right, Precedence.Add, Associativity.Left);
        }

        public static BinaryExpression MakeMul(Expression in_left, Expression in_right)
        {
            return new("*", in_left, in_right, Precedence.Mul, Associativity.Left);
        }

        public static BinaryExpression MakeDiv(Expression in_left, Expression in_right)
        {
            return new("/", in_left, in_right, Precedence.Mul, Associativity.Left);
        }

        public static BinaryExpression MakeMod(Expression in_left, Expression in_right)
        {
            return new("%", in_left, in_right, Precedence.Mul, Associativity.Left);
        }

        public static BinaryExpression MakePow(Expression in_left, Expression in_right)
        {
            return new("^", in_left, in_right, Precedence.Pow, Associativity.Right);
        }

        public static UnaryExpression MakeUnm(Expression in_expression)
        {
            return new("-", in_expression, Precedence.Unary);
        }

        public static UnaryExpression MakeNot(Expression in_expression)
        {
            return new("not ", in_expression, Precedence.Unary);
        }

        public static UnaryExpression MakeLen(Expression in_expression)
        {
            return new("#", in_expression, Precedence.Unary);
        }

        /// <summary>
        /// Determines the index of the last declared constant in this expression.
        /// <para>If there is no constant in the expression, return -1.</para>
        /// </summary>
        public abstract int GetConstantIndex();

        public virtual bool BeginsWithParen()
        {
            return false;
        }

        public virtual bool IsNil()
        {
            return false;
        }

        public virtual bool IsClosure()
        {
            return false;
        }

        public virtual bool IsConstant()
        {
            return false;
        }

        public virtual bool IsUpvalueOf(int in_register)
        {
            // Only supported for closures.
            throw new NotSupportedException();
        }

        public virtual bool IsBoolean()
        {
            return false;
        }

        public virtual bool IsInteger()
        {
            return false;
        }

        public virtual int AsInteger()
        {
            throw new NotSupportedException();
        }

        public virtual bool IsString()
        {
            return false;
        }

        public virtual bool IsIdentifier()
        {
            return false;
        }

        /// <summary>
        /// Determines if this can be part of a function name.
        /// </summary>
        public virtual bool IsDotChain()
        {
            return false;
        }

        public virtual int ClosureUpvalueLine()
        {
            throw new NotSupportedException();
        }

        public virtual void WriteClosure(Output in_output, Target in_name)
        {
            throw new NotSupportedException();
        }

        public virtual string AsName()
        {
            throw new NotSupportedException();
        }

        public virtual bool IsTableLiteral()
        {
            return false;
        }

        public virtual void AddEntry(TableEntry in_entry)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines if the expression has more than one return value stored into registers.
        /// </summary>
        public virtual bool IsMultiple()
        {
            return false;
        }

        public virtual bool IsMemberAccess()
        {
            return false;
        }

        public virtual Expression GetTable()
        {
            throw new NotSupportedException();
        }

        public virtual string GetField()
        {
            throw new NotSupportedException();
        }

        public virtual bool IsBrief()
        {
            return false;
        }

        /// <summary>
        /// Prints out a sequences of expressions with commas and optionally handling multiple expressions and return value adjustment.
        /// </summary>
        public static void WriteSequence(Output in_output, List<Expression> in_expressions, bool in_lineBreak, bool in_isMultiple)
        {
            var n = in_expressions.Count;
            var i = 1;

            foreach (var expression in in_expressions)
            {
                var isLast = i == n;

                if (expression.IsMultiple())
                    isLast = true;

                if (isLast)
                {
                    if (in_isMultiple)
                    {
                        expression.WriteMultiple(in_output);
                    }
                    else
                    {
                        expression.Write(in_output);
                    }

                    break;
                }
                else
                {
                    expression.Write(in_output);

                    in_output.Write(",");

                    if (in_lineBreak)
                    {
                        in_output.WriteLine();
                    }
                    else
                    {
                        in_output.Write(" ");
                    }
                }

                i++;
            }
        }

        protected static void WriteUnary(Output in_output, string in_operator, Expression in_expression)
        {
            in_output.Write(in_operator);
            in_expression.Write(in_output);
        }

        protected static void WriteBinary(Output in_output, string in_operator, Expression in_left, Expression in_right)
        {
            in_left.Write(in_output);
            in_output.Write(" ");
            in_output.Write(in_operator);
            in_output.Write(" ");
            in_right.Write(in_output);
        }

        public abstract void Write(Output in_output);

        /// <summary>
        /// Prints the expression in a context that accepts multiple values.
        /// <para>If an expression that normally could return multiple values doesn't, it should use parens to adjust to 1.</para>
        /// </summary>
        public void WriteMultiple(Output in_output)
        {
            Write(in_output);
        }
    }
}
