using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations
{
    public class TableSet : Operation
    {
        private Expression table,
                           index,
                           value;

        private bool isTable;
        private int timestamp;

        public TableSet(int line, Expression table, Expression index, Expression value, bool isTable, int timestamp) : base(line)
        {
            this.table = table;
            this.index = index;
            this.value = value;
            this.isTable = isTable;
            this.timestamp = timestamp;
        }

        public override Statement process(Registers r, Block block)
        {
            if (table.isTableLiteral())
            {
                table.addEntry(new TableLiteral.Entry(index, value, !isTable, timestamp));
                return null;
            }
            else
                return new Assignment(new TableTarget(table, index), value);
        }
    }
}
