﻿namespace Marathon.Formats.Placement
{
    /// <summary>
    /// File base for the *.prop format.
    /// <para>Used in SONIC THE HEDGEHOG for defining object properties.</para>
    /// </summary>
    public class PropertyDatabase : FileBase
    {
        public PropertyDatabase() { }

        public PropertyDatabase(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Data = JsonDeserialise<FormatData>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Data);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".prop";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ObjectProperty> Objects { get; set; } = [];

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new(); 

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.JumpAhead(12); // Skip extra nulls.

            Data.Name        = reader.ReadNullPaddedString(0x20); // Prop's name.
            uint objectCount = reader.ReadUInt32();               // Amount of objects in this Prop.
            uint offsetTable = reader.ReadUInt32();               // Position of this Prop's offset table (likely always the same as the data is right after it).

            reader.JumpTo(offsetTable, true); // Should already be at this position, but its good practice to be absolutely sure.

            // Get objects.
            for (int i = 0; i < objectCount; i++)
            {
                ObjectProperty entry = new();

                uint objectNameOffset = reader.ReadUInt32();
                uint parameterCount   = reader.ReadUInt32();
                uint parameterOffset  = reader.ReadUInt32();

                reader.JumpAhead(8); // Always 0.

                long position = reader.BaseStream.Position;

                // Read object's name.
                entry.Name = reader.ReadNullTerminatedString(false, objectNameOffset, true);

                // Get parameters.
                for (int p = 0; p < parameterCount; p++)
                {
                    reader.JumpTo(parameterOffset + (p * 0x18), true);

                    ObjectParameter parameter = new()
                    {
                        Name = new string(reader.ReadChars(0x10)).Trim('\0'),
                        Type = (SetDataType)reader.ReadUInt32()
                    };

                    entry.Parameters.Add(parameter);
                }

                reader.JumpTo(position, false);
                Data.Objects.Add(entry);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteNulls(12);

            writer.WriteNullPaddedString(string.Concat(Data.Name.Take(0x20)), 0x20);
            writer.Write(Data.Objects.Count);

            writer.AddOffset("offsetTable");

            // Objects.
            writer.FillOffset("offsetTable", true);

            for (int i = 0; i < Data.Objects.Count; i++)
            {
                writer.AddString($"object{i}Name", Data.Objects[i].Name);
                writer.Write(Data.Objects[i].Parameters.Count);

                if (Data.Objects[i].Parameters.Count == 0)
                    writer.WriteNulls(4);
                else
                    writer.AddOffset($"object{i}ParameterOffset");

                writer.WriteNulls(8);
            }

            // Parameters.
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                if (Data.Objects[i].Parameters.Count != 0)
                {
                    writer.FillOffset($"object{i}ParameterOffset", true);
                    for (int p = 0; p < Data.Objects[i].Parameters.Count; p++)
                    {
                        writer.WriteNullPaddedString(string.Concat(Data.Objects[i].Parameters[p].Name.Take(0x10)), 0x10);
                        writer.Write((uint)Data.Objects[i].Parameters[p].Type);
                        writer.Write(p);
                    }
                }
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class ObjectProperty
    {
        public string Name { get; set; }

        public List<ObjectParameter> Parameters { get; set; } = [];

        public ObjectProperty() { }

        public ObjectProperty(string in_name, List<ObjectParameter> in_parameters)
        {
            Name = in_name;
            Parameters = in_parameters;
        }

        public override string ToString() => Name;
    }

    public class ObjectParameter
    {
        public string Name { get; set; }

        public SetDataType Type { get; set; }

        public ObjectParameter() { }

        public ObjectParameter(string in_name, SetDataType in_type)
        {
            Name = in_name;
            Type = in_type;
        }

        public override string ToString() => Name;
    }
}
