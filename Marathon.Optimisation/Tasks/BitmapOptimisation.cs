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
using System.Collections.Generic;

namespace Marathon.Optimisation.Tasks
{
    class BitmapOptimisation
    {
        public static void UseCachedResources(string file)
        {
            List<string> DesignerCode = File.ReadAllLines(file).ToList();

            foreach (string line in DesignerCode)
            {
                string netResourceString = "global::Marathon.Toolkit.Properties.Resources.";

                if (line.Contains("Image") && line.Contains(netResourceString))
                {
                    string[] splitProperty = line.Split('=');

                    // Replace the .NET resources property with the cached resource type.
                    splitProperty[1] = $"Resources.LoadBitmapResource({splitProperty[1].Replace(netResourceString, string.Empty).Remove(netResourceString.Length - 1)});";
                }
            }
        }
    }
}
