using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using System.Collections.Generic;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class IfThenEndBlock(LFunction in_function, Branch in_branch, Stack<Branch> in_stack, Registers in_registers)
        : Block(in_function, (in_branch.Begin == in_branch.End) ? in_branch.Begin - 1 : in_branch.Begin, (in_branch.Begin == in_branch.End) ? in_branch.Begin - 1 : in_branch.End)
    {
        private readonly List<Statement> _statements = new(in_branch.End - in_branch.Begin + 1);

        public Branch Branch { get; } = in_branch;

        public IfThenEndBlock(LFunction in_function, Branch in_branch, Registers in_registers)
            : this(in_function, in_branch, null, in_registers) { }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override bool IsUnprotected()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override Operation Process(Decompiler in_decompiler)
        {
            if (_statements.Count == 1)
            {
                var statement = _statements[0];

                if (statement is Assignment out_assignment)
                {
                    if (out_assignment.GetArity() == 1)
                    {
                        if (Branch is TestNode out_node)
                        {
                            var declaration = in_registers.GetDeclaration(out_node.Register, out_node.Line);

                            if (out_assignment.GetFirstTarget().IsDeclaration(declaration))
                            {
                                var @operator = out_node.IsInverted ? "or" : "and";
                                var left = new LocalVariable(declaration);
                                var right = out_assignment.GetFirstValue();
                                var precedence = out_node.IsInverted ? Precedence.Or : Precedence.And;
                                var associativity = Associativity.None;

                                return new IfThenEndBlockSingleStatementOperation(this, out_assignment,
                                    new BinaryExpression(@operator, left, right, precedence, associativity));
                            }
                        }
                    }
                }
            }
            else if (_statements.Count == 0 && in_stack != null)
            {
                var testRegister = Branch.GetRegister();

                if (testRegister < 0)
                {
                    for (int register = 0; register < in_registers.RegisterCount; register++)
                    {
                        if (in_registers.GetUpdated(register, Branch.End - 1) >= Branch.Begin)
                        {
                            if (testRegister >= 0)
                            {
                                testRegister = -1;
                                break;
                            }

                            testRegister = register;
                        }
                    }
                }

                if (testRegister >= 0)
                {
                    if (in_registers.GetUpdated(testRegister, Branch.End - 1) >= Branch.Begin)
                    {
                        var right = in_registers.GetValue(testRegister, Branch.End);
                        var setBranch = in_decompiler.PopSetCondition(in_stack, in_stack.Peek().End);

                        setBranch.UseExpression(right);

                        return new IfThenEndBlockNoStatementOperation(this, setBranch, testRegister);
                    }
                }
            }

            return base.Process(in_decompiler);
        }

        public override void Write(Output in_output)
        {
            in_output.Write("if ");

            Branch.AsExpression(in_registers).Write(in_output);

            in_output.Write(" then");
            in_output.WriteLine();
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("end");
        }
    }

    file class IfThenEndBlockSingleStatementOperation(IfThenEndBlock in_outerInstance, Assignment in_assignment, Expression in_expression) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new Assignment(in_assignment.GetFirstTarget(), in_expression);
        }
    }

    file class IfThenEndBlockNoStatementOperation(IfThenEndBlock in_outerInstance, Branch in_setBranch, int in_testRegister) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            in_registers.SetValue(in_testRegister, in_outerInstance.Branch.End - 1, in_setBranch.AsExpression(in_registers));
            return null;
        }
    }
}
