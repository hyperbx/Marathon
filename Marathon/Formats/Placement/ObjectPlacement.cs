using Marathon.Exceptions;
using Marathon.Helpers;
using Newtonsoft.Json;

namespace Marathon.Formats.Placement
{
    /// <summary>
    /// File base for the *.set format.
    /// <para>Used in SONIC THE HEDGEHOG for object layouts.</para>
    /// </summary>
    public class ObjectPlacement : FileBase
    {
        public ObjectPlacement() { }

        public ObjectPlacement(string file, bool serialise = false, bool displayIndex = true)
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
                    // Set index state.
                    DisplayIndex = displayIndex;

                    Load(file);

                    if (serialise)
                        JsonSerialise(Data);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".set";

        public class FormatData
        {
            public string Name { get; set; }

            public List<SetObject> Objects { get; set; } = new();

            public List<SetGroup> Groups { get; set; } = new();

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new();

        /// <summary>
        /// Determines whether or not the JSON export should display indices per object.
        /// </summary>
        public bool DisplayIndex { get; set; } = true;

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.JumpAhead(0x0C); // Always zero - maybe padding.
            Data.Name = reader.ReadNullPaddedString(0x20); // Usually 'test', but not always.

            // Read data table information.
            uint objectCount       = reader.ReadUInt32();
            uint objectTableOffset = reader.ReadUInt32();
            uint groupCount        = reader.ReadUInt32();
            uint groupTableOffset  = reader.ReadUInt32();

            // Jump to objects.
            reader.JumpTo(objectTableOffset, true);

            // Read objects.
            for (int o = 0; o < objectCount; o++)
            {
                // Initialise new object.
                SetObject setObject = new()
                {
                    Index = o, // Set object ID to the current index.
                    DisplayIndex = DisplayIndex
                };

                uint objectNameOffset = reader.ReadUInt32(); // Offset to this object's name.
                uint objectTypeOffset = reader.ReadUInt32(); // Offset to this object's type.

                reader.JumpAhead(0x03); // Skip the 40 00 00 bytes at the start of the object, as these seem to NEVER change.
                setObject.StartInactive = reader.ReadBoolean();
                reader.JumpAhead(0x0C); // Skip the remaining bytes which I believe to all be padding, as they're always 00?

                setObject.Position     = reader.ReadVector3();
                setObject.DrawDistance = reader.ReadSingle();
                setObject.Rotation     = reader.ReadQuaternion();
                
                uint parameterCount  = reader.ReadUInt32(); // Amount of parameters this object has.
                uint parameterOffset = reader.ReadUInt32(); // Offset to the data for this object's parameters.

                long position = reader.BaseStream.Position; // Save our current position in the stream so we can jump back after reading the name, type and parameters.

                // Read this object's name and type.
                if (objectNameOffset != 0)
                    setObject.Name = reader.ReadNullTerminatedString(false, objectNameOffset, true);

                if (objectTypeOffset != 0)
                    setObject.Type = reader.ReadNullTerminatedString(false, objectTypeOffset, true);

                // Parameters.
                if (parameterCount != 0)
                {
                    reader.JumpTo(parameterOffset, true);
                    for (int p = 0; p < parameterCount; p++)
                    {
                        // Read object data type.
                        ObjectDataType type = (ObjectDataType)reader.ReadUInt32();

                        // Initialise new parameter with the read type.
                        SetParameter setParameter = new()
                        {
                            Type = type
                        };

                        switch (type)
                        {
                            case ObjectDataType.Boolean:
                                setParameter.Data = reader.ReadBoolean(4);
                                reader.JumpAhead(0x0C);
                                break;

                            case ObjectDataType.Int32:
                                setParameter.Data = reader.ReadInt32();
                                reader.JumpAhead(0x0C);
                                break;

                            case ObjectDataType.Single:
                                setParameter.Data = reader.ReadSingle();
                                reader.JumpAhead(0x0C);
                                break;

                            case ObjectDataType.String:
                            {
                                uint stringOffset = reader.ReadUInt32();

                                // Store current position.
                                long stringParameterPosition = reader.BaseStream.Position;

                                // Read null-terminated string.
                                reader.JumpTo(stringOffset, true);
                                {
                                    setParameter.Data = reader.ReadNullTerminatedString();
                                }

                                // Jump back to previous position.
                                reader.JumpTo(stringParameterPosition);

                                // Always 1, 0 then the amount of characters minus one in the string?
                                reader.JumpAhead(0x0C);

                                break;
                            }

                            case ObjectDataType.Vector3:
                                setParameter.Data = reader.ReadVector3();
                                reader.JumpAhead(0x04);
                                break;

                            case ObjectDataType.UInt32:
                                setParameter.Data = reader.ReadUInt32();
                                reader.JumpAhead(0x0C);
                                break;

                            default:
                                throw new InvalidSetParameterType((uint)type, reader.BaseStream.Position - 4);
                        }

                        setObject.Parameters.Add(setParameter);
                    }
                }

                // Add this object to the list of objects then jump back to the previously saved position.
                Data.Objects.Add(setObject);
                reader.JumpTo(position);
            }

            // Groups.
            reader.JumpTo(groupTableOffset, true);
            for (int g = 0; g < groupCount; g++)
            {
                // Initialise new SetGroup.
                SetGroup setGroup = new();

                // Store offsets for this group and the amount of objects within it.
                uint groupNameOffset       = reader.ReadUInt32();
                uint groupFunctionOffset   = reader.ReadUInt32();
                uint groupObjectCount      = reader.ReadUInt32();
                uint groupObjectListOffset = reader.ReadUInt32();

                long position = reader.BaseStream.Position; // Save our current position in the stream so we can jump back after reading the name, type and list.

                // Read this group's name and type.
                if (groupNameOffset != 0)
                    setGroup.Name = reader.ReadNullTerminatedString(false, groupNameOffset, true);

                if (groupFunctionOffset != 0)
                    setGroup.Function = reader.ReadNullTerminatedString(false, groupFunctionOffset, true);

                // Read this group's object list
                reader.JumpTo(groupObjectListOffset, true);
                for (int o = 0; o < groupObjectCount; o++)
                    setGroup.Objects.Add(reader.ReadUInt64());

                // Add this group to the list of groups then jump back to the previously saved position.
                Data.Groups.Add(setGroup);
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteNulls(0x0C);

            // Write the name, but default to 'test', because Sonic Team.
            writer.WriteNullPaddedString(string.Concat(Data.Name == null ? "test".Take(0x20) : Data.Name.Take(0x20)), 0x20);

            // Write data table information.
            writer.Write(Data.Objects.Count);
            writer.AddOffset("objectTableOffset");
            writer.Write(Data.Groups.Count);
            writer.AddOffset("groupTableOffset");
            writer.FillOffset("objectTableOffset", true);

            // Set up objects.
            for (int o = 0; o < Data.Objects.Count; o++)
            {
                // If the object's name is empty, add an offset to a null entry.
                if (string.IsNullOrEmpty(Data.Objects[o].Name))
                {
                    writer.AddOffset($"object{o}NullName");
                }
                else
                {
                    writer.AddString($"object{o}Name", Data.Objects[o].Name);
                }

                writer.AddString($"object{o}Type", Data.Objects[o].Type);

                writer.Write((byte)0x40);
                writer.WriteNulls(0x02);
                writer.Write(Data.Objects[o].StartInactive);
                writer.WriteNulls(0x0C);

                writer.Write(Data.Objects[o].Position);
                writer.Write(Data.Objects[o].DrawDistance);
                writer.Write(Data.Objects[o].Rotation);

                writer.Write(Data.Objects[o].Parameters.Count);
                writer.AddOffset($"object{o}Offsets");
            }

            // Set up object parameters.
            for (int o = 0; o < Data.Objects.Count; o++)
            {
                // Skip this loop if this object has no parameters.
                if (Data.Objects[o].Parameters.Count == 0)
                    continue;

                writer.FillOffset($"object{o}Offsets", true);

                for (int p = 0; p < Data.Objects[o].Parameters.Count; p++)
                {
                    ObjectDataType type = Data.Objects[o].Parameters[p].Type;

                    // Write object data type.
                    writer.Write((uint)type);

                    switch (type)
                    {
                        case ObjectDataType.Boolean:
                            writer.WriteBoolean32((bool)Data.Objects[o].Parameters[p].Data);
                            writer.WriteNulls(0x0C);
                            break;

                        case ObjectDataType.Int32:
                            writer.Write(Convert.ToInt32(Data.Objects[o].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;

                        case ObjectDataType.Single:
                            writer.Write(Convert.ToSingle(Data.Objects[o].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;

                        case ObjectDataType.String:
                        {                
                            // If the parameter's string is empty, add an offset to a null entry.
                            if (string.IsNullOrEmpty(Data.Objects[o].Parameters[p].Data.ToString()))
                            {
                                writer.AddOffset($"object{o}Parameter{p}NullString");
                            }
                            else
                            {
                                writer.AddString($"object{o}Parameter{p}String", Data.Objects[o].Parameters[p].Data.ToString());
                            }

                            writer.Write(1u);
                            writer.Write(0u);
                            writer.Write(Data.Objects[o].Parameters[p].Data.ToString().Length + 1);

                            break;
                        }

                        case ObjectDataType.Vector3:
                            writer.Write(VectorHelper.ParseVector3(Data.Objects[o].Parameters[p].Data));
                            writer.WriteNulls(0x04);
                            break;

                        case ObjectDataType.UInt32:
                            writer.Write(Convert.ToUInt32(Data.Objects[o].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;

                        default:
                            throw new InvalidSetParameterType(Data.Objects[o].Parameters[p].Type.ToString(), writer.BaseStream.Position);
                    }
                }
            }

            // Set up groups.
            if (Data.Groups.Count != 0)
            {
                writer.FillOffset("groupTableOffset", true);

                for (int g = 0; g < Data.Groups.Count; g++)
                {
                    writer.AddString($"group{g}Name", Data.Groups[g].Name);

                    // If the group doesn't have a function, add an offset to a null entry.
                    if (string.IsNullOrEmpty(Data.Groups[g].Function))
                    {
                        writer.AddOffset($"group{g}NullFunction");
                    }
                    else
                    {
                        writer.AddString($"group{g}Function", Data.Groups[g].Function);
                    }

                    writer.Write(Data.Groups[g].Objects.Count);
                    writer.AddOffset($"group{g}ObjectList");
                }

                for (int g = 0; g < Data.Groups.Count; g++)
                {
                    /* Skip this loop if this group has no objects
                       associated with it (because that can happen apparently) */
                    if (Data.Groups[g].Objects.Count == 0)
                        continue;

                    writer.FillOffset($"group{g}ObjectList", true);

                    foreach (ulong ObjectID in Data.Groups[g].Objects)
                        writer.Write(ObjectID);
                }
            }

            // Process null object strings.
            for (int o = 0; o < Data.Objects.Count; o++)
            {
                if (string.IsNullOrEmpty(Data.Objects[o].Name))
                    writer.FillOffset($"object{o}NullName", true);

                for (int p = 0; p < Data.Objects[o].Parameters.Count; p++)
                {
                    if (Data.Objects[o].Parameters[p].Type == ObjectDataType.String)
                    {
                        if (string.IsNullOrEmpty(Data.Objects[o].Parameters[p].Data.ToString()))
                        {
                            writer.FillOffset($"object{o}Parameter{p}NullString", true);
                            writer.WriteNulls(0x4);
                        }
                    }
                }
            }

            // Process null group strings.
            for (int g = 0; g < Data.Groups.Count; g++)
            {
                if (string.IsNullOrEmpty(Data.Groups[g].Function))
                {
                    writer.FillOffset($"group{g}NullFunction", true);
                    writer.WriteNulls(0x4);
                }
            }

            writer.FinishWrite();
        }
    }

    public class SetObject
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of object.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Determines whether the object should be inactive to be initialised later.
        /// </summary>
        public bool StartInactive { get; set; }

        /// <summary>
        /// X/Y/Z co-ordinates for where the object should be located.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The distance the player must be in range for the object to be drawn.
        /// </summary>
        public float DrawDistance { get; set; }

        /// <summary>
        /// X/Y/Z/W Quaternion angle for how the object should be rotated.
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// The parameters pertaining to the object type.
        /// </summary>
        public List<SetParameter> Parameters { get; set; } = new();

        /// <summary>
        /// Determines whether or not the JSON export should display indices per object.
        /// </summary>
        [JsonIgnore]
        public bool DisplayIndex { get; set; }

        /// <summary>
        /// This object's index - not required for writing, just for convenience with SET grouping via JSON.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Determines whether or not <see cref="Index"/> should be serialised.
        /// </summary>
        public bool ShouldSerializeIndex() => DisplayIndex;

        public override string ToString() => Name;
    }

    public class SetParameter
    {
        /// <summary>
        /// The data pertaining to this parameter.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The data type for <see cref="Data"/>.
        /// </summary>
        public ObjectDataType Type { get; set; }

        public override string ToString() => Data.ToString();
    }

    public class SetGroup
    {
        /// <summary>
        /// The name of this group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The function in Lua called when the group is complete.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// The object indices required to be disposed for this group to be complete.
        /// </summary>
        public List<ulong> Objects { get; set; } = new();

        public override string ToString() => Name;
    }
}
