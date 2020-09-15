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
using System.Collections.Generic;

namespace Marathon.Optimisation.Tasks
{
    class BitmapOptimisation
    {
        /// <summary>
        /// Replaces all instances of .NET resources with cached bitmaps.
        /// </summary>
        /// <param name="file">Designer file.</param>
        public static void UseCachedResources(string file)
        {
            bool BitmapFlag = false;
            List<string> DesignerCode = File.ReadAllLines(file).ToList();

            for (int i = 0; i < DesignerCode.Count; i++)
            {
                // Typical string used by .NET for referencing internal resources.
                string netResourceString = "global::Marathon.Toolkit.Properties.Resources.";

                if (DesignerCode[i].Contains("Image") && DesignerCode[i].Contains(netResourceString))
                {
                    // File uses GDI+ bitmaps, ring the alarm bells!
                    BitmapFlag = true;

                    // Split the property.
                    string[] splitProperty = DesignerCode[i].Split('=');

                    // Get resource name from reference.
                    string resourceName = splitProperty[1].Substring(1).Replace(netResourceString, string.Empty);

                    // Replace the .NET resources property with the cached resource type.
                    splitProperty[1] = $"Resources.LoadBitmapResource(\"{resourceName.Remove(resourceName.Length - 1)}\");";

                    // Join the string together and replace.
                    DesignerCode[i] = string.Join("= ", splitProperty);

                    // Report feedback.
                    Console.WriteLine($"Optimised bitmap on line {i + 1} in designer file -> {file}");
                }
            }

            if (BitmapFlag)
                File.WriteAllLines(file, DesignerCode);
        }

        /// <summary>
        /// Replaces all instances of cached bitmaps with .NET resources.
        /// </summary>
        /// <param name="file"></param>
        public static void RevertCachedResources(string file)
        {
            bool BitmapFlag = false;
            List<string> DesignerCode = File.ReadAllLines(file).ToList();

            for (int i = 0; i < DesignerCode.Count; i++)
            {
                // Typical string used by .NET for referencing internal resources.
                string bitmapResourceString = "Resources.LoadBitmapResource(\"";

                if (DesignerCode[i].Contains("Image") && DesignerCode[i].Contains(bitmapResourceString))
                {
                    // File uses cached bitmaps.
                    BitmapFlag = true;

                    // Split the property.
                    string[] splitProperty = DesignerCode[i].Split('=');

                    // Get resource name from reference.
                    string resourceName = splitProperty[1].Substring(1).Replace(bitmapResourceString, string.Empty);

                    // Replace the .NET resources property with the cached resource type.
                    splitProperty[1] = $"global::Marathon.Toolkit.Properties.Resources.{resourceName.Remove(resourceName.Length - 3)};";

                    // Join the string together and replace.
                    DesignerCode[i] = string.Join("= ", splitProperty);

                    // Report feedback.
                    Console.WriteLine($"Reverted cached bitmap on line {i + 1} in designer file -> {file}");
                }
            }

            if (BitmapFlag)
                File.WriteAllLines(file, DesignerCode);
        }
    }
}
