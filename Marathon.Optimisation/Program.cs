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
using Marathon.Optimisation.Tasks;

namespace Marathon.Optimisation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                switch (args[0])
                {
                    case "--gdi-cache":
                    {
                        // Optimise designer code to use cached resources for Bitmaps.
                        foreach (string designer in Directory.GetFiles(args.Last(), "*.Designer.cs", SearchOption.AllDirectories))
                        {
                            BitmapOptimisation.UseCachedResources(designer);
                        }

                        break;
                    }
                }
            }
            else
                Console.WriteLine("Marathon Optimisation\n\n" +
                                  "" +
                                  "WARNING: this tool is designed to be used pre-compile time!\n\n" +
                                  "" +
                                  "Usage:\n" +
                                  "Marathon.exe [parameters] \"C:\\Directory\"\n\n" +
                                  "" +
                                  "Parameters:\n" +
                                  "--gdi-cache - optimise GDI+ Bitmaps to use cached resources.");
        }
    }
}
