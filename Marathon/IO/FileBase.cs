using Amicitia.IO.Binary;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Marathon.IO
{
    public class FileBase : IDisposable
    {
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
        /// The endianness of this file.
        /// </summary>
        [JsonIgnore]
        public Endianness Endianness { get; set; } = Endianness.Big;

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

            if (!File.Exists(in_path))
                throw new FileNotFoundException("The specified file does not exist.", in_path);

            Stream = new FileStream(in_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            Read(Stream);
        }

        public virtual void Read(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(string in_path, bool in_overwrite = true)
        {
            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            if (!in_overwrite && File.Exists(in_path))
                throw new IOException("The specified file already exists.");

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

        public virtual void Import(string in_path) { }

        public virtual void Export(string in_path = "") { }

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
