using System;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class FunctionCall : Expression
    {
        private readonly Expression function;
        private readonly Expression[] arguments;
        private readonly bool multiple;

        public FunctionCall(Expression function, Expression[] arguments, bool multiple) : base(PRECEDENCE_ATOMIC)
        {
            this.function = function;
            this.arguments = arguments;
            this.multiple = multiple;
        }

        public override int getConstantIndex()
        {
            int index = function.getConstantIndex();

            foreach (Expression argument in arguments)
                index = Math.Max(argument.getConstantIndex(), index);

            return index;
        }

        public bool isMultiple() => multiple;

        public void printMultiple(Output @out)
        {
            if (!multiple)
                @out.print("(");

            print(@out);

            if (!multiple)
                @out.print(")");
        }

        private bool isMethodCall() => function.isMemberAccess() && arguments.Length > 0 && function.getTable() == arguments[0];

        public bool beginsWithParen()
        {
            if (isMethodCall())
            {
                Expression obj = function.getTable();

                return obj.isClosure() || obj.isConstant() || obj.beginsWithParen();
            }
            else
                return function.isClosure() || function.isConstant() || function.beginsWithParen();
        }

        public override void print(Output @out)
        {
            List<Expression> args = new List<Expression>(arguments.Length);

            if (isMethodCall())
            {
                Expression obj = function.getTable();

                if (obj.isClosure() || obj.isConstant())
                {
                    @out.print("(");
                    obj.print(@out);
                    @out.print(")");
                }
                else
                    obj.print(@out);

                @out.print(":");
                @out.print(function.getField());

                for (int i = 1; i < arguments.Length; i++)
                    args.Add(arguments[i]);
            }
            else
            {
                if (function.isClosure() || function.isConstant())
                {
                    @out.print("(");
                    function.print(@out);
                    @out.print(")");
                }
                else
                    function.print(@out);

                for (int i = 0; i < arguments.Length; i++)
                    args.Add(arguments[i]);
            }

            @out.print("(");

            printSequence(@out, args, false, true);

            @out.print(")");
        }
    }
}
