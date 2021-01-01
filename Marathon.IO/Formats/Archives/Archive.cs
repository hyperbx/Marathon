// Archive.cs is licensed under the MIT License:
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
using System.Collections.Generic;
using Marathon.Helpers;

namespace Marathon.IO.Formats.Archives
{
    /// <summary>
    /// Determines how archives are streamed.
    /// </summary>
    public enum ArchiveStreamMode
    {
        /// <summary>
        /// Loads only the file index (used for quick loading for contents).
        /// </summary>
        IndexOnly,

        /// <summary>
        /// Copies all data to memory (used for unpacking and repacking).
        /// </summary>
        CopyToMemory
    }

    /// <summary>
    /// <para>File base used for universal archive management.</para>
    /// </summary>
    public class Archive : FileBase
    {
        public ArchiveStreamMode ArchiveStreamMode { get; set; } = ArchiveStreamMode.CopyToMemory;

        public List<ArchiveData> Data = new List<ArchiveData>();

        public Archive() { }

        public Archive(string file, ArchiveStreamMode archiveMode = ArchiveStreamMode.CopyToMemory) : base(file)
        {
            // Set archive loading mode.
            ArchiveStreamMode = archiveMode;
        }

        public Archive(Archive arc, ArchiveStreamMode archiveMode = ArchiveStreamMode.CopyToMemory)
        {
            // Overwrite all local data with the input archive's data.
            Data = arc.Data;

            // Set archive loading mode.
            ArchiveStreamMode = archiveMode;
        }

        /// <summary>
        /// Reloads the archive with virtual instructions from an inherited class.
        /// </summary>
        public virtual Archive Reload()
            => throw new NotImplementedException();

        #region Data Extraction
        /// <summary>
        /// Saves the archive with virtual instructions from an inherited class.
        /// </summary>
        /// <param name="file">Location to save to.</param>
        /// <param name="overwrite">Overwrite the original archive.</param>
        public override void Save(string file, bool overwrite = true)
        {
            // Decompress all files before saving.
            Decompress(ref Data);

            // Save with virtual instructions.
            base.Save(file, overwrite);
        }

        /// <summary>
        /// Decompresses all files in a list of ArchiveData nodes.
        /// </summary>
        /// <param name="files">List of ArchiveData nodes to iterate through and decompress.</param>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public void Decompress(ref List<ArchiveData> files, bool includeSubDirectories = true)
        {
            foreach (ArchiveData data in files)
            {
                // Iterate through directory to decompress further.
                if (includeSubDirectories && data is ArchiveDirectory dir)
                {
                    Decompress(ref dir.Data);
                }

                // Iterate through files and decompress.
                else if (data is ArchiveFile file)
                {
                    file.Data = file.Decompress(Location, file);
                }
            }
        }

        /// <summary>
        /// Extracts all data from the archive.
        /// </summary>
        /// <param name="directory">Directory to extract to.</param>
        public void Extract(string directory)
        {
            // Create root directory to extract to.
            Directory.CreateDirectory(directory);

            foreach (var entry in Data)
            {
                // Extract with virtual instructions.
                entry.Extract(Path.Combine(directory, entry.Name));
            }
        }
        #endregion

        #region Data Iteration
        /// <summary>
        /// Jumps to a directory based on a given path.
        /// </summary>
        /// <param name="dirPath">Path to jump to.</param>
        /// <param name="@base">Directory to jump from (uses root if null).</param>
        public ArchiveDirectory JumpToDirectory(string dirPath, ArchiveDirectory @base = null)
        {
            ArchiveDirectory dir = null;

            foreach (string dirName in dirPath.Split('\\'))
            {
                // Set based on iteration.
                List<ArchiveData> data = dir == null ?
                                         @base == null ? ((ArchiveDirectory)Data[0]).Data : @base.Data :
                                         dir.Data;

                // Navigate to the directory if it exists already.
                if (data.Exists(t => t.Name == dirName))
                {
                    dir = data.Find(t => t.Name == dirName) as ArchiveDirectory;
                }
            }

            return dir;
        }

        /// <summary>
        /// Jumps to a file based on a given path.
        /// </summary>
        /// <param name="dirPath">File to jump to.</param>
        /// <param name="@base">Directory to jump from (uses root if null).</param>
        public ArchiveFile JumpToFile(string dirPath, ArchiveDirectory @base = null)
        {
            ArchiveDirectory dir = null;
            ArchiveFile file = null;

            foreach (string dirName in dirPath.Split('\\'))
            {
                // Set based on iteration.
                List<ArchiveData> data = dir == null ?
                                         @base == null ? ((ArchiveDirectory)Data[0]).Data : @base.Data :
                                         dir.Data;

                // Navigate to the directory if it exists already.
                if (data.Exists(t => t.Name == dirName))
                {
                    ArchiveData result = data.Find(t => t.Name == dirName);

                    if (result.IsDirectory)
                    {
                        dir = result as ArchiveDirectory;
                    }
                    else
                    {
                        file = result as ArchiveFile;
                    }
                }
            }

            return file;
        }

        /// <summary>
        /// Returns all files from the input list.
        /// </summary>
        /// <param name="files">List of ArchiveData nodes to iterate through and decompress.</param>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public static List<ArchiveFile> GetFiles(List<ArchiveData> files, bool includeSubDirectories = true)
        {
            // List used to store the files to be returned.
            List<ArchiveFile> list = new List<ArchiveFile>();

            foreach (ArchiveData data in files)
            {
                // Iterate through directory to gather further.
                if (includeSubDirectories && data is ArchiveDirectory dir)
                {
                    list.AddRange(GetFiles(dir.Data));
                }

                // Iterate through files and gather them.
                else if (data is ArchiveFile file)
                {
                    list.Add(file);
                }
            }

            return list;
        }

        /// <summary>
        /// Returns all files from the input list - used for recursion.
        /// </summary>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public List<ArchiveFile> GetFiles(bool includeSubDirectories = true)
            => GetFiles(Data, includeSubDirectories);

        /// <summary>
        /// Recursively gathers all directories from the current list.
        /// </summary>
        /// <param name="directories">List of entries to gather from.</param>
        public static List<ArchiveDirectory> GetDirectories(List<ArchiveData> directories)
        {
            List<ArchiveDirectory> list = new List<ArchiveDirectory>();

            foreach (ArchiveData data in directories)
            {
                if (data is ArchiveDirectory dir)
                {
                    list.AddRange(GetDirectories(dir.Data));
                }
            }

            return list;
        }
        #endregion

        #region Data Addition
        /// <summary>
        /// Adds all files from the requested path to the archive.
        /// </summary>
        /// <param name="directoryPath">Directory on disk to add from.</param>
        /// <param name="base">Directory these nodes should be added to in the archive.</param>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public void AddDirectory(string directoryPath, ArchiveData @base, bool includeSubDirectories = false)
        {
            if (@base is ArchiveDirectory dir)
            {
                // Recursively add files from disk to the archive.
                dir.Data.AddRange(GetFilesFromDirectory(directoryPath, includeSubDirectories));
            }
            else
            {
                throw new NotSupportedException
                (
                    $"Encountered an archive entry of unsupported type ({@base.GetType()}). " +
                    "The input *must* be an ArchiveDirectory."
                );
            }
        }

        /// <summary>
        /// Adds all files from the requested path to the archive - used for recursion.
        /// </summary>
        /// <param name="directoryPath">Directory on disk to add from.</param>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public void AddDirectory(string directoryPath, bool includeSubDirectories = false)
            => Data.AddRange(GetFilesFromDirectory(directoryPath, includeSubDirectories));

        /// <summary>
        /// Converts all files from the requested path to ArchiveData nodes.
        /// </summary>
        /// <param name="directoryPath">Directory on disk to add from.</param>
        /// <param name="includeSubDirectories">Include subdirectories when iterating.</param>
        public List<ArchiveData> GetFilesFromDirectory(string directoryPath, bool includeSubDirectories = false)
        {
            ArchiveDirectory dir = new ArchiveDirectory(Path.GetFileName(directoryPath));

            // Add each file from the current directory.
            foreach (string filePath in Directory.GetFiles(directoryPath))
            {
                ArchiveFile file;

                if (ArchiveStreamMode == ArchiveStreamMode.CopyToMemory)
                {
                    // Add file and copy to memory.
                    file = new ArchiveFile(filePath) { Parent = dir };
                }
                else
                {
                    // Add file explicitly without gathering its data.
                    file = new ArchiveFile()
                    {
                        Name = Path.GetFileName(filePath),
                        Parent = dir,
                        Location = filePath,
                        UncompressedSize = (uint)new FileInfo(filePath).Length
                    };
                }

                dir.Data.Add(file);
            }

            // Repeat for each sub directory.
            if (includeSubDirectories)
            {
                // Add each subdirectory from the requested path.
                foreach (string subDir in Directory.GetDirectories(directoryPath))
                {
                    dir.Data.Add
                    (
                        new ArchiveDirectory(Path.GetFileName(subDir))
                        {
                            Parent = dir,
                            Data = GetFilesFromDirectory(subDir, includeSubDirectories)
                        }
                    );
                }
            }

            return dir.Data;
        }

        /// <summary>
        /// Recursively creates new subdirectories based on a path.
        /// </summary>
        /// <param name="dirPath">Path to create.</param>
        public ArchiveDirectory CreateDirectories(string dirPath)
        {
            ArchiveDirectory dir = null;

            foreach (string dirName in dirPath.Split('\\'))
            {
                // Set based on iteration.
                List<ArchiveData> data = dir == null ? Data : dir.Data;

                // Navigate to the directory if it exists already.
                if (data.Exists(t => t.Name == dirName))
                {
                    dir = data.Find(t => t.Name == dirName) as ArchiveDirectory;
                }

                // Create the first directory, since it doesn't exist.
                else if (dir == null)
                {
                    // Create new directory based on the current split.
                    ArchiveDirectory directory = new ArchiveDirectory(dirName);

                    // Add this directory to the data.
                    Data.Add(directory);

                    // Set next iteration to this new directory.
                    dir = directory;
                }

                // Create the subdirectories.
                else
                {
                    // Create new subdirectory based on the current split.
                    ArchiveDirectory directory = new ArchiveDirectory(dirName)
                    {
                        Parent = dir
                    };

                    // Add this directory to the parent data.
                    dir.Data.Add(directory);

                    // Set next iteration to this new directory.
                    dir = directory;
                }
            }

            return dir;
        }
        #endregion

        #region Data Subtraction
        /// <summary>
        /// Replaces a file with another.
        /// </summary>
        /// <param name="original">Original file to replace.</param>
        /// <param name="replacement">Replacement file to use.</param>
        public void ReplaceFile(ArchiveFile original, ArchiveFile replacement)
        {
            // Store parent of original file so we know where to replace.
            ArchiveDirectory parent = (ArchiveDirectory)original.Parent;

            // Store index of original file so we know where to insert.
            int originalIndex = parent.Data.IndexOf(original);

            if (parent != null)
            {
                // Remove original file.
                parent.Data.Remove(original);

                // Insert replacement file at original index.
                parent.Data.Insert(originalIndex, replacement);
            }
        }
        #endregion
    }

    public abstract class ArchiveData
    {
        /// <summary>
        /// The name pertaining to this entry.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parent node pertaining to this entry.
        /// </summary>
        public ArchiveData Parent = null;

        /// <summary>
        /// Determines whether this entry is a directory or not.
        /// </summary>
        public abstract bool IsDirectory { get; }

        /// <summary>
        /// Extracts this node with virtual instructions from an inherited class.
        /// </summary>
        /// <param name="placeholder">To be determined.</param>
        public virtual void Extract(string placeholder)
            => throw new NotImplementedException();

        #region Totals
        /// <summary>
        /// Gets the total number of data in this directory (including subdirectories).
        /// </summary>
        /// <param name="dataEntries">List of entries to query.</param>
        public static int GetTotalCount(List<ArchiveData> dataEntries)
        {
            // Total count for this directory.
            int totalCount = dataEntries.Count;

            foreach (var dataEntry in dataEntries)
            {
                if (dataEntry is ArchiveDirectory childDirEntry)
                {
                    // When we call TotalContentsCount, we recurse again for the child directory.
                    totalCount += childDirEntry.TotalContentsCount;
                }
            }

            return totalCount;
        }

        /// <summary>
        /// Gets the total size of data in this directory (including subdirectories).
        /// </summary>
        /// <param name="dataEntries">List of entries to query.</param>
        public static uint GetTotalSize(List<ArchiveData> dataEntries)
        {
            uint totalSize = 0;

            foreach (var dataEntry in dataEntries)
            {
                if (dataEntry is ArchiveDirectory childDirEntry)
                {
                    // When we call TotalContentsSize, we recurse again for the child directory.
                    totalSize += childDirEntry.TotalContentsSize;
                }
                else if (dataEntry is ArchiveFile fileEntry)
                {
                    // Add uncompressed size to total.
                    totalSize += fileEntry.UncompressedSize;
                }
            }

            return totalSize;
        }
        #endregion
    }

    public class ArchiveFile : ArchiveData
    {
        /// <summary>
        /// The data pertaining to this file.
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// The size of the file (used if <see cref="Data"/> isn't populated).
        /// </summary>
        public uint Length;

        /// <summary>
        /// The uncompressed size of this file.
        /// </summary>
        public uint UncompressedSize;

        /// <summary>
        /// The offset where the data is stored (used when seeking data with <see cref="ArchiveStreamMode.CopyToMemory"/> mode).
        /// </summary>
        public uint Offset;

        /// <summary>
        /// The physical location of the file (used when importing files with <see cref="ArchiveStreamMode.IndexOnly"/> mode).
        /// </summary>
        public string Location;

        // This should be false, since it's a file, duh.
        public override bool IsDirectory
            => false;

        public ArchiveFile() { }

        public ArchiveFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The given file could not be found.", filePath);

            // Get the name of the file on disk.
            Name = new FileInfo(filePath).Name;

            // Get the file's data on disk.
            Data = File.ReadAllBytes(filePath);

            // Get the size of the data.
            UncompressedSize = (uint)Data.Length;
        }

        public ArchiveFile(ArchiveFile file, bool keepOffset = true)
        {
            // Set the name of the file.
            Name = file.Name;

            // Set the parent of the file.
            Parent = file.Parent;

            // Set the data of the file.
            Data = file.Data;

            // Set the length of the file.
            Length = file.Length;

            // Set the uncompressed size of the file.
            UncompressedSize = file.UncompressedSize;

            // Set the offset of the file if requested.
            if (keepOffset)
                Offset = file.Offset;

            // Set the physical location of the file.
            Location = file.Location;
        }

        public ArchiveFile(string name, byte[] data)
        {
            // Set the name of the file.
            Name = name;

            // Set the data of the file.
            Data = data;
        }

        /// <summary>
        /// Decompresses the file data using the overridden method.
        /// </summary>
        public virtual byte[] Decompress(string archive, ArchiveFile file)
        {
            // Archive is new.
            if (!File.Exists(archive))
                return file.Data;

            // File is stored in memory or in an archive.
            if (string.IsNullOrEmpty(Location))
            {
                // Check if the lengths are valid.
                if (file.Length != 0 && file.UncompressedSize != 0)
                {
                    // Write feedback to the console output.
                    ConsoleWriter.WriteLine($"Decompressing {file.Name} ({StringHelper.ByteLengthToDecimalString(file.UncompressedSize)})...");

                    // Decompress with virtual instructions.
                    using (var fileStream = File.OpenRead(archive))
                        return Decompress(fileStream, file);
                }

                // File is probably already decompressed.
                else
                {
                    return file.Data;
                }
            }

            // File is stored on disk, so read it.
            else if (!string.IsNullOrEmpty(Location) && Data == null)
            {
                return File.ReadAllBytes(Location);
            }

            // File is probably already decompressed.
            else
            {
                return file.Data;
            }
        }

        /// <summary>
        /// Decompresses the file data using the overridden method.
        /// </summary>
        public virtual byte[] Decompress(Stream stream, ArchiveFile file)
            => throw new NotImplementedException();

        /// <summary>
        /// Extracts file data from the archive.
        /// </summary>
        /// <param name="filePath">Location to extract to.</param>
        public override void Extract(string filePath)
            => File.WriteAllBytes(filePath, Data);
    }

    public class ArchiveDirectory : ArchiveData
    {
        /// <summary>
        /// The subsequent data contained in this directory.
        /// </summary>
        public List<ArchiveData> Data = new List<ArchiveData>();

        /// <summary>
        /// Total amount of entries in this directory (including subdirectories).
        /// </summary>
        public int TotalContentsCount
            => GetTotalCount(Data);

        /// <summary>
        /// Total size of entries in this directory (including subdirectories).
        /// </summary>
        public uint TotalContentsSize
            => GetTotalSize(Data);

        /// <summary>
        /// Determines if the current directory is the root of the archive.
        /// </summary>
        public bool IsRoot;

        // Yes, the class called 'ArchiveDirectory' is a directory.
        public override bool IsDirectory
            => true;

        public ArchiveDirectory() { }

        public ArchiveDirectory(string directoryName)
            => Name = directoryName;

        /// <summary>
        /// Extracts all data from the current directory.
        /// </summary>
        /// <param name="directory">Directory to extract to.</param>
        public override void Extract(string directory)
        {
            // Create subdirectory to extract to.
            Directory.CreateDirectory(directory);

            foreach (ArchiveData data in Data)
            {
                // Extract the data to the directory.
                data.Extract(Path.Combine(directory, data.Name));
            }
        }
    }
}