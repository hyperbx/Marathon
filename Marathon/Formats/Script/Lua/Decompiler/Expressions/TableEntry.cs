using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableEntry(Expression in_key, Expression in_value, bool in_isList, int in_timestamp) : IComparable<TableEntry>
    {
        public Expression Key => in_key;

        public Expression Value => in_value;

        public bool IsList => in_isList;

        public int Timestamp => in_timestamp;

        public int CompareTo(TableEntry in_entry)
        {
            return Timestamp.CompareTo(in_entry.Timestamp);
        }
    }
}
