using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Helpers;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Registers
    {
        public readonly int registers, length;

        private readonly Declaration[,] decls;
        private readonly Function f;
        private readonly Expression[,] values;
        private readonly int[,] updated;

        public Registers(int registers, int length, Declaration[] declList, Function f)
        {
            this.registers = registers;
            this.length = length;
            decls = new Declaration[registers, length + 1];

            for (int i = 0; i < declList.Length; i++)
            {
                Declaration decl = declList[i];
                int register = 0;

                while (decls[register, decl.begin] != null)
                    register++;

                decl.register = register;

                for (int line = decl.begin; line <= decl.end; line++)
                    decls[register, line] = decl;
            }

            values = new Expression[registers, length + 1];

            for (int register = 0; register < registers; register++)
                values[register, 0] = Expression.NIL;

            updated = new int[registers, length + 1];
            startedLines = new bool[length + 1];

            ArrayHelper.Fill(startedLines, false);

            this.f = f;
        }

        public bool isAssignable(int register, int line) => isLocal(register, line) && !decls[register, line].forLoop;

        public bool isLocal(int register, int line)
        {
            if (register < 0)
                return false;

            return decls[register, line] != null;
        }

        public bool isNewLocal(int register, int line)
        {
            Declaration decl = decls[register, line];

            return decl != null && decl.begin == line && !decl.forLoop;
        }

        public List<Declaration> getNewLocals(int line)
        {
            List<Declaration> locals = new List<Declaration>(registers);

            for (int register = 0; register < registers; register++)
                locals.Add(getDeclaration(register, line));

            return locals;
        }

        public Declaration getDeclaration(int register, int line) => decls[register, line];

        private bool[] startedLines;

        public void startLine(int line)
        {
            startedLines[line] = true;

            for (int register = 0; register < registers; register++)
            {
                values[register, line] = values[register, line - 1];
                updated[register, line] = updated[register, line - 1];
            }
        }

        public Expression getExpression(int register, int line)
        {
            if (isLocal(register, line - 1))
                return new LocalVariable(getDeclaration(register, line - 1));

            else
                return values[register, line - 1];
        }

        public Expression getKExpression(int register, int line)
        {
            if (f.isConstant(register))
                return f.getConstantExpression(f.constantIndex(register));

            else
                return values[register, line - 1];
        }

        public Expression getValue(int register, int line) => values[register, line - 1];

        public int getUpdated(int register, int line) => updated[register, line];

        public void setValue(int register, int line, Expression expression)
        {
            values[register, line] = expression;
            updated[register, line] = line;
        }

        public Target getTarget(int register, int line)
        {
            if (!isLocal(register, line))
                decls[register, line] = new Declaration("_TMP_", 0, 0);

            return new VariableTarget(decls[register, line]);
        }

        public void setInternalLoopVariable(int register, int begin, int end)
        {
            Declaration decl = getDeclaration(register, begin);

            if (decl == null)
            {
                decl = new Declaration("_FOR_", begin, end);
                decl.register = register;

                newDeclaration(decl, register, begin, end);
            }

            decl.forLoop = true;
        }

        public void setExplicitLoopVariable(int register, int begin, int end)
        {
            Declaration decl = getDeclaration(register, begin);

            if (decl == null)
            {
                decl = new Declaration($"_FORV_{register}_", begin, end);
                decl.register = register;

                newDeclaration(decl, register, begin, end);
            }

            decl.forLoopExplicit = true;
        }

        private void newDeclaration(Declaration decl, int register, int begin, int end)
        {
            for (int line = begin; line <= end; line++)
                decls[register, line] = decl;
        }
    }
}
