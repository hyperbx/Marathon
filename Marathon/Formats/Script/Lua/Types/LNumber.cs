namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class LNumber : LObject
    {
        public static LNumber MakeInteger(int number) => new LIntNumber(number);

        public abstract new string ToString();

        public abstract double Value();
    }

    class LFloatNumber : LNumber
    {
        public readonly float Number;

        public LFloatNumber(float number) => Number = number;

        public override string ToString()
        {
            if (Number == (float)Math.Round(Number))
            {
                return ((int)Number).ToString();
            }
            else
            {
                return Number.ToString();
            }
        }

        public override string Dereference() => throw new NotImplementedException();

        public override bool Equals(object o)
        {
            if (o is LFloatNumber lFloatNumber)
            {
                return Number == lFloatNumber.Number;
            }
            else if (o is LNumber lNumber)
            {
                return Value() == lNumber.Value();
            }

            return false;
        }

        public override double Value() => Number;
    }

    class LDoubleNumber : LNumber
    {
        public readonly double Number;

        public LDoubleNumber(double number) => Number = number;

        public override string ToString()
        {
            if (Number == (double)Math.Round(Number))
            {
                return ((long)Number).ToString();
            }
            else
            {
                return Number.ToString();
            }
        }

        public override string Dereference() => throw new NotImplementedException();

        public override bool Equals(object o)
        {
            if (o is LDoubleNumber lDoubleNumber)
            {
                return Number == lDoubleNumber.Number;
            }
            else if (o is LNumber lNumber)
            {
                return Value() == lNumber.Value();
            }

            return false;
        }

        public override double Value() => Number;
    }

    class LIntNumber : LNumber
    {
        public readonly int Number;

        public LIntNumber(int number) => Number = number;

        public override string ToString() => Number.ToString();

        public override string Dereference() => throw new NotImplementedException();

        public override bool Equals(object o)
        {
            if (o is LIntNumber lIntNumber)
            {
                return Number == lIntNumber.Number;
            }
            else if (o is LNumber lNumber)
            {
                return Value() == lNumber.Value();
            }

            return false;
        }

        public override double Value() => Number;
    }

    class LLongNumber : LNumber
    {
        public readonly long Number;

        public LLongNumber(long number) => Number = number;

        public override string ToString() => Number.ToString();

        public override string Dereference() => throw new NotImplementedException();

        public override bool Equals(object o)
        {
            if (o is LLongNumber lLongNumber)
            {
                return Number == lLongNumber.Number;
            }
            else if (o is LNumber lNumber)
            {
                return Value() == lNumber.Value();
            }

            return false;
        }

        public override double Value() => Number;
    }
}
