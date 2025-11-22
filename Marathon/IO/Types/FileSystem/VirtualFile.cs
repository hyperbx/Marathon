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

        public long Length { get; set; }

        public long UncompressedLength { get; set; }

        public Func<IFile, CompressionLevel, bool> Compress { get; set; }

        public Func<IFile, bool> Decompress { get; set; }

        public Stream BaseStream { get; set; }

        public VirtualFile() { }

        public VirtualFile(string in_name)
        {
            Name = in_name;
        }

        public Stream Open(FileAccess in_access = FileAccess.Read)
        {
            return BaseStream;
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
