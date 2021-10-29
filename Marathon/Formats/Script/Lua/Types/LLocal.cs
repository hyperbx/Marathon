namespace Marathon.Formats.Script.Lua.Types
{
    public class LLocal : BObject
    {
        public readonly LString Name;
        public readonly int Start, End;

        /// <summary>
        /// Used by the decompiler for annotation.
        /// </summary>
        public bool ForLoop = false;

        public LLocal(LString name, BInteger start, BInteger end)
        {
            Name = name;
            Start = start.AsInt();
            End = end.AsInt();
        }

        public override string ToString() => Name.Dereference();
    }
}
