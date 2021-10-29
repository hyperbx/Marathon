using System;
using System.Numerics;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BInteger : BObject
    {
        private readonly BigInteger _big;
        private readonly int _n;

        private static BigInteger _max;
        private static BigInteger _min;

        public BInteger(BInteger b)
        {
            _big = b._big;
            _n = b._n;
        }

        public BInteger(int n)
        {
            _big = BigInteger.Zero;
            _n = n;
        }

        public BInteger(BigInteger big)
        {
            _big = big;
            _n = 0;

            if (_max == 0)
            {
                _max = int.MaxValue;
                _min = int.MinValue;
            }
        }

        public int AsInt()
        {
            if (_big == 0)
            {
                return _n;
            }
            else if (_big.CompareTo(_max) > 0 || _big.CompareTo(_min) < 0)
            {
                throw new Exception("The size of an integer is outside the range that unluac can handle.");
            }
            else
            {
                return (int)_big;
            }
        }

        public void Iterate(Action thunk)
        {
            if (_big == 0)
            {
                int i = _n;

                while (i-- != 0)
                    thunk();
            }
            else
            {
                BigInteger i = _big;

                while (_big.Sign > 0)
                {
                    thunk();
                    i -= BigInteger.One;
                }
            }
        }
    }
}
