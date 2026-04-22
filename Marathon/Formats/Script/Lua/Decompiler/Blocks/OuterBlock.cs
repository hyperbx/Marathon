using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System.Collections.Generic;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class OuterBlock(LFunction in_function, int in_length) : Block(in_function, 0, in_length + 1)
    {
        private readonly List<Statement> _statements = new(in_length);

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override bool IsUnprotected()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override int ScopeEnd()
        {
            return End - 2;
        }

        public override void Write(Output in_output)
        {
            // Extra return statement.
            var last = _statements.Count - 1;

            if (last < 0 || _statements[last] is not Return)
                throw new Exception(_statements[last].ToString());

            _statements.RemoveAt(last);

            WriteSequence(in_output, _statements);
        }
    }
}
