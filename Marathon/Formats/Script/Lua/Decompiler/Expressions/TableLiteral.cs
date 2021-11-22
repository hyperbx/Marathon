namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableLiteral : Expression
    {
        public class Entry : IComparable<Entry>
        {
            public readonly Expression Key, Value;
            public readonly bool IsList;
            public readonly int Timestamp;

            public Entry(Expression key, Expression value, bool isList, int timestamp)
            {
                Key = key;
                Value = value;
                IsList = isList;
                Timestamp = timestamp;
            }

            public int CompareTo(Entry e) => Timestamp.CompareTo(e.Timestamp);
        }

        private List<Entry> _entries;

        private bool _isObject = true,
                     _isList = true;

        private int listLength = 1;

        public TableLiteral() : this(5, 5) { }

        public TableLiteral(int arraySize, int hashSize) : base(Precedence.ATOMIC) => _entries = new List<Entry>(arraySize + hashSize);

        public override int GetConstantIndex()
        {
            int index = -1;

            foreach (Entry entry in _entries)
            {
                index = Math.Max(entry.Key.GetConstantIndex(), index);
                index = Math.Max(entry.Value.GetConstantIndex(), index);
            }

            return index;
        }

        public override void Write(Output @out)
        {
            _entries.Sort();

            listLength = 1;

            if (_entries.Count == 0)
            {
                @out.Write("{}");
            }
            else
            {
                bool lineBreak = _isList && _entries.Count > 5 || _isObject && _entries.Count > 2 || !_isObject;

                if (!lineBreak)
                {
                    foreach (Entry entry in _entries)
                    {
                        Expression value = entry.Value;

                        if (!value.IsBrief())
                        {
                            lineBreak = true;

                            break;
                        }
                    }
                }

                @out.Write("{");

                if (lineBreak)
                {
                    @out.WriteLine();
                    @out.Indent();
                }

                WriteEntry(0, @out);

                if (!_entries[0].Value.IsMultiple())
                {
                    for (int index = 1; index < _entries.Count; index++)
                    {
                        @out.Write(",");

                        if (lineBreak)
                        {
                            @out.WriteLine();
                        }
                        else
                        {
                            @out.Write(" ");
                        }

                        WriteEntry(index, @out);

                        if (_entries[index].Value.IsMultiple())
                            break;
                    }
                }

                if (lineBreak)
                {
                    @out.WriteLine();
                    @out.Dedent();
                }

                @out.Write("}");
            }
        }

        private void WriteEntry(int index, Output @out)
        {
            Entry entry = _entries[index];

            Expression key = entry.Key,
                       value = entry.Value;

            bool isList = entry.IsList,
                 multiple = index + 1 >= _entries.Count || value.IsMultiple();

            if (isList && key.IsInteger() && listLength == key.AsInteger())
            {
                if (multiple)
                {
                    value.WriteMultiple(@out);
                }
                else
                {
                    value.Write(@out);
                }

                listLength++;
            }
            else if (_isObject && key.IsIdentifier())
            {
                @out.Write(key.AsName());
                @out.Write(" = ");
                value.Write(@out);
            }
            else
            {
                @out.Write("[");
                key.Write(@out);
                @out.Write("] = ");
                value.Write(@out);
            }
        }

        public override bool IsTableLiteral() => true;

        public override void AddEntry(Entry entry)
        {
            _entries.Add(entry);

            _isObject = _isObject && (entry.IsList || entry.Key.IsIdentifier());

            _isList = _isList && entry.IsList;
        }

        public override bool IsBrief() => false;
    }
}
