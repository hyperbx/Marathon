using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions
{
    public class TableReference : Expression
    {
        private readonly Expression table, index;

        public TableReference(Expression table, Expression index) : base(PRECEDENCE_ATOMIC)
        {
            this.table = table;
            this.index = index;
        }

        public override int getConstantIndex() => Math.Max(table.getConstantIndex(), index.getConstantIndex());

        public override void print(Output @out)
        {
            table.print(@out);

            if (index.isIdentifier())
            {
                @out.print(".");
                @out.print(index.asName());
            }
            else
            {
                @out.print("[");
                index.print(@out);
                @out.print("]");
            }
        }

        public bool isDotChain() => index.isIdentifier() && table.isDotChain();

        public bool isMemberAccess() => index.isIdentifier();

        public Expression getTable() => table;

        public string getField() => index.asName();
    }
}
