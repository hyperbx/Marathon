using Marathon.IO.Formats.Scripts.Lua.Helpers;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Declaration
    {
        public readonly string name;
        public readonly int begin, end;
        public int register;

        /// <summary>
        /// Whether this is an invisible for-loop book-keeping variable.
        /// </summary>
        public bool forLoop = false;

        /// <summary>
        /// Whether this is an explicit for-loop declared variable.
        /// </summary>
        public bool forLoopExplicit = false;

        public Declaration(LLocal local)
        {
            name = local.toString();
            begin = local.start;
            end = local.end;
        }

        public Declaration(string name, int begin, int end)
        {
            this.name = name;
            this.begin = begin;
            this.end = end;
        }
    }
}
