using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Declaration
    {
        public readonly string Name;
        public readonly int Begin, End;
        public int Register;

        /// <summary>
        /// Whether this is an invisible for loop book-keeping variable.
        /// </summary>
        public bool ForLoop = false;

        /// <summary>
        /// Whether this is an explicit for loop declared variable.
        /// </summary>
        public bool ForLoopExplicit = false;

        public Declaration(LLocal local)
        {
            Name = local.ToString();
            Begin = local.Start;
            End = local.End;
        }

        public Declaration(string name, int begin, int end)
        {
            Name = name;
            Begin = begin;
            End = end;
        }
    }
}
