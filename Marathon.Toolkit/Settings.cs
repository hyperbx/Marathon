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

using System.IO;
using System.Windows.Forms;
using Config.Net;

namespace Marathon.Toolkit
{
    public class Settings
    {
        /// <summary>
        /// Location of the configuration file.
        /// </summary>
        private static readonly string Configuration = Path.Combine(Application.StartupPath, $"Marathon.Toolkit.json");

        /// <summary>
        /// Initialises the settings interface.
        /// </summary>
        public static ISettings Marathon = new ConfigurationBuilder<ISettings>().UseJsonFile(Configuration).Build();
    }

    public interface ISettings
    {
        /*
         * This is where properties can be added for the configuration file.
         * 
         * Please use the following etiquette for creating new properties:
         * 
         *     /// <summary>
         *     /// Dummy description for this property.
         *     /// </summary>
         *     [Option(Alias = "DummyProperty", DefaultValue = [value])]
         *     [type] DummyProperty { get; set; }
         *     
         * You must provide a summary, alias and default value for each property;
         * aliases must only be used when necessary if multiple properties fall under
         * the same category and should be grouped together.
         * 
         * Properties that require custom get and set functions can use initialisers.
         */

        /// <summary>
        /// The location of the executable file for SONIC THE HEDGEHOG.
        /// </summary>
        public string GameExecutable { get; set; }
    }
}
