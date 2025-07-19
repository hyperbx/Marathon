using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System;
using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class ElseEndBlock(LFunction in_function, int in_begin, int in_end) : Block(in_function, in_begin, in_end), IComparable<Block>
    {
        private readonly List<Statement> _statements = new(in_end - in_begin + 1);

        public IfThenElseBlock Partner { get; set; }

        public new int CompareTo(Block in_block)
        {
            if (in_block == Partner)
                return 1;

            return base.CompareTo(in_block);
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }
            

        public override bool IsUnprotected()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            if (_statements.Count == 1 && _statements[0] is IfThenEndBlock)
            {
                in_output.Write("else");

                _statements[0].Write(in_output);
            }
            else if (_statements.Count == 2 && _statements[0] is IfThenElseBlock && _statements[1] is ElseEndBlock)
            {
                in_output.Write("else");

                _statements[0].Write(in_output);
                _statements[1].Write(in_output);
            }
            else
            {
                in_output.Write("else");
                in_output.WriteLine();
                in_output.Indent();

                WriteSequence(in_output, _statements);

                in_output.Dedent();
                in_output.Write("end");
            }
        }
    }
}
