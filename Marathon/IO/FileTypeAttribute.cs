using System;
using System.IO.Enumeration;
using System.Text.RegularExpressions;

namespace Marathon.IO
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FileTypeAttribute(string in_name, string in_category, string in_pattern, bool in_isRegex = false) : Attribute
    {
        public string Name => in_name;

        public string Category => in_category;

        public string Pattern => in_pattern;

        public bool IsRegex => in_isRegex;

        public string GetExtension()
        {
            return Pattern;
        }

        public string GetExtension(string in_path)
        {
            if (IsRegex)
                return GetExtensionMatch(in_path).Value;

            return Pattern;
        }

        public Match GetExtensionMatch(string in_path)
        {
            return Regex.Match(in_path, Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool IsMatchingPattern(string in_path)
        {
            if (string.IsNullOrEmpty(Pattern))
                return false;

            if (IsRegex)
                return GetExtensionMatch(in_path).Success;

            var pattern = Pattern;

            if (!pattern.StartsWith('*'))
                pattern = '*' + pattern;

            return FileSystemName.MatchesSimpleExpression(pattern, in_path);
        }

        public override string ToString()
        {
            return $"{Name} (\"{Pattern}\")";
        }
    }
}
