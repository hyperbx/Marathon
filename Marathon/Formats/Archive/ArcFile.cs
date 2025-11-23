using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Compression;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

// Format names:        Arc File, ARC
// Format references:   Sonicteam::SoX::ArcFile, Sonicteam::SoX::FileSystemARC
// Format designers:    Nintendo, Sonic Team
// Format researchers:  Nintendo homebrew community, xose
//
// Format research references:
// - https://www.wiibrew.org/wiki/U8_archive
// - https://wiki.tockdom.com/wiki/U8_(File_Format)

namespace Marathon.Formats.Archive
{
    /// <summary>
    /// Support for *.arc files; used for the game's packed filesystem.
    /// </summary>
    public class ArcFile : FileBase, IDirectory
    {
        private const string _extension = ".arc";   // "ARChive"
        private const uint _signature = 0x55AA382D; // "U.8-"
        private const uint _soxMagic = 0xE4F91200;
        private VirtualDirectory _root = new();

        public string Name { get; set; } = string.Empty;

        public string Path
        {
            get => _root.Path;
            set => throw new NotSupportedException();
        }

        public IDirectory Parent { get; set; } = null;

        public bool IsDirectory => true;

        public bool IsSoXArchive { get; set; } = true;

        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Optimal;

        public override string Extension => _extension;

        public INode this[string in_name] => _root[in_name];

        public INode this[int in_index] => _root[in_index];

        public ArcFile() { }

        public ArcFile(CompressionLevel in_compressionLevel)
        {
            CompressionLevel = in_compressionLevel;
        }

        public ArcFile(string in_path, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            CompressionLevel = in_compressionLevel;

            if (System.IO.Path.GetExtension(in_path) == _extension)
            {
                Read(in_path);
            }
            else if (Directory.Exists(in_path))
            {
                Import(in_path);
            }
            else
            {
                throw new InvalidPathException(in_path);
            }
        }

        public ArcFile(Stream in_stream, CompressionLevel in_compressionLevel) : base(in_stream)
        {
            CompressionLevel = in_compressionLevel;
        }

        public ArcFile(IFile in_file, CompressionLevel in_compressionLevel) : base(in_file)
        {
            CompressionLevel = in_compressionLevel;
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness, Encoding.ASCII);

            reader.Endianness = Endianness = reader.GetEndiannessFromSignature(_signature);

            var fsTableOffset = reader.Read<uint>();
            var fsTableLength = reader.Read<uint>();
            var dataOffset = reader.Read<uint>();

            var reserved1 = reader.ReadBig<uint>();
            var reserved2 = reader.ReadBig<uint>();
            var reserved3 = reader.ReadBig<uint>();
            var reserved4 = reader.ReadBig<uint>();

            // Sonic '06 magic numbers.
            IsSoXArchive = reserved1 == _soxMagic || reserved1 == 0xE4F91300 || reserved4 == 0x78013800;

            // Checks the first entry's type and name offset after root.
            // This should always be zero for uncompressed size in Sonic '06 archives.
            // That alone would've been a better way to detect them, but their tool
            // shoved in some uninitialised memory, so this isn't 100% reliable.
            if (!IsSoXArchive)
                reader.ReadAtOffset(fsTableOffset + 0x0C, () => IsSoXArchive = reader.Read<uint>() == 0);

            reader.JumpTo(fsTableOffset);

            var root = reader.ReadObject<ArcFilesystemEntry, ArcFile>(this);

            if (!root.IsDirectory)
                throw new InvalidDataException("The root node is not a directory.");

            var fsEntrySize = IsSoXArchive ? 16 : 12;
            var stringPoolOffset = fsTableOffset + (root.NodeCount * fsEntrySize);
            var entries = new ArcFilesystemEntry[root.NodeCount];

            entries[0] = root;

            for (int i = 1; i < root.NodeCount; ++i)
                entries[i] = reader.ReadObject<ArcFilesystemEntry, ArcFile>(this);

            WalkEntries(0, new(), true);

            uint WalkEntries(uint in_entryIndex, VirtualDirectory in_directory, bool in_isRoot = false)
            {
                var entry = entries[(int)in_entryIndex];
                var entryName = string.Empty;

                reader.ReadAtOffset(stringPoolOffset + entry.NameOffset,
                    () => entryName = reader.ReadStringNullTerminated());

                if (!in_isRoot)
                {
                    // Skip this entry.
                    if (string.IsNullOrEmpty(entryName))
                        return entry.Length;
                }

                if (entry.IsDirectory)
                {
                    var directory = new VirtualDirectory
                    {
                        Name = entryName
                    };

                    if (in_isRoot)
                    {
                        _root = directory;
                    }
                    else
                    {
                        directory.Parent = in_directory;

                        in_directory.AddDirectory(directory);
                    }

                    var childIndex = ++in_entryIndex;

                    // Parse current directory's children.
                    while (childIndex < entry.NodeCount)
                        childIndex = WalkEntries(childIndex, directory);

                    // Return the index of the next directory.
                    return entry.NodeCount;
                }
                else
                {
                    var file = new VirtualFile
                    {
                        Name = entryName,
                        Parent = in_directory,
                        UncompressedLength = entry.UncompressedLength,
                        CompressionMethod = CompressionMethod,
                        DecompressionMethod = DecompressionMethod,
                        BaseStream = new SubStream(BaseStream, entry.DataOffset, entry.Length)
                    };

                    in_directory.AddFile(file);

                    // Return the index of the next entry.
                    return ++in_entryIndex;
                }
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness, Encoding.ASCII);

            writer.Write(_signature);
            writer.Reserve<uint>("EntriesOffset");
            writer.Reserve<uint>("EntriesLength");
            writer.Reserve<uint>("DataOffset");
            writer.Write(IsSoXArchive ? _soxMagic : 0);
            writer.WriteNullBytes(12);

            writer.WriteReserved("EntriesOffset", (uint)writer.Position);

            var globalEntryIndex = 0;
            var stringPoolLength = 0;
            var hasData = false;

            WriteEntryTable(_root);
            WriteEntryNames(_root);
            
            writer.WriteReserved("EntriesLength", (uint)writer.Position - 0x20);
            
            if (hasData)
                writer.Align(32);
            
            writer.WriteReserved("DataOffset", (uint)writer.Position);
            
            if (hasData)
            {
                globalEntryIndex = 0;
                WriteEntryData(_root);
            }

            void WriteEntryTable(INode in_node, int in_parentIndex = 0)
            {
                if (writer.Endianness == Endianness.Big)
                {
                    writer.Write(in_node.IsDirectory);
                    writer.WriteInt24(stringPoolLength);
                }
                else
                {
                    writer.WriteInt24(stringPoolLength);
                    writer.Write(in_node.IsDirectory);
                }

                stringPoolLength += string.IsNullOrEmpty(in_node.Name)
                    ? 1
                    : writer.Encoding.GetByteCount(in_node.Name) + 1;

                if (in_node.IsDirectory)
                {
                    var dir = in_node as IDirectory;

                    if (dir.Parent == null)
                    {
                        // Root has no parent.
                        writer.Write(0);

                        ++globalEntryIndex;

                        writer.Write(dir.GetNodeCount(SearchOption.AllDirectories) + 1);
                    }
                    else
                    {
                        writer.Write(in_parentIndex);

                        in_parentIndex = globalEntryIndex;

                        ++globalEntryIndex;

                        writer.Write(globalEntryIndex + dir.GetNodeCount(SearchOption.AllDirectories));
                    }

                    if (IsSoXArchive)
                        writer.Write(0);

                    foreach (var node in dir)
                        WriteEntryTable(node, in_parentIndex);
                }
                else
                {
                    var file = in_node as IFile;

                    writer.Reserve<uint>($"File{globalEntryIndex}Data");
                    writer.Reserve<uint>($"File{globalEntryIndex}Length");

                    if (IsSoXArchive)
                        writer.Reserve<uint>($"File{globalEntryIndex}UncompressedLength");

                    ++globalEntryIndex;

                    hasData = true;
                }
            }

            void WriteEntryNames(INode in_node)
            {
                if (string.IsNullOrEmpty(in_node.Name))
                {
                    writer.WriteByte(0);
                }
                else
                {
                    writer.WriteStringNullTerminated(in_node.Name);
                }

                if (!in_node.IsDirectory)
                    return;

                var dir = in_node as IDirectory;

                foreach (var node in dir)
                    WriteEntryNames(node);
            }

            void WriteEntryData(INode in_node)
            {
                if (in_node.IsDirectory)
                {
                    var dir = in_node as IDirectory;

                    ++globalEntryIndex;

                    foreach (var node in dir)
                        WriteEntryData(node);
                }
                else
                {
                    var file = in_node as IFile;
                    var fileLength = (uint)file.Length;
                    var fileUncompressedLength = (uint)file.UncompressedLength;

                    writer.Align(32);
                    writer.WriteReserved($"File{globalEntryIndex}Data", (uint)writer.Position);

                    if (fileUncompressedLength <= 0 && CompressionLevel != CompressionLevel.NoCompression)
                    {
                        if (!ZLib.TryCompress(file.Open(), writer.GetBaseStream(), CompressionLevel, out var out_compressedStream))
                            throw new IOException($"Failed to compress file: {file.Path}");

                        fileUncompressedLength = fileLength;
                        fileLength = (uint)out_compressedStream.Length;
                    }
                    else
                    {
                        file.Open().CopyTo(writer.GetBaseStream());
                    }

                    file.BaseStream.Position = 0;

                    writer.WriteReserved($"File{globalEntryIndex}Length", fileLength);
                    writer.WriteReserved($"File{globalEntryIndex}UncompressedLength", fileUncompressedLength);

                    ++globalEntryIndex;
                }
            }
        }

        public override void Import(string in_path)
        {
            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            if (File.Exists(in_path))
                throw new IOException($"{nameof(in_path)} is not a path to a directory.");

            var dir = new PhysicalDirectory(in_path);

            foreach (var node in dir.EnumerateNodes())
            {
                if (node.IsDirectory)
                {
                    AddDirectory(node as IDirectory);
                }
                else
                {
                    AddFile(node as IFile);
                }
            }    
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, FileSystemHelper.GetDirectoryNameOfFileName);

            Directory.CreateDirectory(in_path);

            foreach (var file in EnumerateFiles(in_searchOption: SearchOption.AllDirectories))
            {
                var filePath = System.IO.Path.Combine(in_path, file.Path);

                if (!in_overwrite)
                    ThrowHelper.ThrowFileExistsException(filePath);

                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    if (file.UncompressedLength > 0)
                    {
                        if (!ZLib.TryDecompress(file.Open(), fs, out var out_uncompressedStream) || out_uncompressedStream.Length != file.UncompressedLength)
                            throw new IOException($"Failed to decompress file: {file.Path}");
                    }
                    else
                    {
                        file.Open().CopyTo(fs);
                    }

                    file.BaseStream.Position = 0;
                }
            }
        }

        private static bool CompressionMethod(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel)
        {
            return ZLib.TryCompress(in_srcStream, in_destStream, in_compressionLevel, out _);
        }

        private static bool DecompressionMethod(Stream in_srcStream, Stream in_destStream)
        {
            return ZLib.TryDecompress(in_srcStream, in_destStream, out _);
        }

        public int GetNodeCount(SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.GetNodeCount(in_searchOption);
        }

        /// <summary>
        /// Returns the nodes that match the specified search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of nodes in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of nodes in the specified archive that match the specified criteria, or an empty array if no nodes are found.
        /// </returns>
        public static INode[] GetNodes(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).GetNodes(in_searchPattern, in_searchOption);
        }

        public INode[] GetNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.GetNodes(in_searchPattern, in_searchOption);
        }

        /// <summary>
        /// Returns an enumerable collection of nodes that match a search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of nodes in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of nodes in the specified archive that match the specified search pattern and search option.
        /// </returns>
        public static IEnumerable<INode> EnumerateNodes(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).EnumerateNodes(in_searchPattern, in_searchOption);
        }

        public IEnumerable<INode> EnumerateNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.EnumerateNodes(in_searchPattern, in_searchOption);
        }

        public INode AddNode(INode in_node, bool in_overwrite = true)
        {
            return _root.AddNode(in_node, in_overwrite);
        }

        public IDirectory GetDirectory()
        {
            return _root;
        }

        /// <summary>
        /// Returns the directories that match the specified search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of directories in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of directories in the specified archive that match the specified criteria, or an empty array if no directories are found.
        /// </returns>
        public static IDirectory[] GetDirectories(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).GetDirectories(in_searchPattern, in_searchOption);
        }

        public IDirectory[] GetDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.GetDirectories(in_searchPattern, in_searchOption);
        }

        /// <summary>
        /// Returns an enumerable collection of directories that match a search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of directories in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of directories in the specified archive that match the specified search pattern and search option.
        /// </returns>
        public static IEnumerable<IDirectory> EnumerateDirectories(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).EnumerateDirectories(in_searchPattern, in_searchOption);
        }

        public IEnumerable<IDirectory> EnumerateDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.EnumerateDirectories(in_searchPattern, in_searchOption);
        }

        public IDirectory GetDirectory(string in_path)
        {
            return _root.GetDirectory(in_path);
        }

        public IDirectory CreateDirectory(string in_path)
        {
            return _root.CreateDirectory(in_path);
        }

        public IDirectory AddDirectory(IDirectory in_directory, bool in_merge = true)
        {
            return _root.AddDirectory(in_directory, in_merge);
        }

        public bool DeleteDirectory(string in_path)
        {
            return _root.DeleteDirectory(in_path);
        }

        /// <summary>
        /// Returns the files that match the specified search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of files in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of files in the specified archive that match the specified criteria, or an empty array if no files are found.
        /// </returns>
        public static IFile[] GetFiles(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).GetFiles(in_searchPattern, in_searchOption);
        }

        public IFile[] GetFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.GetFiles(in_searchPattern, in_searchOption);
        }

        /// <summary>
        /// Returns an enumerable collection of files that match a search pattern in the specified archive, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of files in the specified archive.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of files in the specified archive that match the specified search pattern and search option.
        /// </returns>
        public static IEnumerable<IFile> EnumerateFiles(string in_path, string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return new ArcFile(in_path).EnumerateFiles(in_searchPattern, in_searchOption);
        }

        public IEnumerable<IFile> EnumerateFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return _root.EnumerateFiles(in_searchPattern, in_searchOption);
        }

        public IFile GetFile(string in_path)
        {
            return _root.GetFile(in_path);
        }

        public IFile CreateFile(string in_path, bool in_overwrite = true)
        {
            var file = _root.CreateFile(in_path, in_overwrite);

            file.CompressionMethod = CompressionMethod;
            file.DecompressionMethod = DecompressionMethod;

            return file;
        }

        public IFile AddFile(IFile in_path, bool in_overwrite = true)
        {
            var file = _root.AddFile(in_path, in_overwrite);

            file.CompressionMethod = CompressionMethod;
            file.DecompressionMethod = DecompressionMethod;

            return file;
        }

        public bool DeleteFile(string in_path)
        {
            return _root.DeleteFile(in_path);
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return _root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ArcFilesystemEntry : IBinarySerializable<ArcFile>
    {
        /// <summary>
        /// Determines whether this entry is a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// The offset of this entry's name in the string pool.
        /// </summary>
        public uint NameOffset { get; set; }

        /// <summary>
        /// The offset of this file's data.
        /// </summary>
        public uint DataOffset { get; set; }

        /// <summary>
        /// The index of the parent node for this directory.
        /// </summary>
        public uint ParentIndex => DataOffset;

        /// <summary>
        /// The length of this file's data.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// The total number of nodes in this directory.
        /// </summary>
        public uint NodeCount => Length;

        /// <summary>
        /// The uncompressed length of this file's data (unique field for SoX format).
        /// </summary>
        public uint UncompressedLength { get; set; }

        public void Read(BinaryObjectReader in_reader, ArcFile in_context)
        {
            if (in_reader.Endianness == Endianness.Big)
            {
                IsDirectory = in_reader.Read<bool>();
                NameOffset = in_reader.ReadUInt24();
            }
            else
            {
                NameOffset = in_reader.ReadUInt24();
                IsDirectory = in_reader.Read<bool>();
            }

            DataOffset = in_reader.Read<uint>();
            Length = in_reader.Read<uint>();

            if (in_context.IsSoXArchive)
                UncompressedLength = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriter in_writer, ArcFile in_context)
        {
            throw new NotImplementedException();
        }
    }
}
