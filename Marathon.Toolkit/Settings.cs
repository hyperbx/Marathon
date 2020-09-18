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

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using Marathon.Toolkit.Forms;
using Marathon.Toolkit.Exceptions;

namespace Marathon.Toolkit
{
    public partial class Settings
    {
        public const string _RootElement     = "Marathon",
                            _PropertyElement = "Property",
                            _NameElement     = "Name",
                            _TypeElement     = "Type";

        public static readonly string _Configuration = Path.Combine(Application.StartupPath, $"{_RootElement}.xml");

        public static XDocument _ConfigurationXML = File.Exists(_Configuration) ? XDocument.Load(_Configuration) : null;

        /// <summary>
        /// Loads the stored application settings.
        /// </summary>
        public static void Load()
        {
            // Dummy XML element for the exception.
            XElement pointOfFailure = new XElement("Marathon.InvalidSettingsException");

#if !DEBUG
            try
            {
#endif
                if (File.Exists(_Configuration))
                {
                    foreach (XElement propertyElem in _ConfigurationXML.Root.Elements(_PropertyElement))
                    {
                        // In case of failure, set the dummy XML element so we know where it failed.
                        pointOfFailure = propertyElem;

                        // Finds the property.
                        PropertyInfo property = typeof(Settings).GetProperties().Where(x => x.Name == propertyElem.Attribute(_NameElement).Value).Single();

                        // Gets the data type from the attribute.
                        Type propertyType = Type.GetType(propertyElem.Attribute(_TypeElement).Value);

                        // Sets the new value from the configuration.
                        property.SetValue(property, TypeDescriptor.GetConverter(propertyType).ConvertTo(propertyElem.Value, propertyType));
                    }
                }
#if !DEBUG
            }
            catch (Exception ex)
            {
                // If there's somehow a false positive, this will ensure it doesn't mess up further.
                string @name = pointOfFailure.Attributes().Count() == 0 ? "Marathon.InvalidSettingsException" : pointOfFailure.Attribute(_NameElement).Value;

                // Display the error handler, just to be safe.
                new ErrorHandler(new InvalidSettingsException(@name, ex)).ShowDialog();

                // Warn the user of attempted recovery.
                MarathonMessageBox.Show("Marathon will now attempt to correct the error...", "Configuration Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Recover(); // Fix broken elements in the configuration.
            }
#endif
        }

        /// <summary>
        /// Returns the display name given to the property in Settings.Designer.cs.
        /// </summary>
        /// <param name="property">Raw property.</param>
        public static string GetPropertyDisplayName(PropertyInfo property)
            => property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;

        /// <summary>
        /// Returns the description given to the property in Settings.Designer.cs.
        /// </summary>
        /// <param name="property">Raw property.</param>
        public static string GetPropertyDescription(PropertyInfo property)
            => property.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().Single().Description;

        /// <summary>
        /// Saves the current application settings.
        /// </summary>
        public static void Save()
        {
            XElement rootElem = new XElement(_RootElement);

            foreach (PropertyInfo property in typeof(Settings).GetProperties())
            {
                XElement propertyElem = new XElement(_PropertyElement, property.GetValue(property));
                propertyElem.Add(new XAttribute(_NameElement, property.Name));

                // The assembly is native to .NET, so just write the type by name.
                if (property.PropertyType.Module.ScopeName == "CommonLanguageRuntimeLibrary")
                    propertyElem.Add(new XAttribute(_TypeElement, property.PropertyType));

                // The assembly is not native, so write the full qualified name.
                else
                    propertyElem.Add(new XAttribute(_TypeElement, property.PropertyType.AssemblyQualifiedName));

                rootElem.Add(propertyElem);
            }

            new XDocument(rootElem).Save(_Configuration);
        }

        /// <summary>
        /// Attempts to recover the application settings.
        /// </summary>
        public static void Recover()
        {
            if (File.Exists(_Configuration))
            {
                foreach (XElement propertyElem in _ConfigurationXML.Root.Elements(_PropertyElement).ToArray())
                {
                    // Stage 1: unable to locate the property.
                    if (typeof(Settings).GetProperties().Where(x => x.Name == propertyElem.Attribute(_NameElement).Value).Count() == 0)
                        propertyElem.Remove(); // Get outta here!

                    // Stage 2: unable to get the data type from the attribute.
                    if (!TypeDescriptor.GetConverter(propertyElem.Value).CanConvertTo(Type.GetType(propertyElem.Attribute(_TypeElement).Value)))
                        propertyElem.Remove(); // Removes the scuffed property.
                }

                _ConfigurationXML.Save(_Configuration);

                Load(); // Reload settings after fixing configuration.
            }
        }
    }
}
