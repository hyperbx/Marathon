using System;
using System.Numerics;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BInteger : BObject
    {
        private readonly BigInteger? _big;
        private readonly int _n;

        private static BigInteger? _max;
        private static BigInteger? _min;

        public BInteger(BInteger b)
        {
            _big = b._big;
            _n = b._n;
        }

        public BInteger(int n)
        {
            _big = null;
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
            if (_big == null)
            {
                return _n;
            }
            else if (_big.Value.CompareTo(_max) > 0 || _big.Value.CompareTo(_min) < 0)
            {
                throw new Exception("The size of an integer is outside the range that unluac can handle.");
            }
            else
            {
                return (int)_big.Value;
            }
        }

        public void Iterate(Action thunk)
        {
            if (_big == null)
            {
                int i = _n;

                while (i-- != 0)
                    thunk();
            }
            else
            {
                BigInteger i = _big.Value;

                while (_big.Value.Sign > 0)
                {
                    thunk();

                    i -= BigInteger.One;
                }
            }
        }
    }
}
