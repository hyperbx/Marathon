using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Types;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class SetBlock : Block
    {
        private Assignment _assignment;
        private Registers _registers;
        private bool _isEmpty;
        private bool _finalize = false;

        public int Target { get; }

        public Branch Branch { get; }

        public SetBlock(LFunction in_function, Branch in_branch, int in_target, int in_line, int in_begin, int in_end, bool in_isEmpty, Registers in_registers)
            : base(in_function, in_begin, in_end)
        {
            _registers = in_registers;
            _isEmpty = in_isEmpty;

            Target = in_target;
            Branch = in_branch;

            if (in_begin == in_end)
                Begin -= 1;
        }

        public override void AddStatement(Statement in_statement)
        {
            if (!_finalize && in_statement is Assignment out_assignment)
            {
                _assignment = out_assignment;
            }
            else if (in_statement is BooleanIndicator)
            {
                _finalize = true;
            }
        }

        public override bool IsUnprotected()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            if (_assignment != null && _assignment.GetFirstTarget() != null)
            {
                var assignment = new Assignment(_assignment.GetFirstTarget(), GetValue());

                assignment.Write(in_output);
            }
            else
            {
                in_output.Write("-- WARNING: unhandled set block!");
                in_output.WriteLine();
            }
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsContainer()
        {
            return false;
        }

        public void UseAssignment(Assignment in_assignment)
        {
            _assignment = in_assignment;

            Branch.UseExpression(in_assignment.GetFirstValue());
        }

        public Expression GetValue()
        {
            return Branch.AsExpression(_registers);
        }

        public override Operation Process(Decompiler in_decompiler)
        {
            if (_isEmpty)
            {
                var expression = _registers.GetExpression(Branch.SetTarget, End);

                Branch.UseExpression(expression);

                return new RegisterSet(End - 1, Branch.SetTarget, Branch.AsExpression(_registers));
            }
            else if (_assignment != null)
            {
                Branch.UseExpression(_assignment.GetFirstValue());

                return new SetBlockAssignmentOperation(this, _assignment.GetFirstTarget(), GetValue());
            }

            return new SetBlockOperation(this, in_decompiler);
        }
    }

    file class SetBlockAssignmentOperation(SetBlock in_outerInstance, Target in_target, Expression in_value) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new Assignment(in_target, in_value);
        }
    }

    file class SetBlockOperation(SetBlock in_outerInstance, Decompiler in_decompiler) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            Expression expression = null;

            for (int i = 0; i < in_registers.RegisterCount; i++)
            {
                if (in_registers.GetUpdated(i, in_outerInstance.Branch.End - 1) == in_outerInstance.Branch.End - 1)
                {
                    expression = in_registers.GetValue(i, in_outerInstance.Branch.End);
                    break;
                }
            }

            if (in_decompiler.Code.Op(in_outerInstance.Branch.End - 2) == Opcode.LOADBOOL && in_decompiler.Code.C(in_outerInstance.Branch.End - 2) != 0)
            {
                var target = in_decompiler.Code.A(in_outerInstance.Branch.End - 2);

                if (in_decompiler.Code.Op(in_outerInstance.Branch.End - 3) == Opcode.JMP && in_decompiler.Code.sBx(in_outerInstance.Branch.End - 3) == 2)
                {
                    expression = in_registers.GetValue(target, in_outerInstance.Branch.End - 2);
                }
                else
                {
                    expression = in_registers.GetValue(target, in_outerInstance.Branch.Begin);
                }

                in_outerInstance.Branch.UseExpression(expression);

                if (in_registers.IsLocal(target, in_outerInstance.Branch.End - 1))
                    return new Assignment(in_registers.GetTarget(target, in_outerInstance.Branch.End - 1), in_outerInstance.Branch.AsExpression(in_registers));

                in_registers.SetValue(target, in_outerInstance.Branch.End - 1, in_outerInstance.Branch.AsExpression(in_registers));
            }
            else if (expression != null && in_outerInstance.Target >= 0)
            {
                in_outerInstance.Branch.UseExpression(expression);

                if (in_registers.IsLocal(in_outerInstance.Target, in_outerInstance.Branch.End - 1))
                    return new Assignment(in_registers.GetTarget(in_outerInstance.Target, in_outerInstance.Branch.End - 1), in_outerInstance.Branch.AsExpression(in_registers));

                in_registers.SetValue(in_outerInstance.Target, in_outerInstance.Branch.End - 1, in_outerInstance.Branch.AsExpression(in_registers));
            }
            else
            {
                throw new Exception($"Fail {in_outerInstance.Branch.End - 1}: {expression} (target: {in_outerInstance.Target})");
            }

            return null;
        }
    }
}
