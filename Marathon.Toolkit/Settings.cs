// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
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

        /// <summary>
        /// Returns the display name given to the property.
        /// </summary>
        /// <param name="property">Raw property.</param>
        public static string GetPropertyDisplayName(PropertyInfo property)
            => property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;

        /// <summary>
        /// Returns the description given to the property.
        /// </summary>
        /// <param name="property">Raw property.</param>
        public static string GetPropertyDescription(PropertyInfo property)
            => property.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().Single().Description;

        /// <summary>
        /// Returns the default value given to the property.
        /// </summary>
        /// <param name="property">Raw property.</param>
        public static object GetPropertyDefault(PropertyInfo property)
            => property.GetCustomAttributes(typeof(DefaultValueAttribute), true).Cast<DefaultValueAttribute>().Single().Value;
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
         *     [DisplayName("Dummy Property"), Description("Dummy description for this property."), DefaultValue([value])]
         *     [type] DummyProperty { get; set; }
         *     
         * You must provide a summary, display name and description for each property;
         * without a display name, the property will be discarded from the Options menu.
         * 
         * Properties that require custom get and set functions can use initialisers.
         */

        /// <summary>
        /// Automatically open the Start Page upon launch.
        /// </summary>
        [DisplayName("Display Start Page on Launch"), Description("Automatically open the Start Page upon launch."), DefaultValue(true)]
        public bool StartPageOnLaunch { get; set; }
    }
}
