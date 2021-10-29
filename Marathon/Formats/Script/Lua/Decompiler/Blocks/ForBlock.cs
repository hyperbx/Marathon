using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class ForBlock : Block
    {
        private readonly int _register;
        private readonly Registers _r;
        private readonly List<Statement> _statements;

        public ForBlock(LFunction function, int begin, int end, int register, Registers r) : base(function, begin, end)
        {
            _register = register;
            _r = r;
            _statements = new List<Statement>(end - begin + 1);
        }

        public override int ScopeEnd() => End - 2;

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool Breakable() => true;

        public override bool IsContainer() => true;

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            @out.Write("for ");

            if (_function.Header.Version == Version.LUA50)
            {
                _r.GetTarget(_register, Begin - 1).Write(@out);
            }
            else
            {
                _r.GetTarget(_register + 3, Begin - 1).Write(@out);
            }

            @out.Write(" = ");

            if (_function.Header.Version == Version.LUA50)
            {
                _r.GetValue(_register, Begin - 2).Write(@out);
            }
            else
            {
                _r.GetValue(_register, Begin - 1).Write(@out);
            }

            @out.Write(", ");

            _r.GetValue(_register + 1, Begin - 1).Write(@out);

            Expression step = _r.GetValue(_register + 2, Begin - 1);

            if (!step.IsInteger() || step.AsInteger() != 1)
            {
                @out.Write(", ");
                step.Write(@out);
            }

            @out.Write(" do");
            @out.WriteLine();
            @out.Indent();

            WriteSequence(@out, _statements);

            @out.Dedent();
            @out.Write("end");
        }
    }
}
