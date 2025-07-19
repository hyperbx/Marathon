using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public abstract class Statement
    {
        public string Comment { get; set; }

        /// <summary>
        /// Prints out a sequences of statements on separate lines.
        /// <para>Correctly informs the last statement that it is last in a block.</para>
        /// </summary>
        public static void WriteSequence(Output in_output, List<Statement> in_statements)
        {
            for (int i = 0; i < in_statements.Count; i++)
            {
                var isLast = i + 1 == in_statements.Count;
                var statement = in_statements[i];
                var nextStatement = isLast ? null : in_statements[i + 1];

                if (isLast)
                {
                    statement.WriteTail(in_output);
                }
                else
                {
                    statement.Write(in_output);
                }

                if (nextStatement != null && statement is FunctionCallStatement && nextStatement.BeginsWithParen())
                    in_output.Write(";");

                if (statement is not IfThenElseBlock)
                    in_output.WriteLine();
            }
        }

        public virtual bool BeginsWithParen()
        {
            return false;
        }

        public abstract void Write(Output in_output);

        public virtual void WriteTail(Output in_output)
        {
            Write(in_output);
        }
    }
}
