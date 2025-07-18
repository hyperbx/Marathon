using System;
using System.Collections.Generic;

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class FunctionCall(Expression in_function, Expression[] in_arguments, bool in_isMultiple) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex()
        {
            var index = in_function.GetConstantIndex();

            foreach (var argument in in_arguments)
                index = Math.Max(argument.GetConstantIndex(), index);

            return index;
        }

        public override bool IsMultiple()
        {
            return in_isMultiple;
        }

        public void PrintMultiple(Output in_output)
        {
            if (!in_isMultiple)
                in_output.Write("(");

            Write(in_output);

            if (!in_isMultiple)
                in_output.Write(")");
        }

        private bool IsMethodCall()
        {
            return in_function.IsMemberAccess() && in_arguments.Length > 0 && in_function.GetTable() == in_arguments[0];
        }

        public override bool BeginsWithParen()
        {
            if (IsMethodCall())
            {
                var obj = in_function.GetTable();

                return obj.IsClosure() || obj.IsConstant() || obj.BeginsWithParen();
            }

            return in_function.IsClosure() || in_function.IsConstant() || in_function.BeginsWithParen();
        }

        public override void Write(Output in_output)
        {
            var args = new List<Expression>(in_arguments.Length);

            if (IsMethodCall())
            {
                var obj = in_function.GetTable();

                if (obj.IsClosure() || obj.IsConstant())
                {
                    in_output.Write("(");
                    obj.Write(in_output);
                    in_output.Write(")");
                }
                else
                {
                    obj.Write(in_output);
                }

                in_output.Write(":");
                in_output.Write(in_function.GetField());

                for (int i = 1; i < in_arguments.Length; i++)
                    args.Add(in_arguments[i]);
            }
            else
            {
                if (in_function.IsClosure() || in_function.IsConstant())
                {
                    in_output.Write("(");
                    in_function.Write(in_output);
                    in_output.Write(")");
                }
                else
                {
                    in_function.Write(in_output);
                }

                for (int i = 0; i < in_arguments.Length; i++)
                    args.Add(in_arguments[i]);
            }

            in_output.Write("(");

            WriteSequence(in_output, args, false, true);

            in_output.Write(")");
        }
    }
}
