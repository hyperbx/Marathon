using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using System;
using System.Collections.Generic;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Registers
    {
        private readonly Declaration[,] _declarations;
        private readonly Function _function;
        private readonly Expression[,] _values;
        private readonly int[,] _updated;
        private bool[] _startedLines;

        public int RegisterCount { get; }

        public int Length { get; }

        public Registers(int in_registerCount, int in_length, Declaration[] in_declarations, Function in_function)
        {
            RegisterCount = in_registerCount;
            Length = in_length;

            _declarations = new Declaration[in_registerCount, in_length + 1];

            for (int i = 0; i < in_declarations.Length; i++)
            {
                var declaration = in_declarations[i];
                var register = 0;

                while (_declarations[register, declaration.Begin] != null)
                    register++;

                declaration.Register = register;

                for (int line = declaration.Begin; line <= declaration.End; line++)
                    _declarations[register, line] = declaration;
            }

            _values = new Expression[in_registerCount, in_length + 1];

            for (int register = 0; register < in_registerCount; register++)
                _values[register, 0] = Expression.Nil;

            _updated = new int[in_registerCount, in_length + 1];
            _startedLines = new bool[in_length + 1];

            Array.Fill(_startedLines, false);

            _function = in_function;
        }

        public bool IsAssignable(int in_register, int in_line)
        {
            return IsLocal(in_register, in_line) && !_declarations[in_register, in_line].IsForLoop;
        }

        public bool IsLocal(int in_register, int in_line)
        {
            if (in_register < 0)
                return false;

            return _declarations[in_register, in_line] != null;
        }

        public bool IsNewLocal(int in_register, int in_line)
        {
            var declaration = _declarations[in_register, in_line];

            return declaration != null && declaration.Begin == in_line && !declaration.IsForLoop;
        }

        public List<Declaration> GetNewLocals(int in_line)
        {
            var locals = new List<Declaration>(RegisterCount);

            for (int register = 0; register < RegisterCount; register++)
            {
                if (IsNewLocal(register, in_line))
                    locals.Add(GetDeclaration(register, in_line));
            }

            return locals;
        }

        public Declaration GetDeclaration(int in_register, int in_line)
        {
            return _declarations[in_register, in_line];
        }

        public void StartLine(int in_line)
        {
            _startedLines[in_line] = true;

            for (int register = 0; register < RegisterCount; register++)
            {
                _values[register, in_line] = _values[register, in_line - 1];
                _updated[register, in_line] = _updated[register, in_line - 1];
            }
        }

        public Expression GetExpression(int in_register, int in_line)
        {
            if (IsLocal(in_register, in_line - 1))
            {
                return new LocalVariable(GetDeclaration(in_register, in_line - 1));
            }
            else
            {
                return _values[in_register, in_line - 1];
            }
        }

        public Expression GetConstantExpression(int in_register, int in_line)
        {
            if (_function.IsConstant(in_register))
            {
                return _function.GetConstantExpression(_function.ConstantIndex(in_register));
            }
            else
            {
                return GetExpression(in_register, in_line);
            }
        }

        public Expression GetValue(int in_register, int in_line)
        {
            return _values[in_register, in_line - 1];
        }

        public int GetUpdated(int in_register, int in_line)
        {
            return _updated[in_register, in_line];
        }

        public void SetValue(int in_register, int in_line, Expression in_expression)
        {
            _values[in_register, in_line] = in_expression;
            _updated[in_register, in_line] = in_line;
        }

        public Target GetTarget(int in_register, int in_line)
        {
            if (!IsLocal(in_register, in_line))
                _declarations[in_register, in_line] = new Declaration("i", 0, 0);

            return new VariableTarget(_declarations[in_register, in_line]);
        }

        public void SetInternalLoopVariable(int in_register, int in_begin, int in_end)
        {
            var declaration = GetDeclaration(in_register, in_begin);

            if (declaration == null)
            {
                declaration = new("i", in_begin, in_end)
                {
                    Register = in_register
                };

                NewDeclaration(declaration, in_register, in_begin, in_end);
            }

            declaration.IsForLoop = true;
        }

        public void SetExplicitLoopVariable(int in_register, int in_begin, int in_end)
        {
            var declaration = GetDeclaration(in_register, in_begin);

            if (declaration == null)
            {
                declaration = new($"v{in_register}", in_begin, in_end)
                {
                    Register = in_register
                };

                NewDeclaration(declaration, in_register, in_begin, in_end);
            }

            declaration.IsForLoopExplicit = true;
        }

        private void NewDeclaration(Declaration in_declaration, int in_register, int in_begin, int in_end)
        {
            for (int line = in_begin; line <= in_end; line++)
                _declarations[in_register, line] = in_declaration;
        }
    }
}
