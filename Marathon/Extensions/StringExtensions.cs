using Marathon.IO;

namespace Marathon.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitLineBreaks(this string in_str)
        {
            return in_str.Split(['\r', '\n']);
        }

        public static string Truncate(this string in_str, int in_maxLength, bool in_isEllipsis = false)
        {
            if (string.IsNullOrEmpty(in_str))
                return in_str;

            if (in_str.Length <= in_maxLength)
                return in_str;

            if (in_isEllipsis)
            {
                if (in_maxLength <= 3)
                {
                    return in_str[..in_maxLength];
                }
                else
                {
                    return in_str[..(in_maxLength - 3)] + "...";
                }
            }

            return in_str[..in_maxLength];
        }

        public static NumberType GetNumberType(this string in_str)
        {
            if (in_str.StartsWith("0x"))
            {
                return NumberType.Hexadecimal;
            }
            else if (in_str.StartsWith("0b"))
            {
                return NumberType.Binary;
            }
            else if (in_str.StartsWith("0o"))
            {
                return NumberType.Octal;
            }

            return NumberType.Decimal;
        }

        public static string TrimNumberIdentifier(this string in_str)
        {
            if (in_str.GetNumberType() == NumberType.Decimal)
                return in_str;

            return in_str[2..];
        }
    }
}
