using Amicitia.IO.Binary;
using Marathon.Helpers;
using Marathon.IO.Types.FileSystem;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Marathon.IO
{
    public class FileBase(WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false) : IDisposable
    {
        private const string _defaultIntermediateExtension = ".json";

        /// <summary>
        /// The underlying stream to the file.
        /// </summary>
        [JsonIgnore]
        public Stream BaseStream { get; private set; }

        /// <summary>
        /// The location of this file.
        /// </summary>
        [JsonIgnore]
        public string Location { get; private set; }

        /// <summary>
        /// Determines whether this file format has a file extension.
        /// </summary>
        [JsonIgnore]
        public virtual bool HasExtension { get; private set; } = true;

        /// <summary>
        /// The file extension of this file format.
        /// </summary>
        [JsonIgnore]
        public virtual string Extension { get; private set; }

        /// <summary>
        /// The endianness of this file format.
        /// </summary>
        [JsonIgnore]
        public virtual Endianness Endianness { get; set; } = Endianness.Big;

        /// <summary>
        /// The method used for writing the file.
        /// </summary>
        [JsonIgnore]
        public virtual WriteMode WriteMode { get; set; } = in_writeMode;

        /// <summary>
        /// Determines whether to write to a temporary file first before replacing the original.
        /// <para>This should be used for writing to files that have streams associated with them.</para>
        /// </summary>
        [JsonIgnore]
        public virtual bool UseTempFile { get; set; }

        /// <summary>
        /// Leaves the <see cref="BaseStream"/> open after disposing.
        /// </summary>
        [JsonIgnore]
        public virtual bool LeaveOpen { get; set; } = in_leaveOpen;

        public FileBase(string in_path, WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false) : this(in_writeMode, in_leaveOpen)
        {
            var extension = '.' + string.Join('.', FileSystemHelper.GetExtensions(in_path, 2));

            if (extension.Equals(Extension + _defaultIntermediateExtension, StringComparison.OrdinalIgnoreCase))
            {
                Import(in_path);
            }
            else
            {
                Read(in_path);
            }
        }

        public FileBase(Stream in_stream, WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false) : this(in_writeMode, in_leaveOpen)
        {
            Read(in_stream);
        }

        public FileBase(IFile in_file, WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false) : this(in_writeMode, in_leaveOpen)
        {
            Read(in_file.Open());
        }

        public virtual void Read(string in_path)
        {
            Location = in_path;

            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            ThrowHelper.ThrowFileNotFoundException(in_path);

            BaseStream = new FileStream(in_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            Read(BaseStream);
        }

        public virtual void Read(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public virtual void Read(IFile in_file)
        {
            Read(in_file.Open());
        }

        public virtual void Write(string in_path, bool in_overwrite = true)
        {
            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            var origPath = in_path;
            var tempPath = origPath;
            var isTempFile = UseTempFile && origPath == Location && File.Exists(Location);

            // Create a file in the temporary data location.
            // This will be used for writing before being moved
            // back to the original file to replace it.
            if (isTempFile)
                tempPath = FileSystemHelper.ChangeFileName(origPath, FileSystemHelper.GetTempFileName(origPath), false);

            switch (WriteMode)
            {
                case WriteMode.New:
                {
                    Location = tempPath;

                    using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        Write(stream);

                    break;
                }

                case WriteMode.Fixed:
                {
                    origPath = Location;

                    if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
                    {
                        // Copy the fixed file to the new writing location.
                        if (tempPath != Location)
                            File.Copy(Location, tempPath, true);
                    }
                    else
                    {
                        ThrowHelper.ThrowFileNotFoundException(Location, false);
                    }

                    using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        Write(stream);

                    break;
                }
            }

            // Replace the original file with the final
            // written file from the temporary data location.
            if (isTempFile)
            {
                var isLeaveOpen = LeaveOpen;

                // Close the stream so we can replace this file.
                LeaveOpen = false;
                Dispose();

                FileSystemHelper.ReplaceFile(tempPath, origPath);

                // Reconfigure and reopen stream from the new file.
                LeaveOpen = isLeaveOpen;
                BaseStream = new FileStream(origPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            // Restore original path.
            Location = origPath;
        }

        public virtual void Write(bool in_overwrite = true)
        {
            Write(Location, in_overwrite);
        }

        public virtual void Write(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IFile in_file)
        {
            in_file.BaseStream = new MemoryStream();

            Write(in_file.Open());

            in_file.UncompressedLength = 0;
        }

        public Stream Write()
        {
            Dispose();

            BaseStream = new MemoryStream();

            Write(BaseStream);

            return BaseStream;
        }

        public virtual void Import(string in_path)
        {
            if (HasExtension && string.IsNullOrEmpty(Extension))
                throw new Exception("A file extension for this format has not been provided.");

            ThrowHelper.ThrowFileNotFoundException(in_path);

            if (!in_path.EndsWith(Extension + _defaultIntermediateExtension, StringComparison.OrdinalIgnoreCase))
                throw new IOException("The specified file is not in the default intermediate format.");

            JsonConvert.PopulateObject(File.ReadAllText(in_path), this);
        }

        public virtual void Export(string in_path = "", bool in_overwrite = true)
        {
            if (HasExtension && string.IsNullOrEmpty(Extension))
                throw new Exception("A file extension for this format has not been provided.");

            if (string.IsNullOrEmpty(in_path))
            {
                if (string.IsNullOrEmpty(Location))
                    throw new ArgumentNullException(nameof(in_path));

                in_path = $"{Location}.json";
            }

            in_path = FileSystemHelper.EnsureExtension(in_path, Extension + _defaultIntermediateExtension);

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            File.WriteAllText(in_path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void EnsurePath(ref string in_path, Func<string, string> in_modifier = null)
        {
            if (string.IsNullOrEmpty(in_path))
            {
                if (string.IsNullOrEmpty(Location))
                    throw new ArgumentNullException(nameof(in_path));

                in_path = Location;
            }

            if (in_modifier == null)
                return;

            in_path = in_modifier(in_path);
        }

        public void Dispose()
        {
            if (LeaveOpen)
                return;

            BaseStream?.Dispose();
            BaseStream = null;
        }
    }

    public enum WriteMode
    {
        /// <summary>
        /// Writes the file from scratch.
        /// </summary>
        New,

        /// <summary>
        /// Writes to the file directly.
        /// </summary>
        Fixed
    }
}
