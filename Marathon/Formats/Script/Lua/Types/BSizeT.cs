using System.Numerics;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class BSizeT : BInteger
    {
        public BSizeT(BInteger in_value) : base(in_value) { }

        public BSizeT(int in_value) : base(in_value) { }

        public BSizeT(BigInteger in_value) : base(in_value) { }
    }
}
