using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Operations
{
    public class TableSet : Operation
    {
        private Expression _table,
                           _index,
                           _value;

        private bool _isTable;

        private int _timestamp;

        public TableSet(int line, Expression table, Expression index, Expression value, bool isTable, int timestamp) : base(line)
        {
            _table = table;
            _index = index;
            _value = value;
            _isTable = isTable;
            _timestamp = timestamp;
        }

        public override Statement Process(Registers r, Block block)
        {
            if (_table.IsTableLiteral())
            {
                _table.AddEntry(new TableLiteral.Entry(_index, _value, !_isTable, _timestamp));

                return null;
            }
            else
            {
                return new Assignment(new TableTarget(_table, _index), _value);
            }
        }
    }
}
