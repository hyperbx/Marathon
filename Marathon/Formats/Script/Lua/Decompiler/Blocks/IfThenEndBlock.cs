using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class IfThenEndBlock : Block
    {
        private readonly Branch _branch;
        private readonly Stack<Branch> _stack;
        private readonly Registers _r;
        private readonly List<Statement> _statements;

        public IfThenEndBlock(LFunction function, Branch branch, Registers r) : this(function, branch, null, r) { }

        public IfThenEndBlock(LFunction function, Branch branch, Stack<Branch> stack, Registers r)
            : base(function, (branch.Begin == branch.End) ? branch.Begin - 1 : branch.Begin, (branch.Begin == branch.End) ? branch.Begin - 1 : branch.End)
        {
            _branch = branch;
            _stack = stack;
            _r = r;

            _statements = new List<Statement>(branch.End - branch.Begin + 1);
        }

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool Breakable() => false;

        public override bool IsContainer() => true;

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            @out.Write("if ");

            _branch.AsExpression(_r).Write(@out);

            @out.Write(" then");
            @out.WriteLine();
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("end");
        }

        public override Operation Process(Decompiler d)
        {
            if (_statements.Count == 1)
            {
                Statement statement = _statements[0];

                if (statement is Assignment assign)
                {
                    if (assign.GetArity() == 1)
                    {
                        if (_branch is TestNode node)
                        {
                            Declaration decl = _r.GetDeclaration(node.Test, node.Line);

                            if (assign.GetFirstTarget().IsDeclaration(decl))
                            {
                                Expression expr;

                                if (node._Invert)
                                {
                                    expr = new BinaryExpression
                                    (
                                        "or",
                                        new LocalVariable(decl),
                                        assign.GetFirstValue(),
                                        Precedence.OR,
                                        Associativity.NONE
                                    );
                                }
                                else
                                {
                                    expr = new BinaryExpression
                                    (
                                        "and",
                                        new LocalVariable(decl),
                                        assign.GetFirstValue(),
                                        Precedence.AND,
                                        Associativity.NONE
                                    );
                                }

                                return new OperationAnonymousInnerClass(this, assign, expr);
                            }
                        }
                    }
                }
            }
            else if (_statements.Count == 0 && _stack != null)
            {
                int test = _branch.GetRegister();

                if (test < 0)
                {
                    for (int reg = 0; reg < _r._Registers; reg++)
                    {
                        if (_r.GetUpdated(reg, _branch.End - 1) >= _branch.Begin)
                        {
                            if (test >= 0)
                            {
                                test = -1;
                                break;
                            }

                            test = reg;
                        }
                    }
                }

                if (test >= 0)
                {
                    if (_r.GetUpdated(test, _branch.End - 1) >= _branch.Begin)
                    {
                        Expression right = _r.GetValue(test, _branch.End);

                        Branch setb = d.PopSetCondition(_stack, _stack.Peek().End);
                        setb.UseExpression(right);

                        int testreg = test;

                        return new OperationAnonymousInnerClass2(this, setb, testreg);
                    }
                }
            }

            return base.Process(d);
        }

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly IfThenEndBlock _outerInstance;
            private Assignment _assign;
            private Expression _expr;

            public OperationAnonymousInnerClass(IfThenEndBlock outerInstance, Assignment assign, Expression expr) : base(outerInstance.End - 1)
            {
                _outerInstance = outerInstance;
                _assign = assign;
                _expr = expr;
            }

            public override Statement Process(Registers r, Block block) => new Assignment(_assign.GetFirstTarget(), _expr);
        }

        private class OperationAnonymousInnerClass2 : Operation
        {
            private readonly IfThenEndBlock _outerInstance;
            private Branch _setb;
            private int _testreg;

            public OperationAnonymousInnerClass2(IfThenEndBlock outerInstance, Branch setb, int testreg) : base(outerInstance.End - 1)
            {
                _outerInstance = outerInstance;
                _setb = setb;
                _testreg = testreg;
            }

            public override Statement Process(Registers r, Block block)
            {
                r.SetValue(_testreg, _outerInstance._branch.End - 1, _setb.AsExpression(r));

                return null;
            }
        }
    }
}
