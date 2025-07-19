using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class LNumber : LObject
    {
        public static LNumber MakeInteger(int in_number)
        {
            return new LIntNumber(in_number);
        }

        public abstract new string ToString();

        public abstract double Value();
    }

    class LFloatNumber(float in_number) : LNumber
    {
        public float Number => in_number;

        public override double Value()
        {
            return Number;
        }

        public override string Dereference()
        {
            throw new NotSupportedException();
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is LFloatNumber out_floatNumber)
            {
                return Number == out_floatNumber.Number;
            }
            else if (in_obj is LNumber out_number)
            {
                return Value() == out_number.Value();
            }

            return false;
        }

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
    }

    class LDoubleNumber(double in_number) : LNumber
    {
        public double Number => in_number;

        public override double Value()
        {
            return Number;
        }

        public override string Dereference()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is LDoubleNumber out_doubleNumber)
            {
                return Number == out_doubleNumber.Number;
            }
            else if (in_obj is LNumber out_number)
            {
                return Value() == out_number.Value();
            }

            return false;
        }

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
    }

    class LIntNumber(int in_number) : LNumber
    {
        public int Number => in_number;

        public override double Value()
        {
            return Number;
        }

        public override string Dereference()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is LIntNumber out_intNumber)
            {
                return Number == out_intNumber.Number;
            }
            else if (in_obj is LNumber out_number)
            {
                return Value() == out_number.Value();
            }

            return false;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }

    class LLongNumber(long in_number) : LNumber
    {
        public long Number => in_number;

        public override double Value()
        {
            return Number;
        }

        public override string Dereference()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is LLongNumber out_longNumber)
            {
                return Number == out_longNumber.Number;
            }
            else if (in_obj is LNumber out_number)
            {
                return Value() == out_number.Value();
            }

            return false;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
