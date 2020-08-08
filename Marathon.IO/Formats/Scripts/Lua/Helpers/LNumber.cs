using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public abstract class LNumber : LObject
    {
        public static LNumber makeInteger(int number) => new LIntNumber(number);

        public abstract string toString();

        // TODO: problem solution for this issue.
        public abstract double value();
    }

    class LFloatNumber : LNumber
    {
        public readonly float number;

        public LFloatNumber(float number) => this.number = number;

        public override string toString()
        {
            if (number == (float)Math.Round(number))
                return ((int)number).ToString();

            else
                return number.ToString();
        }

        public override string deref() => throw new NotImplementedException();

        public override bool equals(object o)
        {
            if (o is LFloatNumber)
                return number == ((LFloatNumber)o).number;

            else if (o is LNumber)
                return value() == ((LNumber)o).value();

            return false;
        }

        public override double value() => number;
    }

    class LDoubleNumber : LNumber
    {
        public readonly double number;

        public LDoubleNumber(double number) => this.number = number;

        public override string toString()
        {
            if (number == (double)Math.Round(number))
                return ((long)number).ToString();

            else
                return number.ToString();
        }

        public override string deref() => throw new NotImplementedException();

        public override bool equals(object o)
        {
            if (o is LDoubleNumber)
                return number == ((LDoubleNumber)o).number;

            else if (o is LNumber)
                return value() == ((LNumber)o).value();

            return false;
        }

        public override double value() => number;
    }

    class LIntNumber : LNumber
    {
        public readonly int number;

        public LIntNumber(int number) => this.number = number;

        public override string toString() => number.ToString();

        public override string deref() => throw new NotImplementedException();

        public override bool equals(object o)
        {
            if (o is LIntNumber)
                return number == ((LIntNumber)o).number;

            else if (o is LNumber)
                return value() == ((LNumber)o).value();

            return false;
        }

        public override double value() => number;
    }

    class LLongNumber : LNumber
    {
        public readonly long number;

        public LLongNumber(long number) => this.number = number;

        public override string toString() => number.ToString();

        public override string deref() => throw new NotImplementedException();

        public override bool equals(object o)
        {
            if (o is LLongNumber)
                return number == ((LLongNumber)o).number;

            else if (o is LNumber)
                return value() == ((LNumber)o).value();

            return false;
        }

        public override double value() => number;
    }
}
