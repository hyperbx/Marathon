using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Assignment : Statement
    {
        private readonly List<Target> _targets = new(5);
        private readonly List<Expression> _values = new(5);

        private bool _isAllNil = true;
        private bool _isDeclared = false;

        private int _declareStart = 0;

        public Assignment() { }

        public Target GetFirstTarget()
        {
            return _targets[0];
        }

        public Expression GetFirstValue()
        {
            return _values[0];
        }

        public bool AssignsTarget(Declaration in_declaration)
        {
            foreach (var target in _targets)
            {
                if (target.IsDeclaration(in_declaration))
                    return true;
            }

            return false;
        }

        public int GetArity()
        {
            return _targets.Count;
        }

        public Assignment(Target in_target, Expression in_value)
        {
            _targets.Add(in_target);
            _values.Add(in_value);

            _isAllNil = _isAllNil && in_value.IsNil();
        }

        public void AddFirst(Target in_target, Expression in_value)
        {
            _targets.Insert(0, in_target);
            _values.Insert(0, in_value);

            _isAllNil = _isAllNil && in_value.IsNil();
        }

        public void AddLast(Target in_target, Expression in_value)
        {
            if (_targets.Contains(in_target))
            {
                var index = _targets.IndexOf(in_target);

                _targets.RemoveAt(index);

                in_value = _values[index];
            }

            _targets.Add(in_target);
            _values.Add(in_value);

            _isAllNil = _isAllNil && in_value.IsNil();
        }

        public bool AssignListEquals(List<Declaration> in_declarations)
        {
            if (in_declarations.Count != _targets.Count)
                return false;

            foreach (var target in _targets)
            {
                bool found = false;

                foreach (Declaration declaration in in_declarations)
                {
                    if (target.IsDeclaration(declaration))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public void Declare(int in_declareStart)
        {
            _isDeclared = true;
            _declareStart = in_declareStart;
        }

        public override void Write(Output in_output)
        {
            if (_targets.Count != 0)
            {
                if (_isDeclared)
                    in_output.Write("local ");

                var hasFunctionSugar = false;

                if (_targets.Count == 1 && _values.Count == 1 && _values[0].IsClosure() && _targets[0].IsFunctionName())
                {
                    var closure = _values[0];

                    if (!_isDeclared || _declareStart >= closure.ClosureUpvalueLine())
                        hasFunctionSugar = true;

                    if (_targets[0].IsLocal() && closure.IsUpvalueOf(_targets[0].GetIndex()))
                        hasFunctionSugar = true;
                }

                if (hasFunctionSugar)
                {
                    _values[0].WriteClosure(in_output, _targets[0]);
                }
                else
                {
                    _targets[0].Write(in_output);

                    for (int i = 1; i < _targets.Count; i++)
                    {
                        in_output.Write(", ");
                        _targets[i].Write(in_output);
                    }

                    if (!_isDeclared || !_isAllNil)
                    {
                        in_output.Write(" = ");
                        SymbolResolver.PushScope(_targets[0].ToString());
                        Expression.WriteSequence(in_output, _values, false, false);
                        SymbolResolver.PopScope();
                    }
                }

                if (Comment == null)
                    return;

                in_output.Write(" -- ");
                in_output.Write(Comment);
            }
        }
    }
}
