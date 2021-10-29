using System.Collections.Generic;

namespace Marathon.Formats.Script.Lua.Decompiler.Statements
{
    public class Declare : Statement
    {
        private readonly List<Declaration> _decls;

        public Declare(List<Declaration> decls) => _decls = decls;

        public override void Write(Output @out)
        {
            @out.Write("local ");
            @out.Write(_decls[0].Name);

            for (int i = 1; i < _decls.Count; i++)
            {
                @out.Write(", ");
                @out.Write(_decls[i].Name);
            }
        }
    }
}
