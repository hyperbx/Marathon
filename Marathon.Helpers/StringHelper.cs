// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Marathon.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Removes illegal characters from a string in a cleaner format.
        /// </summary>
        /// <param name="text">Text to remove from.</param>
        public static string UseSafeFormattedCharacters(this string text)
        {
            return text.Replace(@"\", "")
                       .Replace("/", "-")
                       .Replace(":", "-")
                       .Replace("*", "")
                       .Replace("?", "")
                       .Replace("\"", "'")
                       .Replace("<", "")
                       .Replace(">", "")
                       .Replace("|", "");
        }

        /// <summary>
        /// Line breaks a string after a certain number of characters.
        /// </summary>
        /// <param name="text">Text to splice.</param>
        /// <param name="lineLength">Length to splice at.</param>
        public static string Splice(this string text, int lineLength)
        {
            var charCount = 0;

            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(w => (charCount += w.Length + 1) / lineLength)
                            .Select(g => string.Join(" ", g));

            return string.Join("\n", lines.ToArray());
        }

        /// <summary>
        /// Makes a string plural based on a counter.
        /// </summary>
        /// <param name="text">Text to pluralise.</param>
        /// <param name="count">Number for condition.</param>
        public static string Pluralise(string text, int count)
        {
            if (count == 1)
            {
                return text;
            }
            else
            {
                return $"{text}s";
            }
        }

        /// <summary>
        /// Prepends a prefix to the input string.
        /// </summary>
        /// <param name="text">Text to prepend to.</param>
        /// <param name="prefix">Prefix to prepend.</param>
        /// <param name="returnIfNullOrEmpty">Return the result regardless of validity.</param>
        public static string PrependPrefix(string text, string prefix, int lineBreaks = 0, bool returnIfNullOrEmpty = false)
        {
            string result = prefix + text + new string('\n', lineBreaks);

            switch (returnIfNullOrEmpty)
            {
                case true:
                {
                    // Return the result regardless of it's validity.
                    return result;
                }

                case false:
                {
                    // Return nothing if the input text was null.
                    return string.IsNullOrEmpty(text) ? string.Empty : result;
                }
            }

            return result;
        }

        /// <summary>
        /// Appends a suffix to the input string.
        /// </summary>
        /// <param name="text">Text to append to.</param>
        /// <param name="suffix">Suffix to append.</param>
        /// <param name="returnIfNullOrEmpty">Return the result regardless of validity.</param>
        public static string AppendSuffix(string text, string suffix, int lineBreaks = 0, bool returnIfNullOrEmpty = false)
        {
            string result = text + suffix + new string('\n', lineBreaks);

            switch (returnIfNullOrEmpty)
            {
                case true:
                {
                    // Return the result regardless of it's validity.
                    return result;
                }

                case false:
                {
                    // Return nothing if the input text was null.
                    return string.IsNullOrEmpty(text) ? string.Empty : result;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are
        /// replaced with another specified string according the type of search to use for the specified string.
        /// 
        /// <para><see href="https://stackoverflow.com/a/45756981">Learn more...</see></para>
        /// </summary>
        /// <param name="str">The string performing the replace method.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string replace all occurrences of <paramref name="oldValue"/>.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        public static string Replace(this string str, string oldValue, string @newValue, StringComparison comparisonType)
        {
            // Check inputs.
            if (str == null)
            {
                // Same as the original .NET C# string.Replace() behavior.
                throw new ArgumentNullException(nameof(str));
            }
            else if (str.Length == 0)
            {
                // Same as the original .NET C# string.Replace() behavior.
                return str;
            }
            else if (oldValue == null)
            {
                // Same as the original .NET C# string.Replace() behavior.
                throw new ArgumentNullException(nameof(oldValue));
            }
            else if (oldValue.Length == 0)
            {
                // Same as the original .NET C# string.Replace() behavior.
                throw new ArgumentException("String cannot be of zero length.");
            }

            /* Prepare StringBuilder for storing the processed string.
               StringBuilder has a better performance than String by 30-40%. */
            StringBuilder resultStringBuilder = new StringBuilder(str.Length);

            // Analyze the replacement.
            bool isReplacementNullOrEmpty = string.IsNullOrEmpty(@newValue);

            // Error code.
            const int valueNotFound = -1;

            // Index storage.
            int foundAt,
                startSearchFromIndex = 0;

            while ((foundAt = str.IndexOf(oldValue, startSearchFromIndex, comparisonType)) != valueNotFound)
            {
                int @charsUntilReplacment = foundAt - startSearchFromIndex;
                bool isNothingToAppend = @charsUntilReplacment == 0;

                // Append all characters until the found replacement.
                if (!isNothingToAppend)
                    resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilReplacment);

                // Process the replacement.
                if (!isReplacementNullOrEmpty)
                    resultStringBuilder.Append(@newValue);

                /* Prepare start index for the next search.
                   
                   This is needed to prevent an infinite loop, otherwise the method always starts the search 
                   from the start of the string. */
                startSearchFromIndex = foundAt + oldValue.Length;
                if (startSearchFromIndex == str.Length)
                {
                    /* It's the end of the input string - no more space for the next search.
                       The input string ends with a value that has already been replaced; therefore,
                       the StringBuilder with the result is complete and no further action is required. */
                    return resultStringBuilder.ToString();
                }
            }

            int @charsUntilStringEnd = str.Length - startSearchFromIndex;

            // Append the last part to the result.
            resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilStringEnd);

            return resultStringBuilder.ToString();
        }

        /// <summary>
        /// Appends text to the file name before the extension.
        /// </summary>
        /// <param name="fileName">Full file name.</param>
        /// <param name="text">Text to append.</param>
        public static string AppendToFileName(string fileName, string text)
            => Path.GetFileNameWithoutExtension(fileName) + text + GetFullExtension(fileName);

        /// <summary>
        /// Returns the full extension from the file name.
        /// </summary>
        public static string GetFullExtension(string fileName)
            => Regex.Match(fileName, @"\..*").Value;

        /// <summary>
        /// Returns the full path without an extension.
        /// </summary>
        public static string GetPathWithoutExtension(string path)
            => Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));

        /// <summary>
        /// Returns the folder containing the file.
        /// </summary>
        public static string GetContainingFolder(string path)
            => Path.GetFileName(Path.GetDirectoryName(path));

        /// <summary>
        /// Returns if the directory is empty.
        /// </summary>
        public static bool IsDirectoryEmpty(string path)
            => !Directory.EnumerateFileSystemEntries(path).Any();

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckPathLegitimacy(string path)
            => Directory.Exists(path) && path != string.Empty;

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckFileLegitimacy(string path)
            => File.Exists(path) && path != string.Empty;

        /// <summary>
        /// Returns a new path with the specified filename.
        /// </summary>
        public static string ReplaceFilename(string path, string newFile)
            => Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(newFile));

        /// <summary>
        /// Parses all line breaks from a string.
        /// </summary>
        /// <param name="text">String to parse line breaks from.</param>
        public static string[] ParseLineBreaks(string text, bool escaped = false)
            => text.Split(escaped ? new[] { "\\r\\n", "\\r", "\\n" } : new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        /// <summary>
        /// Truncates a string down to the requested length.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <param name="maxLength">Maximum length of the string.</param>
        /// <param name="ellipsis">Ends the truncated string with an ellipsis to display that it's been truncated.</param>
        public static string Truncate(string value, int maxLength, bool ellipsis = true)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + (ellipsis ? "..." : string.Empty);
        }

        /// <summary>
        /// Parses byte length to a Windows-like suffix string.
        /// </summary>
        /// <param name="i">Byte length.</param>
        public static string ByteLengthToDecimalString(long i)
        {
            // Get absolute value.
            long absolute_i = i < 0 ? -i : i;

            // Determine the suffix and readable value.
            string suffix;
            double readable;

            // Exabyte
            if (absolute_i >= 0x1000000000000000)
            {
                suffix = "EB";
                readable = i >> 50;
            }

            // Petabyte
            else if (absolute_i >= 0x4000000000000)
            {
                suffix = "PB";
                readable = i >> 40;
            }

            // Terabyte
            else if (absolute_i >= 0x10000000000)
            {
                suffix = "TB";
                readable = i >> 30;
            }

            // Gigabyte
            else if (absolute_i >= 0x40000000)
            {
                suffix = "GB";
                readable = i >> 20;
            }

            // Megabyte
            else if (absolute_i >= 0x100000)
            {
                suffix = "MB";
                readable = i >> 10;
            }

            // Kilobyte
            else if (absolute_i >= 0x400)
            {
                suffix = "KB";
                readable = i;
            }

            // Byte
            else
            {
                suffix = "KB";
                readable = i % 1024 >= 1 ? i + 1024 - i % 1024 : i - i % 1024;
            }

            // Divide by 1024 to get fractional value.
            readable /= 1024;

            // Return formatted number with suffix.
            return $"{readable:0} {suffix}";
        }
    }
}
