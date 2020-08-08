using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class IfThenEndBlock : Block
    {
        private readonly Branch branch;
        private readonly Helpers.Stack<Branch> stack;
        private readonly Registers r;
        private readonly List<Statement> statements;

        public IfThenEndBlock(LFunction function, Branch branch, Registers r) : this(function, branch, null, r) { }

        public IfThenEndBlock(LFunction function, Branch branch, Helpers.Stack<Branch> stack, Registers r) :
            base(function, branch.begin == branch.end ? branch.begin - 1 : branch.begin, branch.begin == branch.end ? branch.begin - 1 : branch.end)
        {
            this.branch = branch;
            this.stack = stack;
            this.r = r;

            statements = new List<Statement>(branch.end - branch.begin + 1);
        }

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool breakable() => false;

        public override bool isContainer() => true;

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            @out.print("if ");

            branch.asExpression(r).print(@out);

            @out.print(" then");
            @out.println();
            @out.indent();

            printSequence(@out, statements);

            @out.dedent();
            @out.print("end");
        }

        public override Operation process(Decompiler d)
        {
            if (statements.Count == 1)
            {
                Statement stmt = statements[0];

                if (stmt is Assignment)
                {
                    Assignment assign = (Assignment)stmt;

                    if (assign.getArity() == 1)
                    {
                        if (branch is TestNode)
                        {
                            TestNode node = (TestNode)branch;
                            Declaration decl = r.getDeclaration(node.test, node.line);

                            if (assign.getFirstTarget().isDeclaration(decl))
                            {
                                Expression expr;

                                if (node._invert)
                                    expr = new BinaryExpression("or", new LocalVariable(decl), assign.getFirstValue(), Expression.PRECEDENCE_OR, Expression.ASSOCIATIVITY_NONE);

                                else
                                    expr = new BinaryExpression("and", new LocalVariable(decl), assign.getFirstValue(), Expression.PRECEDENCE_AND, Expression.ASSOCIATIVITY_NONE);

                                return new OperationAnonymousInnerClass(this, assign, expr);
                            }
                        }
                    }
                }
            }
            else if (statements.Count == 0 && stack != null)
            {
                int test = branch.getRegister();

                if (test < 0)
                {
                    for (int reg = 0; reg < r.registers; reg++)
                    {
                        if (r.getUpdated(reg, branch.end - 1) >= branch.begin)
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
                    if (r.getUpdated(test, branch.end - 1) >= branch.begin)
                    {
                        Expression right = r.getValue(test, branch.end);

                        Branch setb = d.popSetCondition(stack, stack.peek().end);
                        setb.useExpression(right);

                        int testreg = test;

                        return new OperationAnonymousInnerClass2(this, setb, testreg);
                    }
                }
            }

            return base.process(d);
        }

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly IfThenEndBlock outerInstance;
            private Assignment assign;
            private Expression expr;

            public OperationAnonymousInnerClass(IfThenEndBlock outerInstance, Assignment assign, Expression expr) : base(outerInstance.end - 1)
            {
                this.outerInstance = outerInstance;
                this.assign = assign;
                this.expr = expr;
            }

            public override Statement process(Registers r, Block block) => new Assignment(assign.getFirstTarget(), expr);
        }

        private class OperationAnonymousInnerClass2 : Operation
        {
            private readonly IfThenEndBlock outerInstance;
            private Branch setb;
            private int testreg;

            public OperationAnonymousInnerClass2(IfThenEndBlock outerInstance, Branch setb, int testreg) : base(outerInstance.end - 1)
            {
                this.outerInstance = outerInstance;
                this.setb = setb;
                this.testreg = testreg;
            }

            public override Statement process(Registers r, Block block)
            {
                r.setValue(testreg, outerInstance.branch.end - 1, setb.asExpression(r));
                return null;
            }
        }
    }
}
