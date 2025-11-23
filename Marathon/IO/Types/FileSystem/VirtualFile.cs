using Marathon.Extensions;
using Marathon.Helpers;
using System;
using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Types.FileSystem
{
    public class VirtualFile : IFile
    {
        public string Name { get; set; }

        public string Path
        {
            get => FileSystemHelper.CreatePath(this);
            set => throw new NotSupportedException();
        }

        public IDirectory Parent { get; set; }

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

        public VirtualFile() { }

        public VirtualFile(string in_name)
        {
            Name = in_name;
        }

        public Stream Open(FileAccess in_access = FileAccess.Read)
        {
            if (BaseStream != null && !IsDisposed())
                BaseStream.Position = 0;

            return BaseStream;
        }

        public void ReplaceWith(IFile in_file)
        {
            Length = in_file.Length;
            UncompressedLength = in_file.UncompressedLength;
            CompressionMethod = in_file.CompressionMethod;
            DecompressionMethod = in_file.DecompressionMethod;
            BaseStream = in_file.BaseStream;
        }

        public IFile Compress(CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            return this.Compress<VirtualFile>(in_compressionLevel);
        }

        public IFile Decompress()
        {
            return this.Decompress<VirtualFile>();
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

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return base.ToString();

            return Name;
        }
    }
}
