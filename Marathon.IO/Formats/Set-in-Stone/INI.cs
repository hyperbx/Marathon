// INI.cs is licensed under the MIT License:
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

using System.IO;

namespace Marathon.IO.Formats.SetInStone
{
    /// <summary>
    /// <para>INI parsing class part of Marathon's 'Set-in-Stone' backwards-compatibility.</para>
    /// <para>Used for Sonic '06 Mod Manager interoperability.</para>
    /// </summary>
    public class INI
    {
        /// <summary>
        /// Parses a key from an INI.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>Only for use with Sonic '06 Mod Manager interoperability.</para>
        /// <para>For proper INI parsing, please use the ini-parser library.</para>
        /// </remarks>
        /// <param name="ini">INI to parse from.</param>
        /// <param name="key">Key to return.</param>
        public static string ParseKey(string ini, string key)
        {
            string value = string.Empty;

            using (StreamReader config = new StreamReader(ini))
            {
                string line;

                while ((line = config.ReadLine()) != null)
                {
                    if (line.Split('=')[0] == key)
                    {
                        value = line.Substring(line.IndexOf('=') + 2);
                        value = value.Remove(value.Length - 1);
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Parses a key from an INI as a Boolean.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>Only for use with Sonic '06 Mod Manager interoperability.</para>
        /// <para>For proper INI parsing, please use the ini-parser library.</para>
        /// 
        /// <para>Yes, we tried bool.Parse()...</para>
        /// </remarks>
        /// <param name="ini">INI to parse from.</param>
        /// <param name="key">Key to return.</param>
        public static bool ParseKeyAsBoolean(string ini, string key)
        {
            switch (ParseKey(ini, key))
            {
                case "True":
                {
                    return true;
                }

                case "False":
                {
                    return false;
                }

                default:
                {
                    return false;
                }
            }
        }
    }
}
