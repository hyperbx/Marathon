using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using System.Collections.Generic;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class AlwaysLoop(LFunction in_function, int in_begin, int in_end) : Block(in_function, in_begin, in_end)
    {
        private readonly List<Statement> _statements = new();

        public override int ScopeEnd()
        {
            return End - 2;
        }

        public override bool Breakable()
        {
            return true;
        }

        public override bool IsContainer()
        {
            return true;
        }

        public override bool IsUnprotected()
        {
            return true;
        }

        public override int GetLoopback()
        {
            return Begin;
        }

        public override void AddStatement(Statement in_statement)
        {
            _statements.Add(in_statement);
        }

        public override void Write(Output in_output)
        {
            in_output.WriteLine("while true do");
            in_output.Indent();

            WriteSequence(in_output, _statements);

            in_output.Dedent();
            in_output.Write("end");
        }
    }
}
