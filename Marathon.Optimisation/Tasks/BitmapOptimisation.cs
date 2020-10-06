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
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Marathon.Optimisation.Tasks
{
    class BitmapOptimisation
    {
        /// <summary>
        /// Replaces all instances of .NET resources with cached bitmaps.
        /// </summary>
        /// <param name="file">Designer file.</param>
        /// <param name="dotNet">Use .NET resources.</param>
        public static void SetResourceType(string file, bool dotNet)
        {
            bool BitmapFlag = false;
            List<string> DesignerCode = File.ReadAllLines(file).ToList();

            for (int i = 0; i < DesignerCode.Count; i++)
            {
                // Strings used by .NET resources and Marathon resources.
                string resourceString = dotNet ? "Resources.LoadBitmapResource(\""
                                               : "global::Marathon.Toolkit.Properties.Resources.";

                if (DesignerCode[i].Contains("Image") && DesignerCode[i].Contains(resourceString))
                {
                    // File uses bitmaps!
                    BitmapFlag = true;

                    // Split the property.
                    string[] splitProperty = DesignerCode[i].Split('=');

                    // Get resource name from reference.
                    string resourceName = splitProperty[1].Substring(1).Replace(resourceString, string.Empty);

                    // Replace the property with the requested resource type.
                    splitProperty[1] = dotNet ? $"global::Marathon.Toolkit.Properties.Resources.{resourceName.Remove(resourceName.Length - 3)};"
                                              : $"Resources.LoadBitmapResource(\"{resourceName.Remove(resourceName.Length - 1)}\");";

                    // Join the string together and replace.
                    DesignerCode[i] = string.Join("= ", splitProperty);

                    // Report feedback.
                    Console.WriteLine("Set bitmap type to " + (dotNet ? ".NET Resource" : "Marathon") + $" on line {i + 1} in designer file -> {file}");
                }
            }

            if (BitmapFlag)
                File.WriteAllLines(file, DesignerCode);
        }
    }
}
