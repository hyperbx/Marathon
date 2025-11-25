using Marathon.Extensions;
using Marathon.Helpers;
using Marathon.IO.Compression;
using System;
using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Types.FileSystem
{
    public class PhysicalFile : IFile
    {
        public string Name
        {
            get
            {
                var name = System.IO.Path.GetFileName(Path);

                if (string.IsNullOrEmpty(name))
                    return Path;

                return name;
            }

            set => throw new NotSupportedException();
        }

        public string Path { get; set; }

        public IDirectory Parent
        {
            get
            {
                var parent = PhysicalDirectory.GetParentFromPath(Path);

                if (parent == null)
                    ThrowHelper.ThrowDirectoryNotFoundException(System.IO.Path.GetDirectoryName(Path), false);

                return parent;
            }

            set => throw new NotSupportedException();
        }

        public bool IsDirectory => false;

        public long Length
        {
            get => GetFileInfoProperty(Info, i => i.Length, 0);
            set => throw new NotSupportedException();
        }

        public long UncompressedLength { get; set; }

        public ICompressionService CompressionService { get; set; }

        public Stream BaseStream { get; set; }

        public FileAccess Access { get; set; }

        public FileInfo Info => string.IsNullOrEmpty(Path) ? null : new FileInfo(Path);

        public PhysicalFile() { }

        public PhysicalFile(string in_path)
        {
            Path = System.IO.Path.GetFullPath(in_path);

            ThrowHelper.ThrowFileNotFoundException(Path);
        }

        public IFile Clone()
        {
            return new PhysicalFile
            {
                Path = Path,
                UncompressedLength = UncompressedLength,
                CompressionService = CompressionService,
                BaseStream = BaseStream,
                Access = Access
            };
        }

        public bool Delete()
        {
            return Delete(Path);
        }

        public static bool Delete(string in_path)
        {
            if (!File.Exists(in_path))
                return false;

            try
            {
                File.Delete(in_path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Stream Open(FileAccess in_access = FileAccess.Read)
        {
            if (IsDisposed())
                BaseStream = null;

            if (BaseStream != null)
            {
                if (Access == in_access)
                {
                    BaseStream.Position = 0;

                    return BaseStream;
                }
                else
                {
                    Dispose();
                }
            }

            Access = in_access;
            BaseStream = new FileStream(Path, FileSystemHelper.TransformFileAccessToFileMode(Access), Access, FileShare.ReadWrite);

            return BaseStream;
        }

        public void Export(string in_path, bool in_overwrite = true)
        {
            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            this.Export<PhysicalFile>(in_path);
        }

        public void ReplaceWith(IFile in_file)
        {
            Dispose();

            in_file.Export(Path);

            UncompressedLength = in_file.UncompressedLength;
            CompressionService = in_file.CompressionService;
        }

        public IFile Compress(CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            return this.Compress<PhysicalFile>(in_compressionLevel);
        }

        public IFile Decompress()
        {
            return this.Decompress<PhysicalFile>();
        }

        private T GetFileInfoProperty<T>(FileInfo in_fileInfo, Func<FileInfo, T> in_getter, T in_defaultValue)
        {
            if (string.IsNullOrEmpty(Path) || in_fileInfo == null)
                return in_defaultValue;

            return in_getter(in_fileInfo);
        }

        public bool IsDisposed()
        {
            if (BaseStream == null)
                return false;

            try
            {
                _ = BaseStream.Length;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void Dispose()
        {
            BaseStream?.Dispose();
            BaseStream = null;
        }

        public override bool Equals(object in_obj)
        {
            return in_obj is PhysicalFile out_physicalFile && Path == out_physicalFile.Path;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
