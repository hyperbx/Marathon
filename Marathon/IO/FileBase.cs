using System;
using System.IO;
using Newtonsoft.Json;

namespace Marathon.IO
{
    /// <summary>
    /// Determines how files are streamed.
    /// </summary>
    public enum FileWriteMode
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
    public class FileBase
    {
        /// <summary>
        /// Location of the file loaded.
        /// </summary>
        public string Location;

        /// <summary>
        /// Determines what mode will be used for the stream.
        /// </summary>
        public FileWriteMode FileWriteMode { get; set; } = FileWriteMode.Logical;

        public FileBase() { }

        public FileBase(string file, FileWriteMode writeMode = FileWriteMode.Logical)
        {
            // Set file writing mode.
            FileWriteMode = writeMode;

            // Load the file immediately.
            Load(file);
        }

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

            // Open the file and read.
            using (var fileStream = File.OpenRead(file))
                Load(fileStream);
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
                throw new Exception("Unable to save the specified file as it already exists...");

            switch (FileWriteMode)
            {
                case FileWriteMode.Logical:
                {
                    // Create the file using the stream.
                    using (var fileStream = File.Create(file))
                        Save(fileStream);

                    break;
                }

                case FileWriteMode.Fixed:
                {
                    if (!string.IsNullOrEmpty(Location) && File.Exists(Location))
                    {
                        // Don't bother copying if it's the same location.
                        if (file == Location)
                            return;

                        // Copy the fixed file to the new writing location.
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
    }
}