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
    }
}
