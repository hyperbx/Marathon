using System;

namespace Marathon.Formats.Package
{
    /// <summary>
    /// File base for the PathObj.bin format.
    /// <para>Used in SONIC THE HEDGEHOG for defining valid objects for common_path_obj.</para>
    /// </summary>
    public class PathPackage : FileBase
    {
        public PathPackage() { }

        public PathPackage(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    PathObjects = JsonDeserialise<List<PathObject>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(PathObjects);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".bin";

        public List<PathObject> PathObjects { get; set; } = [];

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // Read the first entry's name to get the string table's position.
            uint stringTablePosition = reader.ReadUInt32();

            reader.JumpBehind(4);

            // Loop through and read entries.
            while (reader.BaseStream.Position < (stringTablePosition - 4) + 0x20)
            {
                var entry = new PathObject();
                
                // Read string offsets.
                uint nameOffset = reader.ReadUInt32();
                uint modelOffset = reader.ReadUInt32();
                uint animOffset = reader.ReadUInt32();
                uint textOffset = reader.ReadUInt32();
                uint matAnimOffset = reader.ReadUInt32();

                long position = reader.BaseStream.Position;

                if (nameOffset != 0)
                    entry.Name = reader.ReadNullTerminatedString(false, nameOffset, true);

                if (modelOffset != 0)
                    entry.Model = reader.ReadNullTerminatedString(false, modelOffset, true);

                if (animOffset != 0)
                    entry.Animation = reader.ReadNullTerminatedString(false, animOffset, true);

                if (textOffset != 0)
                    entry.Text = reader.ReadNullTerminatedString(false, textOffset, true);

                if (matAnimOffset != 0)
                    entry.MaterialAnimation = reader.ReadNullTerminatedString(false, matAnimOffset, true);

                PathObjects.Add(entry);

                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            for (int i = 0; i < PathObjects.Count; i++)
            {
                var pathObj = PathObjects[i];

                writer.AddString($"Name{i}", pathObj.Name);
                writer.AddString($"Model{i}", pathObj.Model);
                writer.AddString($"Animation{i}", pathObj.Animation);
                writer.AddString($"Text{i}", pathObj.Text);
                writer.AddString($"MaterialAnimation{i}", pathObj.MaterialAnimation);
            }

            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }

    public class PathObject
    {
        /// <summary>
        /// Name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Model this object should use.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Animation this object should use.
        /// </summary>
        public string Animation { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Material animation this object should use.
        /// </summary>
        public string MaterialAnimation { get; set; }

        public PathObject() { }

        public PathObject(string in_name, string in_model, string in_animation, string in_text, string in_materialAnimation)
        {
            Name = in_name;
            Model = in_model;
            Animation = in_animation;
            Text = in_text;
            MaterialAnimation = in_materialAnimation;
        }

        public override string ToString() => Name;
    }
}
