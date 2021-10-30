using System;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class SetBlock : Block
    {
        public readonly int Target;
        private Assignment _assign;
        public readonly Branch Branch;
        private Registers _r;
        private bool _empty,
                     _finalize = false;

        public SetBlock(LFunction function, Branch branch, int target, int line, int begin, int end, bool empty, Registers r) : base(function, begin, end)
        {
            _empty = empty;

            if (begin == end)
				Begin -= 1;

            Target = target;
            Branch = branch;
            _r = r;
        }

        public override void AddStatement(Statement statement)
        {
			if (!_finalize && statement is Assignment assignment)
			{
				_assign = assignment;
			}
			else if (statement is BooleanIndicator)
			{
				_finalize = true;
			}
        }

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            if (_assign != null && _assign.GetFirstTarget() != null)
            {
                Assignment assignOut = new(_assign.GetFirstTarget(), GetValue());
                assignOut.Write(@out);
            }
            else
            {
                @out.Write("-- Unhandled set block...");
                @out.WriteLine();
            }
        }

        public override bool Breakable() => false;

        public override bool IsContainer() => false;

        public void UseAssignment(Assignment assign)
        {
            _assign = assign;

            Branch.UseExpression(assign.GetFirstValue());
        }

        public Expression GetValue() => Branch.AsExpression(_r);

        public override Operation Process(Decompiler d)
        {
			if (_empty)
			{
				Expression expression = _r.GetExpression(Branch.SetTarget, End);
				Branch.UseExpression(expression);

				return new RegisterSet(End - 1, Branch.SetTarget, Branch.AsExpression(_r));
			}
			else if (_assign != null)
			{
				Branch.UseExpression(_assign.GetFirstValue());

				Target target = _assign.GetFirstTarget();
				Expression value = GetValue();

				return new OperationAnonymousInnerClass(this, target, value);
			}

			return new OperationAnonymousInnerClass2(this, d);
        }

		private class OperationAnonymousInnerClass : Operation
		{
			private readonly SetBlock _outerInstance;
			private Target _target;
			private Expression _value;

			public OperationAnonymousInnerClass(SetBlock outerInstance, Target target, Expression value) : base(outerInstance.End - 1)
			{
				_outerInstance = outerInstance;
				_target = target;
				_value = value;
			}

			public override Statement Process(Registers r, Block block) => new Assignment(_target, _value);
		}

		private class OperationAnonymousInnerClass2 : Operation
		{
			private readonly SetBlock _outerInstance;
			private Decompiler _d;

			public OperationAnonymousInnerClass2(SetBlock outerInstance, Decompiler d) : base(outerInstance.End - 1)
			{
				_outerInstance = outerInstance;
				_d = d;
			}

			public override Statement Process(Registers r, Block block)
			{
				Expression expr = null;
				int register = 0;

				for (; register < r._Registers; register++)
				{
					if (r.GetUpdated(register, _outerInstance.Branch.End - 1) == _outerInstance.Branch.End - 1)
					{
						expr = r.GetValue(register, _outerInstance.Branch.End);
						break;
					}
				}

				if (_d.Code.Op(_outerInstance.Branch.End - 2) == Op.Opcode.LOADBOOL && _d.Code.C(_outerInstance.Branch.End - 2) != 0)
				{
					int target = _d.Code.A(_outerInstance.Branch.End - 2);

					if (_d.Code.Op(_outerInstance.Branch.End - 3) == Op.Opcode.JMP && _d.Code.sBx(_outerInstance.Branch.End - 3) == 2)
					{
						expr = r.GetValue(target, _outerInstance.Branch.End - 2);
					}
					else
					{
						expr = r.GetValue(target, _outerInstance.Branch.Begin);
					}

					_outerInstance.Branch.UseExpression(expr);

					if (r.IsLocal(target, _outerInstance.Branch.End - 1))
						return new Assignment(r.GetTarget(target, _outerInstance.Branch.End - 1), _outerInstance.Branch.AsExpression(r));

					r.SetValue(target, _outerInstance.Branch.End - 1, _outerInstance.Branch.AsExpression(r));
				}
				else if (expr != null && _outerInstance.Target >= 0)
				{
					_outerInstance.Branch.UseExpression(expr);

					if (r.IsLocal(_outerInstance.Target, _outerInstance.Branch.End - 1))
						return new Assignment(r.GetTarget(_outerInstance.Target, _outerInstance.Branch.End - 1), _outerInstance.Branch.AsExpression(r));

					r.SetValue(_outerInstance.Target, _outerInstance.Branch.End - 1, _outerInstance.Branch.AsExpression(r));
				}
				else
				{
					throw new Exception($"Fail {_outerInstance.Branch.End - 1}: {expr} (target: {_outerInstance.Target})");
				}

				return null;
			}

		}
	}
}
