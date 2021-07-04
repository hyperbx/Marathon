using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public class ElseEndBlock : Block
    {
        private readonly List<Statement> statements;
        public IfThenElseBlock partner;

        public ElseEndBlock(LFunction function, int begin, int end) : base(function, begin, end) => statements = new List<Statement>(end - begin + 1);

        public int compareTo(Block block)
        {
            if (block == partner)
                return 1;

            else
            {
                int result = base.compareTo(block);

                if (result == 0 && block is ElseEndBlock)
                    Console.WriteLine("HEY HEY HEY"); // This was actually a thing in the original source. lol

                return result;
            }
        }

        public override bool breakable() => false;

        public override bool isContainer() => true;

        public override void addStatement(Statement statement) => statements.Add(statement);

        public override bool isUnprotected() => false;

        public override int getLoopback() => throw new Exception();

        public override void print(Output @out)
        {
            if (statements.Count == 1 && statements[0] is IfThenEndBlock)
            {
                @out.print("else");

                statements[0].print(@out);
            }
            else if (statements.Count == 2 && statements[0] is IfThenElseBlock && statements[1] is ElseEndBlock)
            {
                @out.print("else");

                statements[0].print(@out);
                statements[1].print(@out);
            }
            else
            {
                @out.print("else");
                @out.println();
                @out.indent();

                printSequence(@out, statements);

                @out.dedent();
                @out.print("end");
            }
        }
    }
}
