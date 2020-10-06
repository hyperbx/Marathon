// FixedFileBase.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * Copyright (c) 2018 Radfordhound
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

namespace Marathon.IO
{
    /// <summary>
    /// A stream helper for hard-coded format classes in Marathon.IO.
    /// </summary>
    public class FixedFileBase
    {
        public string Location;

        public virtual void Load(string file)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException("file");

            if (!File.Exists(file))
                throw new FileNotFoundException("The specified file doesn't exist...", file);

            Location = file;

            using var fileStream = File.OpenRead(file);

            Load(fileStream);
        }

        public virtual void Load(Stream fileStream)
            => throw new NotImplementedException();

        public virtual void Save(string file, bool overwrite = true)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException("file");

            if (!overwrite && File.Exists(file))
                throw new Exception("Unable to save the specified file as it already exists...");

            else if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
            {
                if (file != Location)
                    File.Copy(Location, file, true);
            }

            using var fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

            Save(fileStream);
        }

        public virtual void Save(Stream fileStream)
            => throw new NotImplementedException();
    }
}