using System;
using System.Collections.Generic;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class TForBlock : Block
    {
        private readonly int _register, _length;
        private readonly Registers _r;
        private readonly List<Statement> _statements;

        public TForBlock(LFunction function, int begin, int end, int register, int length, Registers r) : base(function, begin, end)
        {
            _register = register;
            _length = length;
            _r = r;
            _statements = new List<Statement>(end - begin + 1);
        }

        public override int ScopeEnd() => End - 3;

        public override bool Breakable() => true;

        public override bool IsContainer() => true;

        public override void AddStatement(Statement statement)
            => _statements.Add(statement);

        public override bool IsUnprotected() => false;

        public override int GetLoopback() => throw new Exception();

        public override void Write(Output @out)
        {
            @out.Write("for ");

            if (_function.Header.Version == Version.LUA50)
            {
                _r.GetTarget(_register + 2, Begin - 1).Write(@out);

                for (int r1 = _register + 3; r1 <= _register + 2 + _length; r1++)
                {
                    @out.Write(", ");

                    _r.GetTarget(r1, Begin - 1).Write(@out);
                }
            }
            else
            {
                _r.GetTarget(_register + 3, Begin - 1).Write(@out);

                for (int r1 = _register + 4; r1 <= _register + 2 + _length; r1++)
                {
                    @out.Write(", ");

                    _r.GetTarget(r1, Begin - 1).Write(@out);
                }
            }

            @out.Write(" in ");

            Expression value;
            value = _r.GetValue(_register, Begin - 1);
            value.Write(@out);

            if (!value.IsMultiple())
            {
                @out.Write(", ");

                value = _r.GetValue(_register + 1, Begin - 1);
                value.Write(@out);

                if (!value.IsMultiple())
                {
                    @out.Write(", ");

                    value = _r.GetValue(_register + 2, Begin - 1);
                    value.Write(@out);
                }
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
