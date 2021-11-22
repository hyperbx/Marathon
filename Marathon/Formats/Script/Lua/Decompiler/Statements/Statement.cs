using Marathon.Formats.Script.Lua.Decompiler.Blocks;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public abstract class Statement
    {
        public string Comment;

        /// <summary>
        /// Prints out a sequences of statements on separate lines.
        /// <para>Correctly informs the last statement that it is last in a block.</para>
        /// </summary>
        public static void WriteSequence(Output @out, List<Statement> statements)
        {
            int n = statements.Count;

            for (int i = 0; i < n; i++)
            {
                bool last = i + 1 == n;

                Statement statement = statements[i],
                          nextStatement = last ? null : statements[i + 1];

                if (last)
                {
                    statement.WriteTail(@out);
                }
                else
                {
                    statement.Write(@out);
                }

                if (nextStatement != null && statement is FunctionCallStatement && nextStatement.BeginsWithParent())
                    @out.Write(";");

                if (statement is not IfThenElseBlock)
                    @out.WriteLine();
            }
        }

        public abstract void Write(Output @out);

        public virtual void WriteTail(Output @out)
            => Write(@out);

        public void AddComment(string comment)
            => Comment = comment;

        public virtual bool BeginsWithParent() => false;
    }
}
