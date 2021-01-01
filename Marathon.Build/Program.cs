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
using Marathon.Build.Tasks;

namespace Marathon.Build
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                switch (args[0])
                {
                    case "--bitmap-cache":
                    {
                        foreach (string designer in Directory.GetFiles(args.Last(), "*.Designer.cs", SearchOption.AllDirectories))
                        {
                            // Optimise designer code to use the requested type for bitmaps.
                            Optimisation.SetBitmapResourceType(args.Last(), designer, args[1] == "/dotNet");
                        }

                        break;
                    }

                    case "--update-resources":
                    {
                        // Update the Resources code generator for the input project path.
                        Resources.UpdateResources(args[1], args.Last());

                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine
                (
                    "Marathon Build\n\n" +
                    "" +
                    "WARNING: this tool is designed to be used during compile time!\n\n" +
                    "" +
                    "Usage:\n" +
                    "Marathon.exe [parameters] \"C:\\Directory\"\n\n" +
                    "" +
                    "Parameters:\n" +
                    "--bitmap-cache [project] - optimise bitmaps to use Marathon resources.\n" +
                    "       /dotNet - revert optimisations and use .NET resources for bitmaps.\n" +
                    "--update-resources [source] [project] - updates the Resources code generator."
                );
            }
        }
    }
}
