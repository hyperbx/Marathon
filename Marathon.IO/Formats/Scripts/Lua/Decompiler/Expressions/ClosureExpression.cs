using Marathon.IO.Formats.Scripts.Lua.Helpers;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class ClosureExpression : Expression
    {
        private readonly LFunction function;
        private int upvalueLine;
        private Declaration[] declList;

        public ClosureExpression(LFunction function, Declaration[] declList, int upvalueLine) : base(PRECEDENCE_ATOMIC)
        {
            this.function = function;
            this.upvalueLine = upvalueLine;
            this.declList = declList;
        }

        public override int getConstantIndex() => -1;

        public bool isClosure() => true;

        public bool isUpvalueOf(int register)
        {
            for (int i = 0; i < function.upvalues.Length; i++)
            {
                LUpvalue upvalue = function.upvalues[i];

                if (upvalue.instack && upvalue.idx == register)
                    return true;
            }

            return false;
        }

        public int closureUpvalueLine() => upvalueLine;

        public override void print(Output @out)
        {
            Decompiler d = new Decompiler(function);

            @out.print("function");

            printMain(@out, d, true);
        }

        public void printClosure(Output @out, Target name)
        {
            Decompiler d = new Decompiler(function);

            @out.print("function ");

            if (function.numParams >= 1 && d.declList[0].name.equals("self") && name.GetType().Equals(typeof(TableTarget)))
            {
                name.printMethod(@out);
                printMain(@out, d, false);
            }
            else
            {
                name.print(@out);
                printMain(@out, d, true);
            }
        }

        private void printMain(Output @out, Decompiler d, bool includeFirst)
        {
            @out.print("(");

            int start = includeFirst ? 0 : 1;

            if (function.numParams > start)
            {
                new VariableTarget(d.declList[start]).print(@out);

                for (int i = start + 1; i < function.numParams; i++)
                {
                    @out.print(", ");

                    new VariableTarget(d.declList[i]).print(@out);
                }
            }

            if ((function.vararg & 1) == 1)
            {
                if (function.numParams > start)
                    @out.print(", ...");

                else
                    @out.print("...");
            }

            @out.print(")");
            @out.println();
            @out.indent();

            d.decompile();
            d.print(@out);

            @out.dedent();
            @out.print("end");
        }
    }
}
