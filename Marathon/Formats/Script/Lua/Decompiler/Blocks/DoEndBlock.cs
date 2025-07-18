using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System.Collections.Generic;
using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class DoEndBlock(LFunction in_function, int in_begin, int in_end) : Block(in_function, in_begin, in_end)
    {
        private readonly List<Statement> _statements = new(in_end - in_begin + 1);

        public override void AddStatement(Statement statement)
        {
            _statements.Add(statement);
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

        public override void Write(Output in_output)
        {
            in_output.WriteLine("do");
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("end");
        }
    }
}
