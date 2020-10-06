// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
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

namespace Marathon.Toolkit.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Parses all line breaks from a string.
        /// </summary>
        /// <param name="text">String to parse line breaks from.</param>
        public static string[] ParseLineBreaks(string text)
            => text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

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
