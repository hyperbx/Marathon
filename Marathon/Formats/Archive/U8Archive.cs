using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Interfaces;

namespace Marathon.Formats.Archive
{
    public class U8Archive : FileBase, IArchive
    {
        public U8Archive() : base(true) { }

        public U8Archive(string file, ReadMode readMode = ReadMode.IndexOnly) : base(file, readMode: readMode, leaveOpen: true) { }

        public U8Archive(string path, bool includeSubdirectories = false, CompressionLevel compressionLevel = CompressionLevel.Optimal) : base(true)
        {
            Root.Data = GetFilesFromDirectory(path, includeSubdirectories);
            CompressionLevel = compressionLevel;
        }

        IArchiveDirectory IArchive.Root => Root;

        public U8ArchiveDirectory Root { get; private set; } = new U8ArchiveDirectory();

        public CompressionLevel CompressionLevel { get; set; }

        public override bool LeaveOpen { get; set; } = true;

        public override object Signature { get; } = 0x55AA382D;

        public override string Extension { get; } = ".arc";

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream, true);

            // Read the signature.
            reader.ReadSignature((uint)(int)Signature);

            // Size of each entry in the table.
            uint entrySize = 16;

            // Offset to the entry table.
            uint entriesOffset = reader.ReadUInt32();

            // Length of the entry table.
            uint entriesLength = reader.ReadUInt32();

            // Offset to the data for the first file.
            uint dataOffset = reader.ReadUInt32();

            // Read U8 root entry.
            reader.JumpTo(entriesOffset);
            U8ArchiveFile root = new(reader);

            // Compute string table offset.
            uint stringTableOffset = entriesOffset + (root.Length * entrySize);

            // Create U8 entries list and add the root entry to it.
            var u8Entries = new U8ArchiveFile[root.Length];
            u8Entries[0] = root;

            // Parse U8 child entries.
            for (uint i = 1; i < root.Length; ++i)
                u8Entries[i] = new U8ArchiveFile(reader);

            // Parse U8 entries into generic archive data.
            ParseEntries
            (
                0,

                new U8ArchiveDirectory()
                {
                    // Use archive data list for recursive.
                    Data = Root.Data
                },

                true
            );

            uint ParseEntries(uint u8EntryIndex, U8ArchiveDirectory entries, bool isRoot = false)
            {
                ref U8ArchiveFile u8Entry = ref u8Entries[u8EntryIndex];

                // Read entry name.
                reader.JumpTo(stringTableOffset + u8Entry.NameOffset);
                string name = reader.ReadNullTerminatedString();

                switch (u8Entry.Type)
                {
                    case U8DataType.Directory:
                    {
                        var directory = new U8ArchiveDirectory
                        {
                            Name = name,
                            Parent = entries
                        };

                        // Add directory to the current entries.
                        entries.Data.Add(directory);

                        // Set root directory.
                        if (isRoot)
                            Root = directory;

                        // Use current directory for entry list.
                        entries = directory;

                        uint u8ChildIndex = ++u8EntryIndex;

                        // Parse current directory's child entries.
                        while (u8ChildIndex < u8Entry.Length)
                            u8ChildIndex = ParseEntries(u8ChildIndex, entries);

                        // Return index of the next entry.
                        return u8Entry.Length;
                    }

                    case U8DataType.File:
                    {
                        var file = new U8ArchiveFile()
                        {
                            Name = name,
                            Parent = entries,
                            Offset = u8Entry.Offset,
                            Length = u8Entry.Length,
                            UncompressedSize = u8Entry.UncompressedSize,
                            FileReader = reader
                        };

                        // Decompress file if requested.
                        if (ReadMode == ReadMode.CopyToMemory)
                            file.Decompress();

                        // Add file to the current entries.
                        entries.Data.Add(file);

                        // Return the index of the next entry.
                        return ++u8EntryIndex;
                    }

                    default:
                        throw new NotSupportedException($"Encountered a U8 entry with an unsupported type ({(uint)u8Entry.Type:X}).");
                }
            }
        }

        public override void Save(Stream stream)
        {
            /* TODO: Sort data entries before writing!! This is very important actually
               since iirc the game relies on sorting to find U8 entries more quickly.
               Not doing this may result in game crashes in some cases!
               Search for "sort" here for more info: http://wiki.tockdom.com/wiki/U8_(File_Format) */

            // Create ExtendedBinaryWriter.
            var writer = new BinaryWriterEx(stream, true);

            // Write archive signature.
            writer.Write((uint)(int)Signature);

            // Store the offset locations for later when we fill out the entries.
            writer.AddOffset("EntriesOffset");
            writer.AddOffset("EntriesLength");
            writer.AddOffset("DataOffset");

            /* Write unknown values.
             
               We have to set at least one of these to something non-zero for compatibillity
               with HedgeArcPack, which unfortunately has no other real way of telling if a
               given archive is a standard U8 archive, or a Zlib U8 archive.

               There's nothing special about these constants; they can be anything as long as at
               least one of them is non-zero. I just picked these because ArcTool uses them too.

               TODO: Figure out what these values are.
               The third uint here seems like it's in little endian?? */
            if (CompressionLevel != CompressionLevel.NoCompression)
            {
                writer.Write(0xE4F91200U);
                writer.Write(0x00000402U);
                writer.WriteNulls(8);
            }
            else
            {
                writer.WriteNulls(8);
                writer.Write(0xD03D6D01U);
                writer.Write(0x00006301U);
            }

            // Fill in the offset for where the table starts.
            writer.FillOffset("EntriesOffset");

            uint globalEntryIndex = 0, strTableLen = 0;
            bool hasData = false;

            // Write entries recursively.
            WriteEntries(Root);

            // Write entry names recursively.
            WriteEntryNames(Root);

            // Fill-in EntriesLength.
            writer.FillOffset("EntriesLength", (uint)stream.Position - 0x20);

            // Align the file to an offset divisible by 32.
            if (hasData)
                writer.FixPadding(32);

            // Fill-in DataOffset.
            writer.FillOffset("DataOffset");

            if (hasData)
            {
                globalEntryIndex = 0;

                // Write entry data recursively.
                WriteEntryData(Root);
            }

            void WriteEntries(IArchiveData dataEntry, uint parentIndex = 0)
            {
                // Get U8 entry type.
                var u8EntryType = dataEntry.IsDirectory() ? U8DataType.Directory : U8DataType.File;

                // Write U8 entry flags.
                writer.Write(((uint)u8EntryType << 24) | strTableLen);

                // Increase string table length.
                strTableLen += string.IsNullOrEmpty(dataEntry.Name) ? 1 : (uint)Encoding.UTF8.GetByteCount(dataEntry.Name) + 1;

                if (dataEntry.IsDirectory())
                {
                    var dirEntry = (IArchiveDirectory)dataEntry;

                    if (dirEntry.Parent == null)
                    {
                        // Root node has no parent.
                        writer.WriteNulls(4);

                        // Increase global entry index.
                        ++globalEntryIndex;

                        // Write the number of nodes in the archive.
                        writer.Write((uint)Root.GetTotalCount() + 1);
                    }
                    else
                    {
                        // Write parent index.
                        writer.Write(parentIndex);

                        // Set parent index to the index of the current directory entry.
                        parentIndex = globalEntryIndex;

                        // Increase global entry index.
                        ++globalEntryIndex;

                        // Write next directory index.
                        writer.Write(globalEntryIndex + (uint)dirEntry.GetTotalCount());
                    }

                    // TODO: Figure out what this is; it's only present in '06 archives.
                    writer.WriteNulls(4);

                    // Write directory contents recursively.
                    foreach (var childEntry in dirEntry)
                        WriteEntries(childEntry, parentIndex);
                }
                else
                {
                    var fileEntry = (IArchiveFile)dataEntry;

                    /* Store the locations of the data offset and file compressed
                       size for later when we write the file's data. */
                    writer.AddOffset($"Data_{globalEntryIndex}");

                    if (CompressionLevel != CompressionLevel.NoCompression)
                    {
                        // Write the file's compressed size.
                        writer.AddOffset($"CompressedSize_{globalEntryIndex}");
                    }
                    else
                    {
                        // Write the file's uncompressed size.
                        writer.Write(fileEntry.UncompressedSize);
                    }

                    // Write the file's uncompressed size.
                    writer.AddOffset($"UncompressedSize_{globalEntryIndex}");

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Indicate that this archive does, in fact, contain actual data.
                    hasData = true;
                }
            }

            void WriteEntryNames(IArchiveData dataEntry)
            {
                // Write entry name.
                if (!string.IsNullOrEmpty(dataEntry.Name))
                {
                    writer.WriteNullTerminatedString(dataEntry.Name);
                }
                else
                {
                    writer.WriteNull();
                }

                // Recurse through directory entry contents.
                if (dataEntry.IsDirectory())
                {
                    var dirEntry = (IArchiveDirectory)dataEntry;

                    foreach (var childEntry in dirEntry)
                        WriteEntryNames(childEntry);
                }
            }

            void WriteEntryData(IArchiveData dataEntry)
            {
                if (dataEntry.IsDirectory())
                {
                    var dirEntry = (IArchiveDirectory)dataEntry;

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Write entry data recursively.
                    foreach (var childEntry in dirEntry)
                        WriteEntryData(childEntry);
                }
                else
                {
                    var fileEntry = (U8ArchiveFile)dataEntry;

                    bool dataFilled = false;
                    if (fileEntry.InputStream != null)
                    {
                        fileEntry.Data = new byte[fileEntry.InputStream.Length];
                        fileEntry.InputStream.Read(fileEntry.Data, 0, fileEntry.Data.Length);
                        fileEntry.InputStream.Dispose();
                        fileEntry.InputStream = null;
                        dataFilled = true;
                    }
                    
                    if (CompressionLevel != CompressionLevel.NoCompression && !fileEntry.RequiresDecompression())
                    {
                        Logger.Log($"Compressing {fileEntry.Name}...", Interfaces.LogLevel.Utility, null);

                        // Compress the file's data.
                        fileEntry.Compress(CompressionLevel);
                        dataFilled = true;
                    }
                    else if (CompressionLevel == CompressionLevel.NoCompression && fileEntry.RequiresDecompression())
                    {
                        // Decompress the file's data.
                        fileEntry.Decompress();
                        dataFilled = true;
                    }

                    // Align the file to an offset divisible by 32.
                    writer.FixPadding(32);

                    // Fill in file data offset.
                    writer.FillOffset($"Data_{globalEntryIndex}");

                    if (CompressionLevel != CompressionLevel.NoCompression)
                    {
                        // Fill in compressed size.
                        writer.FillOffset($"CompressedSize_{globalEntryIndex}", (uint)(dataFilled ? fileEntry.Data.Length : fileEntry.GetCompressedData().Length));

                        // Fill in uncompressed size.
                        writer.FillOffset($"UncompressedSize_{globalEntryIndex}", fileEntry.UncompressedSize);
                    }
                    else
                    {
                        /* Fill in uncompressed size as zero, since the compressed
                           size is used as the actual length here. */
                        writer.FillOffset($"UncompressedSize_{globalEntryIndex}", 0);
                    }

                    // Write the file's data.
                    writer.Write(dataFilled ? fileEntry.Data : fileEntry.GetCompressedData());

                    // Increase global entry index.
                    ++globalEntryIndex;
                }
            }

            Dispose();
        }

        public void Extract(string location)
            => Root.Extract(location);

        /// <summary>
        /// Creates an archive structure from a local directory.
        /// </summary>
        /// <param name="path">Path to local directory to load.</param>
        /// <param name="includeSubdirectories">Include subdirectories in iteration.</param>
        public List<IArchiveData> GetFilesFromDirectory(string path, bool includeSubdirectories = false)
        {
            U8ArchiveDirectory dir = new()
            {
                Name = Path.GetFileName(path)
            };

            foreach (string filePath in Directory.EnumerateFiles(path))
            {
                U8ArchiveFile file = new(filePath)
                {
                    Parent = dir,
                };

                Logger.Log($"Loading {Path.GetFileName(filePath)}...", Interfaces.LogLevel.Utility, null);

                dir.Add(file);
            }

            if (includeSubdirectories)
            {
                foreach (string subdirectory in Directory.EnumerateDirectories(path))
                {
                    dir.Add
                    (
                        new U8ArchiveDirectory()
                        {
                            Name = Path.GetFileName(subdirectory),
                            Parent = dir,
                            Data = GetFilesFromDirectory(subdirectory, includeSubdirectories)
                        }
                    );
                }
            }

            return dir.Data;
        }
    }

    public enum U8DataType
    {
        File,
        Directory
    }

    public class U8ArchiveFile : IArchiveFile, IDisposable
    {
        public U8ArchiveFile()  { }

        public U8ArchiveFile(BinaryReaderEx reader)
        {
            Flags = reader.ReadUInt32();
            Offset = reader.ReadUInt32();
            Length = reader.ReadUInt32();
            UncompressedSize = reader.ReadUInt32();
            FileReader = reader;
        }

        public U8ArchiveFile(string file)
        {
            Name = System.IO.Path.GetFileName(file);
            InputStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            UncompressedSize = (uint)InputStream.Length;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public IArchiveData Parent { get; set; }

        public uint Offset { get; set; }

        public uint Length { get; set; }

        public uint UncompressedSize { get; set; }

        public byte[] Data { get; set; }

        public Stream InputStream { get; set; }

        public uint Flags { get; set; }

        public U8DataType Type => (U8DataType)((Flags & _typeMask) >> 24);

        public uint NameOffset => Flags & _nameOffsetMask;

        internal BinaryReaderEx FileReader { get; set; }

        internal byte[] CompressedData { get; set; }

        private const uint _typeMask = 0xFF000000;

        private const uint _nameOffsetMask = 0x00FFFFFF;

        public void Compress(CompressionLevel compressionLevel)
        {
            UncompressedSize = (uint)Data.Length;
            Data = ZlibStream.Compress(Data, compressionLevel);
            Length = (uint)Data.Length;
        }

        public byte[] GetCompressedData()
        {
            if (!RequiresDecompression())
                return null;

            if (CompressedData != null)
                return CompressedData;

            if (IsCompressed() && Data != null)
                return Data;

            // Jump to the file data.
            FileReader.JumpTo(Offset);

            // Read compressed data.
            CompressedData = FileReader.ReadBytes((int)Length);

            return CompressedData;
        }

        public void Decompress()
        {
            if (IsCompressed())
            {
                // Read the compressed data and decompress with ZlibStream.
                Data = ZlibStream.Decompress(GetCompressedData());
                CompressedData = null;
            }
            else
            {
                // Jump to file data offset before reading.
                FileReader.JumpTo(Offset);

                // File is uncompressed, so just read it.
                Data = FileReader.ReadBytes((int)Length);
            }

            Length = (uint)Data.Length;
            UncompressedSize = 0;
        }

        public bool IsCompressed()
        {
            /* Compressed files have both a compressed size and an uncompressed size.
               If a file is uncompressed, the compressed size will represent the length
               of the uncompressed data and the uncompressed size will be zero. */
            return Length != 0 && UncompressedSize != 0;
        }

        public bool RequiresDecompression() 
            => IsCompressed() || Data == null;

        public void Extract(string location)
        {
            if (RequiresDecompression())
                Decompress();

            Logger.Log($"Extracting {System.IO.Path.GetFileName(location)}...", Interfaces.LogLevel.Utility, null);

            File.WriteAllBytes(location, Data);
        }

        public override string ToString() => Name;

        public void Dispose()
            => InputStream?.Dispose();
    }

    public class U8ArchiveDirectory : IArchiveDirectory
    {
        public U8ArchiveDirectory() { }

        public U8ArchiveDirectory(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public List<IArchiveData> Data { get; internal set; } = new();

        public IArchiveData Parent { get; set; }

        public void Add(IArchiveData data, bool overwrite = true)
        {
            int existingIdx = -1;

            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Name == data.Name)
                {
                    existingIdx = i;
                    break;
                }
            }

            if (existingIdx >= 0)
            {
                if (!overwrite)
                    throw new Exception($"{data.Name} already exists");

                Data[existingIdx] = data;
            }
            else
            {
                Data.Add(data);
            }
        }

        public bool Remove(string name, bool isDir)
        {
            IArchiveData item = isDir ? ArchiveHelper.GetDirectory(this, name) : ArchiveHelper.GetFile(this, name);

            if (item == null)
                return false;

            if (item.Parent == this)
            {
                Data.Remove(item);

                return true;
            }

            if (item.Parent is IArchiveDirectory dir)
            {
                if (isDir)
                {
                    return dir.RemoveDirectory(item.Name);
                }
                else
                {
                    return dir.RemoveFile(item.Name);
                }
            }

            return false;
        }

        public bool RemoveFile(string name) 
            => Remove(name, false);

        public bool RemoveDirectory(string name) 
            => Remove(name, true);

        public bool FileExists(string name)
            => ArchiveHelper.GetFile(this, name) != null;

        public bool DirectoryExists(string name)
            => ArchiveHelper.GetDirectory(this, name) != null;

        /// <summary>
        /// Recursively creates new subdirectories based on a path.
        /// </summary>
        /// <param name="path">Path to create.</param>
        public U8ArchiveDirectory CreateDirectories(string path)
        {
            var names = path.Split(ArchiveHelper.DirectorySeperators, StringSplitOptions.RemoveEmptyEntries);

            // Return current directory if path split didn't get any results.
            if (names.Length == 0)
                return this;

            // Create directory base.
            U8ArchiveDirectory dir = null;

            foreach (string name in names)
            {
                // Set based on iteration.
                List<IArchiveData> data = dir == null ? Data : dir.Data;

                // Navigate to the directory if it exists already.
                if (data.Exists(t => t.Name == name))
                {
                    dir = data.Find(t => t.Name == name) as U8ArchiveDirectory;
                }

                // Create the first directory, since it doesn't exist.
                else if (dir == null)
                {
                    // Create new directory based on the current split.
                    U8ArchiveDirectory directory = new(name);

                    // Add this directory to the data.
                    Data.Add(directory);

                    // Set next iteration to this new directory.
                    dir = directory;
                }

                // Create the subdirectories.
                else
                {
                    // Create new subdirectory based on the current split.
                    U8ArchiveDirectory directory = new(name)
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

        public void Extract(string location)
        {
            // Create subdirectory to extract to.
            Directory.CreateDirectory(location);

            // Extract each archive node.
            foreach (IArchiveData data in Data)
                data.Extract(System.IO.Path.Combine(location, data.Name));
        }

        public override string ToString() => Name;

        public IEnumerator<IArchiveData> GetEnumerator()
        {
            return ((IEnumerable<IArchiveData>)Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
