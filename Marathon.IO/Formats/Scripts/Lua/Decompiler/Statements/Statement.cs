using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements
{
    public abstract class Statement
    {
        /// <summary>
        /// Prints out a sequences of statements on separate lines. Correctly informs the last statement that it is last in a block.
        /// </summary>
        public static void printSequence(Output @out, List<Statement> stmts)
        {
            int n = stmts.Count;

            for (int i = 0; i < n; i++)
            {
                bool last = i + 1 == n;
                Statement stmt = stmts[i],
                          next = last ? null : stmts[i + 1];

                if (last)
                    stmt.printTail(@out);

                else
                    stmt.print(@out);

                if (next != null && stmt is FunctionCallStatement && next.beginsWithParen())
                    @out.print(";");

                if (!(stmt is IfThenElseBlock))
                    @out.println();
            }
        }

        public abstract void print(Output @out);

        public void printTail(Output @out) => print(@out);

        public string comment;

        public void addComment(string comment) => this.comment = comment;

        public bool beginsWithParen() => false;
    }
}
