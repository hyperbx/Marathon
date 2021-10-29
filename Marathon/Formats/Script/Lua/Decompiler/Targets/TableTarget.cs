using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class TableTarget : Target
    {
        private readonly Expression _table, _index;

        public TableTarget(Expression table, Expression index)
        {
            _table = table;
            _index = index;
        }

        public override void Write(Output @out)
            => new TableReference(_table, _index).Write(@out);

        public override void WriteMethod(Output @out)
        {
            _table.Write(@out);
            @out.Write(":");
            @out.Write(_index.AsName());
        }

        public override bool IsFunctionName()
        {
            if (!_index.IsIdentifier())
                return false;

            if (!_table.IsDotChain())
                return false;

            return true;
        }
    }
}
