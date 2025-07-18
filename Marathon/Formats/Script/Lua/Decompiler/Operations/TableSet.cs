using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class TableSet(int in_line, Expression in_table, Expression in_index, Expression in_value, bool in_isTable, int in_timestamp) : Operation(in_line)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            if (in_table.IsTableLiteral())
            {
                in_table.AddEntry(new TableEntry(in_index, in_value, !in_isTable, in_timestamp));
                return null;
            }

            return new Assignment(new TableTarget(in_table, in_index), in_value);
        }
    }
}
