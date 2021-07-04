using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets
{
    public class TableTarget : Target
    {
        private readonly Expression table, index;

        public TableTarget(Expression table, Expression index)
        {
            this.table = table;
            this.index = index;
        }

        public override void print(Output @out) => new TableReference(table, index).print(@out);

        public override void printMethod(Output @out)
        {
            table.print(@out);
            @out.print(":");
            @out.print(index.asName());
        }

        public bool isFunctionName()
        {
            if (!index.isIdentifier())
                return false;

            if (!table.isDotChain())
                return false;

            return true;
        }
    }
}
