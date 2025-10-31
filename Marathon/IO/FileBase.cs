using Amicitia.IO.Binary;
using Marathon.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Marathon.IO
{
    public class FileBase : IDisposable
    {
        private const string _intermediateExtension = ".json";

        /// <summary>
        /// The underlying stream to the file.
        /// </summary>
        [JsonIgnore]
        protected Stream Stream { get; private set; }

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
        public virtual WriteMode WriteMode { get; set; } = WriteMode.New;

        /// <summary>
        /// Leaves the <see cref="Stream"/> open after writing.
        /// <para>If left open, the stream must manually be disposed using the <see cref="Dispose"/> method.</para>
        /// </summary>
        [JsonIgnore]
        public virtual bool LeaveOpen { get; set; } = false;

        public FileBase(WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false)
        {
            WriteMode = in_writeMode;
            LeaveOpen = in_leaveOpen;
        }

        public FileBase(string in_path, WriteMode in_writeMode = WriteMode.New, bool in_leaveOpen = false)
            : this(in_writeMode, in_leaveOpen)
        {
            Read(in_path);
        }

        public virtual void Read(string in_path)
        {
            Location = in_path;

            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            ThrowHelper.ThrowFileNotFoundException(in_path);

            Stream = new FileStream(in_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            Read(Stream);
        }

        public virtual void Read(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(string in_path, bool in_isOverwrite = true)
        {
            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            ThrowHelper.ThrowFileExistsException(in_path);

            switch (WriteMode)
            {
                case WriteMode.New:
                {
                    Location = in_path;

                    using (var stream = new FileStream(in_path, FileMode.Create, FileAccess.ReadWrite))
                        Write(stream);

                    break;
                }

                case WriteMode.Fixed:
                {
                    if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
                    {
                        if (in_path == Location)
                            return;

                        // Copy the fixed file to the new writing location.
                        File.Copy(Location, in_path, true);
                    }

                    using (var stream = new FileStream(in_path, FileMode.Open, FileAccess.ReadWrite))
                        Write(stream);

                    break;
                }
            }
        }

        public virtual void Write(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(bool in_isOverwrite = true)
        {
            Write(Location, in_isOverwrite);
        }

        public virtual void Import(string in_path)
        {
            if (HasExtension && string.IsNullOrEmpty(Extension))
                throw new Exception("A file extension for this format has not been provided.");

            ThrowHelper.ThrowFileNotFoundException(in_path);

            if (!in_path.EndsWith(Extension + _intermediateExtension))
                throw new IOException("The specified file is not in the default intermediate format.");

            JsonConvert.PopulateObject(File.ReadAllText(in_path), this);
        }

        public virtual void Export(string in_path = "", bool in_isOverwrite = true)
        {
            if (HasExtension && string.IsNullOrEmpty(Extension))
                throw new Exception("A file extension for this format has not been provided.");

            if (string.IsNullOrEmpty(in_path))
            {
                if (string.IsNullOrEmpty(Location))
                    throw new ArgumentNullException(nameof(in_path));

                in_path = $"{Location}.json";
            }

            in_path = FilesystemHelper.EnsureExtension(in_path, Extension + _intermediateExtension);

            if (!in_isOverwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            File.WriteAllText(in_path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }

    public enum WriteMode
    {
        /// <summary>
        /// Writes to the file directly.
        /// </summary>
        Fixed,

        /// <summary>
        /// Writes the entire file from scratch.
        /// </summary>
        New
    }
}
