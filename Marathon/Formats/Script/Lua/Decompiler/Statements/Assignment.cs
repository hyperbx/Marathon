using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Targets;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Assignment : Statement
    {
        private readonly List<Target> _targets = new(5);
        private readonly List<Expression> _values = new(5);

        private bool _allNil = true,
                     _declare = false;

        private int _declareStart = 0;

        public Assignment() { }

        public Target GetFirstTarget() => _targets[0];

        public Expression GetFirstValue() => _values[0];

        public bool AssignsTarget(Declaration decl)
        {
            foreach (Target target in _targets)
            {
                if (target.IsDeclaration(decl))
                    return true;
            }

            return false;
        }

        public int GetArity() => _targets.Count;

        public Assignment(Target target, Expression value)
        {
            _targets.Add(target);
            _values.Add(value);
            _allNil = _allNil && value.IsNil();
        }

        public void AddFirst(Target target, Expression value)
        {
            _targets.Insert(0, target);
            _values.Insert(0, value);
            _allNil = _allNil && value.IsNil();
        }

        public void AddLast(Target target, Expression value)
        {
            if (_targets.Contains(target))
            {
                int index = _targets.IndexOf(target);
                _targets.RemoveAt(index);
                value = _values[index];
            }

            _targets.Add(target);
            _values.Add(value);
            _allNil = _allNil && value.IsNil();
        }

        public bool AssignListEquals(List<Declaration> decls)
        {
            if (decls.Count != _targets.Count)
                return false;

            foreach (Target target in _targets)
            {
                bool found = false;

                foreach (Declaration decl in decls)
                {
                    if (target.IsDeclaration(decl))
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

        public void Declare(int declareStart)
        {
            _declare = true;
            _declareStart = declareStart;
        }

        public override void Write(Output @out)
        {
            if (_targets.Count != 0)
            {
                if (_declare)
                    @out.Write("local ");

                bool functionSugar = false;

                if (_targets.Count == 1 && _values.Count == 1 && _values[0].IsClosure() && _targets[0].IsFunctionName())
                {
                    Expression closure = _values[0];

                    // This check only works in Lua 5.1.
                    if (!_declare || _declareStart >= closure.ClosureUpvalueLine())
                        functionSugar = true;

                    if (_targets[0].IsLocal() && closure.IsUpvalueOf(_targets[0].GetIndex()))
                        functionSugar = true;
                }

                if (!functionSugar)
                {
                    _targets[0].Write(@out);

                    for (int i = 1; i < _targets.Count; i++)
                    {
                        @out.Write(", ");
                        _targets[i].Write(@out);
                    }

                    if (!_declare || !_allNil)
                    {
                        @out.Write(" = ");
                        Expression.WriteSequence(@out, _values, false, false);
                    }
                }
                else
                {
                    _values[0].WriteClosure(@out, _targets[0]);
                }

                if (Comment != null)
                {
                    @out.Write(" -- ");
                    @out.Write(Comment);
                }
            }
        }
    }
}
