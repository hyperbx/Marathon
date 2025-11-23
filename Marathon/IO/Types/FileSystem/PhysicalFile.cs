using Marathon.Extensions;
using Marathon.Helpers;
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
            get => BaseStream == null ? 0 : BaseStream.Length;
            set => throw new NotSupportedException();
        }

        public long UncompressedLength { get; set; }

        public CompressionDelegate CompressionMethod { get; set; }

        public DecompressionDelegate DecompressionMethod { get; set; }

        public Stream BaseStream { get; set; }

        public FileAccess Access { get; set; }

        public PhysicalFile() { }

        public PhysicalFile(string in_path)
        {
            Path = System.IO.Path.GetFullPath(in_path);

            ThrowHelper.ThrowFileNotFoundException(Path);

            Length = new FileInfo(Path).Length;
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

        public void ReplaceWith(IFile in_file)
        {
            Dispose();

            UncompressedLength = in_file.UncompressedLength;
            CompressionMethod = in_file.CompressionMethod;
            DecompressionMethod = in_file.DecompressionMethod;

            using (var fs = new FileStream(Path, FileMode.Create))
                in_file.Open().CopyTo(fs);
        }

        public IFile Compress(CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            return this.Compress<PhysicalFile>(in_compressionLevel);
        }

        public IFile Decompress()
        {
            return this.Decompress<PhysicalFile>();
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
            return in_obj is PhysicalFile out_file && Path == out_file.Path;
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
