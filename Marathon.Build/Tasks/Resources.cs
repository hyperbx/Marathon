// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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
using Marathon.Helpers;

namespace Marathon.Build.Tasks
{
    class Resources
    {
        /// <summary>
        /// Updates the Resources code generator for the input project for designer support.
        /// </summary>
        /// <param name="source">Source folder for the latest Resources.resx file.</param>
        /// <param name="project">Project folder to update.</param>
        public static void UpdateResources(string source, string project)
        {
            string sourceRESX  = GetResourceRESX(source),
                   projectRESX = GetResourceRESX(project);

            // Source RESX file was located, so perform the update.
            if (File.Exists(sourceRESX))
            {
                // Copy and overwrite the project Resource code generator.
                File.Copy(sourceRESX, projectRESX, true);

                File.WriteAllText
                (
                    projectRESX,

                    /* Redirect all resource references to Marathon.Resources.
                       I know, doing a string.Replace() is probably not the wisest choice, but there are no other occurrences like this anyway. */
                    File.ReadAllText(projectRESX).Replace
                    (
                        @"    <value>..\resources\",
                        @"    <value>..\..\Marathon.Resources\Resources\",
                        StringComparison.OrdinalIgnoreCase
                    )
                );

                // Report feedback.
                Console.WriteLine($"Updated Resource code generator -> {projectRESX}");
            }

            // Abort task.
            else
            {
                // Report feedback.
                Console.WriteLine("Resource code generator not found! Aborting...");

                return;
            }
        }

        /// <summary>
        /// Combines common strings to locate the Resources.resx file.
        /// </summary>
        /// <param name="path">Folder to combine.</param>
        private static string GetResourceRESX(string path)
            => Path.Combine(path, "Properties", "Resources.resx");
    }
}
