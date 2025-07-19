using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class TableReference(Expression in_table, Expression in_index) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex() => Math.Max(in_table.GetConstantIndex(), in_index.GetConstantIndex());

        public override void Write(Output in_output)
        {
            in_table.Write(in_output);

            if (in_index.IsIdentifier())
            {
                in_output.Write(".");
                in_output.Write(in_index.AsName());
            }
            else
            {
                in_output.Write("[");
                in_index.Write(in_output);
                in_output.Write("]");
            }
        }

        public override bool IsDotChain()
        {
            return in_index.IsIdentifier() && in_table.IsDotChain();
        }

        public override bool IsMemberAccess()
        {
            return in_index.IsIdentifier();
        }

        public override Expression GetTable()
        {
            return in_table;
        }

        public override string GetField()
        {
            return in_index.AsName();
        }
    }
}
