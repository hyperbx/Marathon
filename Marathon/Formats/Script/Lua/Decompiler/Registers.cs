using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Targets;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Registers
    {
        public readonly int _Registers, Length;

        private readonly Declaration[,] _decls;
        private readonly Function _f;
        private readonly Expression[,] _values;
        private readonly int[,] _updated;
        private bool[] _startedLines;

        public Registers(int registers, int length, Declaration[] declList, Function f)
        {
            _Registers = registers;
            Length = length;
            _decls = new Declaration[registers, length + 1];

            for (int i = 0; i < declList.Length; i++)
            {
                Declaration decl = declList[i];
                int register = 0;

                while (_decls[register, decl.Begin] != null)
                    register++;

                decl.Register = register;

                for (int line = decl.Begin; line <= decl.End; line++)
                    _decls[register, line] = decl;
            }

            _values = new Expression[registers, length + 1];

            for (int register = 0; register < registers; register++)
                _values[register, 0] = Expression.NIL;

            _updated = new int[registers, length + 1];
            _startedLines = new bool[length + 1];

            Array.Fill(_startedLines, false);

            _f = f;
        }

        public bool IsAssignable(int register, int line) => IsLocal(register, line) && !_decls[register, line].ForLoop;

        public bool IsLocal(int register, int line)
        {
            if (register < 0)
                return false;

            return _decls[register, line] != null;
        }

        public bool IsNewLocal(int register, int line)
        {
            Declaration decl = _decls[register, line];

            return decl != null && decl.Begin == line && !decl.ForLoop;
        }

        public List<Declaration> GetNewLocals(int line)
        {
            List<Declaration> locals = new(_Registers);

            for (int register = 0; register < _Registers; register++)
            {
                if (IsNewLocal(register, line))
                    locals.Add(GetDeclaration(register, line));
            }

            return locals;
        }

        public Declaration GetDeclaration(int register, int line) => _decls[register, line];

        public void StartLine(int line)
        {
            _startedLines[line] = true;

            for (int register = 0; register < _Registers; register++)
            {
                _values[register, line] = _values[register, line - 1];
                _updated[register, line] = _updated[register, line - 1];
            }
        }

        public Expression GetExpression(int register, int line)
        {
            if (IsLocal(register, line - 1))
            {
                return new LocalVariable(GetDeclaration(register, line - 1));
            }
            else
            {
                return _values[register, line - 1];
            }
        }

        public Expression GetConstantExpression(int register, int line)
        {
            if (_f.IsConstant(register))
            {
                return _f.GetConstantExpression(_f.ConstantIndex(register));
            }
            else
            {
                return GetExpression(register, line);
            }
        }

        public Expression GetValue(int register, int line) => _values[register, line - 1];

        public int GetUpdated(int register, int line) => _updated[register, line];

        public void SetValue(int register, int line, Expression expression)
        {
            _values[register, line] = expression;
            _updated[register, line] = line;
        }

        public Target GetTarget(int register, int line)
        {
            if (!IsLocal(register, line))
                _decls[register, line] = new Declaration("_TMP_", 0, 0);

            return new VariableTarget(_decls[register, line]);
        }

        public void SetInternalLoopVariable(int register, int begin, int end)
        {
            Declaration decl = GetDeclaration(register, begin);

            if (decl == null)
            {
                decl = new("_FOR_", begin, end);
                decl.Register = register;

                NewDeclaration(decl, register, begin, end);
            }

            decl.ForLoop = true;
        }

        public void SetExplicitLoopVariable(int register, int begin, int end)
        {
            Declaration decl = GetDeclaration(register, begin);

            if (decl == null)
            {
                decl = new($"_FORV_{register}_", begin, end);
                decl.Register = register;

                NewDeclaration(decl, register, begin, end);
            }

            decl.ForLoopExplicit = true;
        }

        private void NewDeclaration(Declaration decl, int register, int begin, int end)
        {
            for (int line = begin; line <= end; line++)
                _decls[register, line] = decl;
        }
    }
}
