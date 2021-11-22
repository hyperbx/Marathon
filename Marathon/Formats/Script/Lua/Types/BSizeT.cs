namespace Marathon.Formats.Script.Lua.Types
{
    public class BSizeT : BInteger
    {
        public BSizeT(BInteger b) : base(b) { }

        public BSizeT(int n) : base(n) { }

        public BSizeT(BigInteger n) : base(n) { }
    }
}
