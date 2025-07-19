using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Targets;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class ClosureExpression(LFunction in_function, int in_upvalueLine) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex()
        {
            return -1;
        }

        public override bool IsClosure()
        {
            return true;
        }

        public override bool IsUpvalueOf(int in_register)
        {
            for (int i = 0; i < in_function.Upvalues.Length; i++)
            {
                var upvalue = in_function.Upvalues[i];

                if (upvalue.IsInStack && upvalue.Index == in_register)
                    return true;
            }

            return false;
        }

        public override int ClosureUpvalueLine()
        {
            return in_upvalueLine;
        }

        public override void Write(Output in_output)
        {
            var decompiler = new Decompiler(in_function);

            in_output.Write("function");

            PrintMain(in_output, decompiler, true);
        }

        public override void WriteClosure(Output in_output, Target in_name)
        {
            var decompiler = new Decompiler(in_function);

            in_output.Write("function ");

            if (in_function.ParamCount >= 1 && decompiler.DeclarationList[0].Name.Equals("self") && in_name is TableTarget)
            {
                in_name.WriteMethod(in_output);
                PrintMain(in_output, decompiler, false);
            }
            else
            {
                in_name.Write(in_output);
                PrintMain(in_output, decompiler, true);
            }
        }

        private void PrintMain(Output in_output, Decompiler in_decompiler, bool in_includeFirst)
        {
            in_output.Write("(");

            var start = in_includeFirst ? 0 : 1;

            if (in_function.ParamCount > start)
            {
                new VariableTarget(in_decompiler.DeclarationList[start]).Write(in_output);

                for (int i = start + 1; i < in_function.ParamCount; i++)
                {
                    in_output.Write(", ");

                    new VariableTarget(in_decompiler.DeclarationList[i]).Write(in_output);
                }
            }

            if ((in_function.VariadicArgs & 1) == 1)
            {
                if (in_function.ParamCount > start)
                {
                    in_output.Write(", ...");
                }
                else
                {
                    in_output.Write("...");
                }
            }

            in_output.Write(")");
            in_output.WriteLine();
            in_output.Indent();
            
            in_decompiler.Decompile();
            in_decompiler.Write(in_output);

            in_output.Dedent();
            in_output.Write("end");
        }
    }
}
