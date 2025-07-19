using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class BooleanIndicator(LFunction in_function, int in_line) : Block(in_function, in_line, in_line)
    {
        public override void AddStatement(Statement in_statement) { }

        public override bool IsContainer()
        {
            return false;
        }

        public override bool IsUnprotected()
        {
            return false;
        }

        public override bool Breakable()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            in_output.Write("-- WARNING: unhandled boolean indicator!");
        }
    }
}
