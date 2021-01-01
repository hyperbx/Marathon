// FileBase.cs is licensed under the MIT License:
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
    /// Determines how files are streamed.
    /// </summary>
    public enum FileWriteMode
    {
        /// <summary>
        /// Writes to the file directly (used for formats with hard-coded values).
        /// </summary>
        Fixed,

        /// <summary>
        /// Writes the entire file from scratch (used for reverse-engineering whole formats).
        /// </summary>
        Logical
    }

    /// <summary>
    /// A stream helper for format classes in Marathon.IO.
    /// </summary>
    public class FileBase
    {
        /// <summary>
        /// Location of the file loaded.
        /// </summary>
        public string Location;

        /// <summary>
        /// Determines what mode will be used for the stream.
        /// </summary>
        public FileWriteMode FileWriteMode { get; set; } = FileWriteMode.Logical;

        public FileBase() { }

        public FileBase(string file, FileWriteMode writeMode = FileWriteMode.Logical)
        {
            // Set file writing mode.
            FileWriteMode = writeMode;

            // Load the file immediately.
            Load(file);
        }

        /// <summary>
        /// Prepares the file for stream reading.
        /// </summary>
        /// <param name="file">Location of file to load.</param>
        public virtual void Load(string file)
        {
            // The file path was null...
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException("file");

            // The file doesn't exist...
            if (!File.Exists(file))
                throw new FileNotFoundException("The specified file doesn't exist...", file);

            // Set loaded location.
            Location = file;

            // Open the file and read.
            using (var fileStream = File.OpenRead(file))
                Load(fileStream);
        }

        /// <summary>
        /// Loads the file from pre-streamed data.
        /// </summary>
        /// <param name="fileStream">Stream to use.</param>
        public virtual void Load(Stream fileStream)
            => throw new NotImplementedException();

        /// <summary>
        /// Prepares the file for stream writing.
        /// </summary>
        /// <param name="file">Location to write to.</param>
        /// <param name="overwrite">Overwrite the original file.</param>
        public virtual void Save(string file, bool overwrite = true)
        {
            // The file path was null...
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException("file");

            // The file already exists and we've been told explicitly not to overwrite...
            if (!overwrite && File.Exists(file))
                throw new Exception("Unable to save the specified file as it already exists...");

            switch (FileWriteMode)
            {
                case FileWriteMode.Logical:
                {
                    // Create the file using the stream.
                    using (var fileStream = File.Create(file))
                        Save(fileStream);

                    break;
                }

                case FileWriteMode.Fixed:
                {
                    if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
                    {
                        // Don't bother copying if it's the same location.
                        if (file == Location)
                            return;

                        // Copy the fixed file to the new writing location.
                        File.Copy(Location, file, true);
                    }

                    // Write to a pre-existing file directly.
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                        Save(fileStream);

                    break;
                }
            }
        }

        /// <summary>
        /// Saves the file from pre-streamed data.
        /// </summary>
        /// <param name="fileStream">Stream to use.</param>
        public virtual void Save(Stream fileStream)
            => throw new NotImplementedException();
    }
}