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
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Marathon.Toolkit
{
    class Resources
    {
        /// <summary>
        /// Parses Contributors.xml to a TreeNode array.
        /// </summary>
        public static TreeNode[] ParseContributorsToTreeNodeArray()
        {
            List<TreeNode> contributors = new List<TreeNode>();

            XDocument xml = XDocument.Parse(Marathon.Properties.Resources.Contributors);

            foreach (XElement contributorElem in xml.Root.Elements("Contributor"))
            {
                TreeNode node = new TreeNode
                {
                    Text = contributorElem.Value,
                    Tag = contributorElem
                };

                contributors.Add(node);
            }

            return contributors.ToArray();
        }

        /// <summary>
        /// Parses the name of the file type by extension.
        /// </summary>
        public static string ParseFriendlyNameFromFileExtension(string resource, string fileName)
        {
            // Load the resource.
            XDocument xml = XDocument.Parse(resource);

            // Full extension parsed from the file name.
            string extension = Path.GetExtension(fileName);

            foreach (XElement supportedFileTypesElem in xml.Root.Elements("Type"))
            {
                if (supportedFileTypesElem.Attribute("Extension").Value == extension)
                {
                    // Return the friendly name if there are no splits - otherwise, return 'Common' as the type is used frequently.
                    return supportedFileTypesElem.Value.Split('|').Length == 1 ? supportedFileTypesElem.Value : "Common Supported Type";
                }
            }

            // Return the extension.
            return (string.IsNullOrEmpty(extension) ? string.Empty : $"{extension.ToUpper().Substring(1)} ") + "File";
        }

        /// <summary>
        /// Parses file types resource to a string.
        /// </summary>
        public static string ParseFileTypesToFilter(string resource)
        {
            // Create StringBuilder to make the filter.
            StringBuilder stringBuilder = new StringBuilder();

            // Load the resource.
            XDocument xml = XDocument.Parse(resource);

            // Clear the dictionary to prevent duplicates.
            Program.FileTypes.Clear();

            foreach (XElement supportedFileTypesElem in xml.Root.Elements("Type"))
            {
                // Store the extension for later.
                string @extension = supportedFileTypesElem.Attribute("Extension") == null ? string.Empty : supportedFileTypesElem.Attribute("Extension").Value;

                if (!string.IsNullOrEmpty(@extension))
                {
                    string[] commonSplit = supportedFileTypesElem.Value.Split('|');
                    string splitFilter = string.Empty;

                    // Common extensions need to be split.
                    foreach (string common in commonSplit)
                    {
                        // Add to the current filter.
                        stringBuilder.Append(splitFilter = $"{common} (*{@extension})|*{@extension}|");
                    }

                    // Add this type to the dictionary so we can easily refer to it later.
                    Program.FileTypes.Add(@extension, splitFilter.Remove(splitFilter.Length - 1));
                }
            }

            return stringBuilder.ToString().EndsWith("|") ? stringBuilder.ToString().Remove(stringBuilder.Length - 1) : stringBuilder.ToString();
        }

        /// <summary>
        /// Parses file types resource to a string based on category.
        /// </summary>
        public static string ParseFileTypeCategory(string resource, string category)
        {
            // Create StringBuilder to make the filter.
            StringBuilder stringBuilder = new StringBuilder();

            // Load the resource.
            XDocument xml = XDocument.Parse(resource);

            // Search for all elements in the requested category or any.
            foreach (XElement supportedFileTypesElem in xml.Root.Elements("Type")
                                                                .Where(x => x.Attribute("Category").Value == category ||
                                                                            x.Attribute("Category").Value == "Any"))
            {
                // Store the extension and category for later.
                string @extension = supportedFileTypesElem.Attribute("Extension") == null ? string.Empty : supportedFileTypesElem.Attribute("Extension").Value;

                if (!string.IsNullOrEmpty(@extension) && !string.IsNullOrEmpty(@category))
                {
                    string[] commonSplit = supportedFileTypesElem.Value.Split('|');
                    string splitFilter = string.Empty;

                    // Common extensions need to be split.
                    foreach (string common in commonSplit)
                    {
                        // Add to the current filter.
                        stringBuilder.Append(splitFilter = $"{common} (*{@extension})|*{@extension}|");
                    }
                }
            }

            return stringBuilder.ToString().Remove(stringBuilder.Length - 1);
        }
    }
}
