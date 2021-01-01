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

using System.Collections.Generic;

namespace Marathon.Modding
{
    public class Mod
    {
        public string Title,       // The name of this mod.
                      Version,     // The version of this mod.
                      Date,        // The date this mod was created on.
                      Author,      // The author of this mod.
                      Description, // The full description of this mod.
                      Location;    // The location of this mod's configuration file.

        public PlatformType System; // The system supported by this mod.

        public List<string> Patches = new List<string>(); // A list of patches required by this mod.

        public bool Merge,            // Determines if the mod merges with others.
                    SaveRedirect,     // Determines if the mod redirects save data.
                    CustomFilesystem; // Determines if the mod uses a custom filesystem.

        public List<string> ReadOnly = new List<string>(), // A list of read-only archives.
                            Custom = new List<string>();   // A list of custom files.
    }
}
