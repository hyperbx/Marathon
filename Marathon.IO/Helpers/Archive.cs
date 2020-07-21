using System;
using System.IO;
using System.Collections.Generic;

namespace Marathon.IO
{
    public class Archive : FileBase
    {
        public class ArchiveData
        {
            public string Name;

            public virtual void Extract(string filePath)
                => throw new NotImplementedException();
        }

        public class ArchiveFile : ArchiveData
        {
            public byte[] Data;

            public ArchiveFile() { }

            public ArchiveFile(string filePath)
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The given file could not be found.", filePath);

                FileInfo fileInfo = new FileInfo(filePath);

                Name = fileInfo.Name;
                Data = File.ReadAllBytes(filePath);
            }

            public ArchiveFile(string name, byte[] data)
            {
                Name = name;
                Data = data;
            }

            public override void Extract(string filePath)
                => File.WriteAllBytes(filePath, Data);
        }

        public class ArchiveDirectory : ArchiveData
        {
            public List<ArchiveData> Data = new List<ArchiveData>();
            public ArchiveDirectory Parent = null;

            public ArchiveDirectory() { }

            public ArchiveDirectory(string directoryName) => Name = directoryName;

            public override void Extract(string directory)
            {
                Directory.CreateDirectory(directory);

                foreach (ArchiveData file in Data)
                    file.Extract(Path.Combine(directory, file.Name));
            }
        }

        public List<ArchiveData> Data = new List<ArchiveData>();

        public Archive() { }

        public Archive(Archive arc) => Data = arc.Data;

        public static List<ArchiveFile> GetFiles(List<ArchiveData> files, bool includeSubDirectories = true)
        {
            List<ArchiveFile> list = new List<ArchiveFile>();

            foreach (ArchiveData data in files)
            {
                if (includeSubDirectories && data is ArchiveDirectory dir)
                {
                    list.AddRange(GetFiles(dir.Data));
                }
                else if (data is ArchiveFile file)
                {
                    list.Add(file);
                }
            }

            return list;
        }

        public List<ArchiveFile> GetFiles(bool includeSubDirectories = true)
            => GetFiles(Data, includeSubDirectories);

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

        public void Extract(string directory)
        {
            Directory.CreateDirectory(directory);

            foreach (var entry in Data)
                entry.Extract(Path.Combine(directory, entry.Name));
        }

        public void AddDirectory(string dir, bool includeSubDirectories = false)
        {
            Data.AddRange(GetFilesFromDirectory(dir, includeSubDirectories));
        }

        public ArchiveDirectory CreateDirectories(string dirPath)
        {
            ArchiveDirectory dir = null;

            foreach (string dirName in dirPath.Split('/'))
            {
                List<ArchiveData> data = dir == null ? Data : dir.Data;

                if (data.Exists(t => t.Name == dirName))
                    dir = data.Find(t => t.Name == dirName) as ArchiveDirectory;

                else if (dir == null)
                {
                    var directory = new ArchiveDirectory(dirName);
                    Data.Add(directory);
                    dir = directory;
                }
                else
                {
                    var newDirectory = new ArchiveDirectory(dirName);
                    dir.Data.Add(newDirectory);
                    newDirectory.Parent = dir;
                    dir = newDirectory;
                }
            }

            return dir;
        }

        public static List<ArchiveData> GetFilesFromDirectory(string dir, bool includeSubDirectories = false)
        {
            List<ArchiveData> data = new List<ArchiveData>();

            // Add each file in the current sub-directory.
            foreach (string filePath in Directory.GetFiles(dir))
                data.Add(new ArchiveFile(filePath));

            // Repeat for each sub directory.
            if (includeSubDirectories)
                foreach (string subDir in Directory.GetDirectories(dir))
                    data.Add(new ArchiveDirectory() { Data = GetFilesFromDirectory(subDir, includeSubDirectories) });

            return data;
        }
    }
}