using System;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class SetBlock : Block
    {
        public readonly int target;
        private Assignment assign;
        public readonly Branch branch;
        private Registers r;
        private bool empty,
                     finalize = false;

        public SetBlock(LFunction function, Branch branch, int target, int line, int begin, int end, bool empty, Registers r) : base(function, begin, end)
        {
            this.empty = empty;
            if (begin == end) this.begin -= 1;
            this.target = target;
            this.branch = branch;
            this.r = r;
        }

        public override void addStatement(Statement statement)
        {
            if (!finalize && statement is Assignment)
                assign = (Assignment)statement;

            else if (statement is BooleanIndicator)
                finalize = true;
        }

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            if (assign != null && assign.getFirstTarget() != null)
            {
                Assignment assignOut = new Assignment(assign.getFirstTarget(), getValue());
                assignOut.print(@out);
            }
            else
            {
                @out.print("-- unhandled set block");
                @out.println();
            }
        }

        public override bool breakable() => false;

        public override bool isContainer() => false;

        public void useAssignment(Assignment assign)
        {
            this.assign = assign;

            branch.useExpression(assign.getFirstValue());
        }

        public Expression getValue() => branch.asExpression(r);

        public override Operation process(Decompiler d)
        {
            if (empty)
            {
                Expression expression = r.getExpression(branch.setTarget, end);
                branch.useExpression(expression);

                return new RegisterSet(end - 1, branch.setTarget, branch.asExpression(r));
            }
            else if (assign != null)
            {
                branch.useExpression(assign.getFirstValue());

                Target target = assign.getFirstTarget();
                Expression value = getValue();

                return new OperationAnonymousInnerClass(this, target, value);
            }
            else
                return new OperationAnonymousInnerClass2(this, d);
        }

		private class OperationAnonymousInnerClass : Operation
		{
			private readonly SetBlock outerInstance;
			private Target target;
			private Expression value;

			public OperationAnonymousInnerClass(SetBlock outerInstance, Target target, Expression value) : base(outerInstance.end - 1)
			{
				this.outerInstance = outerInstance;
				this.target = target;
				this.value = value;
			}

			public override Statement process(Registers r, Block block) => new Assignment(target, value);
		}

		private class OperationAnonymousInnerClass2 : Operation
		{
			private readonly SetBlock outerInstance;
			private Decompiler d;

			public OperationAnonymousInnerClass2(SetBlock outerInstance, Decompiler d) : base(outerInstance.end - 1)
			{
				this.outerInstance = outerInstance;
				this.d = d;
			}

			public override Statement process(Registers r, Block block)
			{
				Expression expr = null;
				int register = 0;

				for (; register < r.registers; register++)
				{
					if (r.getUpdated(register, outerInstance.branch.end - 1) == outerInstance.branch.end - 1)
					{
						expr = r.getValue(register, outerInstance.branch.end);
						break;
					}
				}

				if (d.code.op(outerInstance.branch.end - 2) == Op.Opcode.LOADBOOL && d.code.C(outerInstance.branch.end - 2) != 0)
				{
					int target = d.code.A(outerInstance.branch.end - 2);

					if (d.code.op(outerInstance.branch.end - 3) == Op.Opcode.JMP && d.code.sBx(outerInstance.branch.end - 3) == 2)
					{
						//System.out.println("-- Dropped boolean expression operand");
						expr = r.getValue(target, outerInstance.branch.end - 2);
					}
					else
					{
						expr = r.getValue(target, outerInstance.branch.begin);
					}

					outerInstance.branch.useExpression(expr);

					if (r.isLocal(target, outerInstance.branch.end - 1))
						return new Assignment(r.getTarget(target, outerInstance.branch.end - 1), outerInstance.branch.asExpression(r));

					r.setValue(target, outerInstance.branch.end - 1, outerInstance.branch.asExpression(r));
				}
				else if (expr != null && outerInstance.target >= 0)
				{
					outerInstance.branch.useExpression(expr);

					if (r.isLocal(outerInstance.target, outerInstance.branch.end - 1))
						return new Assignment(r.getTarget(outerInstance.target, outerInstance.branch.end - 1), outerInstance.branch.asExpression(r));

					r.setValue(outerInstance.target, outerInstance.branch.end - 1, outerInstance.branch.asExpression(r));
				}
				else
				{
					Console.WriteLine("-- fail " + (outerInstance.branch.end - 1));
					Console.WriteLine(expr);
					Console.WriteLine(outerInstance.target);
				}

				return null;
			}

		}
	}
}
