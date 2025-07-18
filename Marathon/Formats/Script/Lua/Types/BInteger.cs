using System;
using System.Numerics;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BInteger : BObject
    {
        private readonly BigInteger? _valueBig;
        private readonly int _value;

        private static BigInteger? _min;
        private static BigInteger? _max;

        public BInteger(BInteger in_value)
        {
            _valueBig = in_value._valueBig;
            _value = in_value._value;
        }

        public BInteger(int in_value)
        {
            _valueBig = null;
            _value = in_value;
        }

        public BInteger(BigInteger in_value)
        {
            _valueBig = in_value;
            _value = 0;

            if (_max == 0)
            {
                _max = int.MaxValue;
                _min = int.MinValue;
            }
        }

        public int AsInt()
        {
            if (_valueBig == null)
            {
                return _value;
            }
            else if (_valueBig.Value.CompareTo(_max) > 0 || _valueBig.Value.CompareTo(_min) < 0)
            {
                throw new Exception("Invalid integer value.");
            }
            else
            {
                return (int)_valueBig.Value;
            }
        }

        public void Iterate(Action in_action)
        {
            if (_valueBig == null)
            {
                var i = _value;

                while (i-- != 0)
                    in_action();
            }
            else
            {
                var i = _valueBig.Value;

                while (_valueBig.Value.Sign > 0)
                {
                    in_action();

                    i -= BigInteger.One;
                }
            }
        }
    }
}
