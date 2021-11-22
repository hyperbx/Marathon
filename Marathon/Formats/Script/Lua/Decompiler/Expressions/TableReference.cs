namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableReference : Expression
    {
        private readonly Expression _table, _index;

        public TableReference(Expression table, Expression index) : base(Precedence.ATOMIC)
        {
            _table = table;
            _index = index;
        }

        public override int GetConstantIndex() => Math.Max(_table.GetConstantIndex(), _index.GetConstantIndex());

        public override void Write(Output @out)
        {
            _table.Write(@out);

            if (_index.IsIdentifier())
            {
                @out.Write(".");
                @out.Write(_index.AsName());
            }
            else
            {
                @out.Write("[");
                _index.Write(@out);
                @out.Write("]");
            }
        }

        public override bool IsDotChain() => _index.IsIdentifier() && _table.IsDotChain();

        public override bool IsMemberAccess() => _index.IsIdentifier();

        public override Expression GetTable() => _table;

        public override string GetField() => _index.AsName();
    }
}
