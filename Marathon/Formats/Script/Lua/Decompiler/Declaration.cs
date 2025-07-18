using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Declaration
    {
        public string Name { get; }

        public int Begin { get; }

        public int End { get; }

        public int Register { get; set; }

        public bool IsForLoop { get; set; } = false;

        public bool IsForLoopExplicit { get; set; } = false;

        public Declaration(LLocal in_local)
        {
            Name = in_local.ToString();
            Begin = in_local.Start;
            End = in_local.End;
        }

        public Declaration(string in_name, int in_begin, int in_end)
        {
            Name = in_name;
            Begin = in_begin;
            End = in_end;
        }
    }
}
