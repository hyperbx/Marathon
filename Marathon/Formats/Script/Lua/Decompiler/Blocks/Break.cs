using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Types;
using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class Break(LFunction in_function, int in_line, int in_target) : Block(in_function, in_line, in_line)
    {
        public readonly int Target = in_target;

        public override void AddStatement(Statement in_statement)
        {
            throw new NotSupportedException();
        }

        public override bool IsContainer()
        {
            return false;
        }

        public override bool Breakable()
        {
            return false;
        }

        public override bool IsUnprotected()
        {
            // This *is* unprotected, but isn't really a block.
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            /* FIX: "do break end" is a syntax error. There's not really
               much point to writing an empty statement, so lets remove it. */

            // in_output.Write("do break end");
        }

        public override void WriteTail(Output in_output)
        {
            in_output.Write("break");
        }
    }
}
