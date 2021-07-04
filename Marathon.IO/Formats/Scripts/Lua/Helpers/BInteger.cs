using System;
using System.Numerics;
using System.Threading;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class BInteger : BObject
    {
        private readonly BigInteger big;
        private readonly int n;

        private static BigInteger MAX_INT;
        private static BigInteger MIN_INT;

        public BInteger(BInteger b)
        {
            big = b.big;
            n = b.n;
        }

        public BInteger(int n)
        {
            big = BigInteger.Zero;
            this.n = n;
        }

        public BInteger(BigInteger big)
        {
            this.big = big;
            n = 0;

            if (MAX_INT == null)
            {
                MAX_INT = int.MaxValue;
                MIN_INT = int.MinValue;
            }
        }

        public int asInt()
        {
            if (big == null)
                return n;

            else if (big.CompareTo(MAX_INT) > 0 || big.CompareTo(MIN_INT) < 0)
                throw new Exception("The size of an integer is outside the range that unluac can handle.");

            else
                return (int)big;
        }

        public void iterate(Thread thunk)
        {
            if (big == null)
            {
                int i = n;

                while (i-- != 0)
                    thunk.Start();
            }
            else
            {
                BigInteger i = big;

                while (big.Sign > 0)
                {
                    thunk.Start();
                    i -= BigInteger.One;
                }
            }
        }
    }
}
