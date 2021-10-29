using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Targets;

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class ClosureExpression : Expression
    {
        private readonly LFunction _function;
        private int _upvalueLine;

        public ClosureExpression(LFunction function, int upvalueLine) : base(Precedence.ATOMIC)
        {
            _function = function;
            _upvalueLine = upvalueLine;
        }

        public override int GetConstantIndex() => -1;

        public override bool IsClosure() => true;

        public override bool IsUpvalueOf(int register)
        {
            for (int i = 0; i < _function.Upvalues.Length; i++)
            {
                LUpvalue upvalue = _function.Upvalues[i];

                if (upvalue.InStack && upvalue.Index == register)
                    return true;
            }

            return false;
        }

        public override int ClosureUpvalueLine() => _upvalueLine;

        public override void Write(Output @out)
        {
            Decompiler d = new(_function);

            @out.Write("function");

            PrintMain(@out, d, true);
        }

        public override void PrintClosure(Output @out, Target name)
        {
            Decompiler d = new(_function);

            @out.Write("function ");

            if (_function.NumParams >= 1 && d.DeclarationList[0].Name.Equals("self") && name is TableTarget)
            {
                name.WriteMethod(@out);
                PrintMain(@out, d, false);
            }
            else
            {
                name.Write(@out);
                PrintMain(@out, d, true);
            }
        }

        private void PrintMain(Output @out, Decompiler d, bool includeFirst)
        {
            @out.Write("(");

            int start = includeFirst ? 0 : 1;

            if (_function.NumParams > start)
            {
                new VariableTarget(d.DeclarationList[start]).Write(@out);

                for (int i = start + 1; i < _function.NumParams; i++)
                {
                    @out.Write(", ");

                    new VariableTarget(d.DeclarationList[i]).Write(@out);
                }
            }

            if ((_function.Vararg & 1) == 1)
            {
                if (_function.NumParams > start)
                {
                    @out.Write(", ...");
                }
                else
                {
                    @out.Write("...");
                }
            }

            @out.Write(")");
            @out.WriteLine();
            @out.Indent();

            d.Decompile();
            d.Write(@out);

            @out.Dedent();
            @out.Write("end");
        }
    }
}
