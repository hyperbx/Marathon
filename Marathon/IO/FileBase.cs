global using Marathon.IO;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Converters;
global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.IO;
global using System.IO.Compression;
global using System.Linq;
global using System.Numerics;
global using System.Text;

namespace Marathon.IO
{
    /// <summary>
    /// Determines how files are read.
    /// <para>This is only necessary for classes inheriting the <see cref="Interfaces.IArchive"/> interface.</para>
    /// </summary>
    public enum ReadMode
    {
        /// <summary>
        /// Gets an index of the file's contents.
        /// </summary>
        IndexOnly,

        /// <summary>
        /// Copies the file's contents to memory.
        /// </summary>
        CopyToMemory
    }

    /// <summary>
    /// Determines how files are written.
    /// </summary>
    public enum WriteMode
    {
        /// <summary>
        /// Writes to the file directly.
        /// </summary>
        Fixed,

        /// <summary>
        /// Writes the entire file from scratch.
        /// </summary>
        Logical
    }

    /// <summary>
    /// A stream helper for format classes.
    /// </summary>
    public class FileBase : IDisposable
    {
        public FileBase(bool leaveOpen = false)
        {
            // Set stream disposal state.
            LeaveOpen = leaveOpen;
        }

        public FileBase(string file, bool saveOnLoad = false, ReadMode readMode = ReadMode.CopyToMemory, WriteMode writeMode = WriteMode.Logical, bool leaveOpen = false) : this(leaveOpen)
        {
            // Set file reading mode.
            ReadMode = readMode;

            // Set file writing mode.
            WriteMode = writeMode;

            // Load the file.
            Load(file);

            // Save the file.
            if (saveOnLoad)
                Save();
        }

        /// <summary>
        /// Location of the file loaded.
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// Determines what mode will be used for the reader.
        /// </summary>
        public virtual ReadMode ReadMode { get; set; } = ReadMode.CopyToMemory;

        /// <summary>
        /// Determines what mode will be used for the writer.
        /// </summary>
        public virtual WriteMode WriteMode { get; set; } = WriteMode.Logical;

        /// <summary>
        /// The stream used for file reading.
        /// </summary>
        public Stream FileStream { get; internal set; }

        /// <summary>
        /// Leaves <see cref="FileStream"/> open after saving.
        /// <para>If left open, the stream must manually be disposed using the <see cref="Dispose"/> method.</para>
        /// </summary>
        public virtual bool LeaveOpen { get; set; } = false;

        /// <summary>
        /// The signature of the file.
        /// </summary>
        public virtual object Signature { get; }

        /// <summary>
        /// The extension used for the file name.
        /// </summary>
        public virtual string Extension { get; }

        /// <summary>
        /// Prepares the file for stream reading.
        /// </summary>
        /// <param name="file">Location of file to load.</param>
        public virtual void Load(string file)
        {
            // The file path was null...
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException(nameof(file));

            // The file doesn't exist...
            if (!File.Exists(file))
                throw new FileNotFoundException("The specified file doesn't exist...", file);

            // Set loaded location.
            Location = file;

            // Open the file and read without complete handle ownership.
            FileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Call load function.
            Load(FileStream);
        }

        /// <summary>
        /// Loads the file from pre-streamed data.
        /// </summary>
        /// <param name="stream">Stream to use.</param>
        public virtual void Load(Stream stream) => throw new NotImplementedException();

        /// <summary>
        /// Prepares the file for stream writing using the input source.
        /// </summary>
        /// <param name="overwrite">Overwrite the original file.</param>
        public virtual void Save(bool overwrite = true) => Save(Location, overwrite);

        /// <summary>
        /// Prepares the file for stream writing.
        /// </summary>
        /// <param name="file">Location to write to.</param>
        /// <param name="overwrite">Overwrite the original file.</param>
        public virtual void Save(string file, bool overwrite = true)
        {
            // The file path was null...
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException(nameof(file));

            // The file already exists and we've been told explicitly not to overwrite...
            if (!overwrite && File.Exists(file))
                throw new IOException("Unable to save the specified file as it already exists...");

            // Dispose the reader stream if requested.
            if (!LeaveOpen)
                FileStream?.Dispose();

            switch (WriteMode)
            {
                case WriteMode.Logical:
                {
                    if (LeaveOpen && file == Location)
                    {
                        // Create temporary file location so we can still read from the open stream.
                        string temp = Path.Combine(Path.GetDirectoryName(Location), $".temp.{Path.GetRandomFileName()}.{Path.GetFileNameWithoutExtension(Location)}{Extension}#");

                        // Create the file from the temporary path.
                        using (var fileStream = File.Create(temp))
                            Save(fileStream);

                        /* We can't really keep the stream open if
                           we're saving to the same file anyway. */
                        Dispose();

                        // Move temporary file to the final path.
                        File.Move(temp, file, true);

                        break;
                    }

                    // Create the file from the input path.
                    using (var fileStream = File.Create(file))
                        Save(fileStream);

                    break;
                }

                case WriteMode.Fixed:
                {
                    if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
                    {
                        // Copy the fixed file to the new writing location.
                        if (file != Location)
                            File.Copy(Location, file, true);
                    }

                    // Write to a pre-existing file directly.
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                        Save(fileStream);

                    break;
                }
            }
        }

        /// <summary>
        /// Saves the file from pre-streamed data.
        /// </summary>
        /// <param name="stream">Stream to use.</param>
        public virtual void Save(Stream stream) => throw new NotImplementedException();

        /// <summary>
        /// Serialises object data to the JSON format.
        /// </summary>
        /// <param name="filePath">Path to write JSON.</param>
        public virtual void JsonSerialise(string filePath) => throw new NotImplementedException();

        /// <summary>
        /// Prepares file path for JSON serialisation.
        /// </summary>
        public void JsonSerialise(object data) => JsonSerialise($"{Location}.json", data);

        /// <summary>
        /// Serialises object data to the JSON format at the specified location with the input data.
        /// </summary>
        /// <param name="filePath">Path to write JSON.</param>
        /// <param name="data">Data to serialise.</param>
        public void JsonSerialise(string filePath, object data)
            => File.WriteAllText(filePath, JsonConvert.SerializeObject(data, Formatting.Indented));

        /// <summary>
        /// Deserialises object data from the JSON format.
        /// </summary>
        /// <param name="filePath">Path to read JSON.</param>
        public virtual void JsonDeserialise(string filePath) => throw new NotImplementedException();

        /// <summary>
        /// Deserialises object data from the JSON format at the specified location and returns it.
        /// </summary>
        /// <param name="filePath">Path to read JSON.</param>
        /// <param name="T">Generic type for deserialisation rules.</param>
        public T JsonDeserialise<T>(string filePath)
            => JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));

        /// <summary>
        /// Deserialises object data from the JSON format at the specified location and returns it.
        /// </summary>
        /// <param name="filePath">Path to read JSON.</param>
        /// <param name="T">Generic type for deserialisation rules.</param>
        public object JsonDeserialise(string filePath, Type type)
            => JsonConvert.DeserializeObject(File.ReadAllText(filePath), type);

        /// <summary>
        /// Disposes the file reader.
        /// </summary>
        public virtual void Dispose() 
            => FileStream?.Dispose();
    }
}