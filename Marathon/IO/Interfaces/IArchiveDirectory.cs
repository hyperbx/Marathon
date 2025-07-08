using System.Collections.Generic;

namespace Marathon.IO.Interfaces
{
    public interface IArchiveDirectory : IArchiveData, IEnumerable<IArchiveData>
    {
        public void Add(IArchiveData in_data, bool in_overwrite = false);
        public bool RemoveFile(string in_name);
        public bool RemoveDirectory(string in_name);
        public bool FileExists(string in_name);
        public bool DirectoryExists(string in_name);
    }
}
