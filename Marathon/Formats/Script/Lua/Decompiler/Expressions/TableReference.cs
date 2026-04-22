using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableReference : Expression
    {
        public Expression Table { get; set; }

        public Expression Index { get; set; }

        public TableReference(Expression in_table, Expression in_index) : base(Precedence.Atomic)
        {
            Table = in_table;
            Index = in_index;

            // FIX (Hyper): set table to use parentheses for accessor.
            if (in_table is TableLiteral out_tableLiteral)
                out_tableLiteral.HasParentheses = true;
        }

        public override int GetConstantIndex()
        {
            return Math.Max(Table.GetConstantIndex(), Index.GetConstantIndex());
        }

        public override void Write(Output in_output)
        {
            Table.Write(in_output);

            if (Index.IsIdentifier())
            {
                in_output.Write(".");
                in_output.Write(Index.AsName());
            }
            else
            {
                in_output.Write("[");
                Index.Write(in_output);
                in_output.Write("]");
            }
        }

        public override bool IsDotChain()
        {
            return Index.IsIdentifier() && Table.IsDotChain();
        }

        public override bool IsMemberAccess()
        {
            return Index.IsIdentifier();
        }

        public override Expression GetTable()
        {
            return Table;
        }

        public override string GetField()
        {
            return Index.AsName();
        }
    }
}
