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
using MoonSharp.Interpreter;

namespace Marathon.Modding
{
    public class Patch
    {
        public string Title,       // The name of this patch.
                      Author,      // The author of this patch.
                      Blurb,       // The short description of this patch.
                      Description, // The full description of this patch.
                      Location;    // The location of this patch.

        public PlatformType System; // The system supported by this mod.
    }

    public class Interpreter
    {
        public static void Run(Patch patch)
        {
            string lua = File.ReadAllText(patch.Location);

            // TODO
        }

        /// <summary>
        /// Gets the information from a patch script.
        /// </summary>
        /// <param name="lua">Lua script to interpret.</param>
        public static void GetInformation(Patch patch, string lua)
        {
            Script script = new Script();

            // Gather information from the patch script.
            script.Globals["Title"] = (Func<string, string>)(x => patch.Title = x);
            script.Globals["Author"] = (Func<string, string>)(x => patch.Author = x);
            script.Globals["Platform"] = (Func<string, string>)(x => patch.Blurb = x); // TODO: actually make this work.
            script.Globals["Blurb"] = (Func<string, string>)(x => patch.Blurb = x);
            script.Globals["Description"] = (Func<string, string>)(x => patch.Description = x);

            script.DoString(lua);
        }

        /// <summary>
        /// Callback function that loads an archive into memory.
        /// </summary>
        /// <param name="filePath">Archive to load.</param>
        public static void BeginBlock(string filePath)
        {
            // TODO: load the archive into memory.
        }

        /// <summary>
        /// Callback function that repacks an archive from memory.
        /// </summary>
        /// <param name="filePath">Archive to repack to.</param>
        public static void EndBlock(string filePath)
        {
            // TODO: repack the archive from memory.
        }
    }
}
