using System;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableLiteral(int in_arraySize, int in_hashSize) : Expression(Precedence.Atomic)
    {
        private readonly List<TableEntry> _entries = new(in_arraySize + in_hashSize);

        private bool _isObject = true;
        private bool _isList = true;

        private int _listLength = 1;

        public TableLiteral() : this(5, 5) { }

        public override int GetConstantIndex()
        {
            var index = -1;

            foreach (var entry in _entries)
            {
                index = Math.Max(entry.Key.GetConstantIndex(), index);
                index = Math.Max(entry.Value.GetConstantIndex(), index);
            }

            return index;
        }

        public override void Write(Output in_output)
        {
            _entries.Sort();

            _listLength = 1;

            if (_entries.Count == 0)
            {
                in_output.Write("{}");
            }
            else
            {
                var lineBreak = _isList && _entries.Count > 5 || _isObject && _entries.Count > 2 || !_isObject;

                if (!lineBreak)
                {
                    foreach (var entry in _entries)
                    {
                        var value = entry.Value;

                        if (!value.IsBrief())
                        {
                            lineBreak = true;
                            break;
                        }
                    }
                }

                in_output.Write("{");

                if (lineBreak)
                {
                    in_output.WriteLine();
                    in_output.Indent();
                }

                WriteEntry(0, in_output);

                if (!_entries[0].Value.IsMultiple())
                {
                    for (int index = 1; index < _entries.Count; index++)
                    {
                        in_output.Write(",");

                        if (lineBreak)
                        {
                            in_output.WriteLine();
                        }
                        else
                        {
                            in_output.Write(" ");
                        }

                        WriteEntry(index, in_output);

                        if (_entries[index].Value.IsMultiple())
                            break;
                    }
                }

                if (lineBreak)
                {
                    in_output.WriteLine();
                    in_output.Dedent();
                }

                in_output.Write("}");
            }
        }

        private void WriteEntry(int in_index, Output in_output)
        {
            var entry = _entries[in_index];
            var key = entry.Key;
            var value = entry.Value;

            if (entry.IsList && key.IsInteger() && _listLength == key.AsInteger())
            {
                var isMultiple = in_index + 1 >= _entries.Count || value.IsMultiple();

                if (isMultiple)
                {
                    value.WriteMultiple(in_output);
                }
                else
                {
                    value.Write(in_output);
                }

                _listLength++;
            }
            else if (_isObject && key.IsIdentifier())
            {
                in_output.Write(key.AsName());
                in_output.Write(" = ");
                value.Write(in_output);
            }
            else
            {
                in_output.Write("[");
                key.Write(in_output);
                in_output.Write("] = ");
                value.Write(in_output);
            }
        }

        public override bool IsTableLiteral()
        {
            return true;
        }

        public override void AddEntry(TableEntry in_entry)
        {
            _entries.Add(in_entry);

            _isObject = _isObject && (in_entry.IsList || in_entry.Key.IsIdentifier());
            _isList = _isList && in_entry.IsList;
        }

        public override bool IsBrief()
        {
            return false;
        }
    }
}
