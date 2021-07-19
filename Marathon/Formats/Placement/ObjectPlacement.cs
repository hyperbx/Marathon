using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Marathon.Exceptions;
using Marathon.Helpers;
using Marathon.IO;
using Newtonsoft.Json;

namespace Marathon.Formats.Placement
{
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
        public Type DataType { get; set; }

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

        public const string Extension = ".set";

        public class FormatData
        {
            public string Name { get; set; }

            public List<SetObject> Objects = new();

            public List<SetGroup> Groups = new();

            public override string ToString() => Name;
        }

        public FormatData Data = new();

        /// <summary>
        /// Determines whether or not the JSON export should display indices per object.
        /// </summary>
        public bool DisplayIndex = true;

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.JumpAhead(0x0C); // Always 0. Padding?
            Data.Name = new string(reader.ReadChars(0x20)).Replace("\0", ""); // Usually test, but not always.

            // Basic Data Table.
            uint objectCount       = reader.ReadUInt32();
            uint objectTableOffset = reader.ReadUInt32();
            uint groupCount        = reader.ReadUInt32();
            uint groupTableOffset  = reader.ReadUInt32();

            // Objects.
            reader.JumpTo(objectTableOffset, true);
            for (int i = 0; i < objectCount; i++)
            {
                // Initialise new SetObject.
                SetObject setObject = new()
                {
                    Index = i, // Set object ID to the current index.
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
                        // Initialise new SetParameter.
                        SetParameter setParameter = new();

                        uint parameterType = reader.ReadUInt32(); // This parameter's data type.
                        switch (parameterType)
                        {
                            case 0: // Boolean
                            {
                                setParameter.DataType = typeof(bool);
                                setParameter.Data = reader.ReadUInt32() == 1;
                                reader.JumpAhead(0x0C);
                                break;
                            }

                            case 1: // Int32
                            {
                                setParameter.DataType = typeof(int);
                                setParameter.Data = reader.ReadInt32();
                                reader.JumpAhead(0x0C);
                                break;
                            }

                            case 2: // Single
                            {
                                setParameter.DataType = typeof(float);
                                setParameter.Data = reader.ReadSingle();
                                reader.JumpAhead(0x0C);
                                break;
                            }

                            case 3: // String
                            {
                                setParameter.DataType = typeof(string);

                                uint stringOffset = reader.ReadUInt32();
                                long stringParameterPosition = reader.BaseStream.Position;
                                reader.JumpTo(stringOffset, true);
                                setParameter.Data = reader.ReadNullTerminatedString();
                                reader.JumpTo(stringParameterPosition);

                                reader.JumpAhead(0x0C); // Always 1, 0 then the amount of characters minus one in the string?
                                break;
                            }

                            case 4: // Vector3
                            {
                                setParameter.DataType = typeof(Vector3);
                                setParameter.Data = reader.ReadVector3();
                                reader.JumpAhead(0x04);
                                break;
                            }

                            case 6: // UInt32
                            {
                                setParameter.DataType = typeof(uint);
                                setParameter.Data = reader.ReadUInt32();
                                reader.JumpAhead(0x0C);
                                break;
                            }

                            default:
                                throw new InvalidSetParameterType(parameterType, reader.BaseStream.Position - 4);
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
            for (int i = 0; i < groupCount; i++)
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

            // Write the name, but default to 'test' because Sonic Team.
            writer.WriteNullPaddedString(string.Concat(Data.Name == null ? "test".Take(0x20) : Data.Name.Take(0x20)), 0x20);

            // Basic Data Table.
            writer.Write(Data.Objects.Count);
            writer.AddOffset("objectTableOffset");
            writer.Write(Data.Groups.Count);
            writer.AddOffset("groupTableOffset");

            // Objects.
            writer.FillOffset("objectTableOffset", true);
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                // If the object's name is empty, add an offset to a null entry.
                if (string.IsNullOrEmpty(Data.Objects[i].Name))
                {
                    writer.AddOffset($"object{i}NullName");
                }
                else
                {
                    writer.AddString($"object{i}Name", Data.Objects[i].Name);
                }

                writer.AddString($"object{i}Type", Data.Objects[i].Type);

                writer.Write((byte)0x40);
                writer.WriteNulls(0x02);
                writer.Write(Data.Objects[i].StartInactive);
                writer.WriteNulls(0x0C);

                writer.Write(Data.Objects[i].Position);
                writer.Write(Data.Objects[i].DrawDistance);
                writer.Write(Data.Objects[i].Rotation);

                writer.Write(Data.Objects[i].Parameters.Count);
                writer.AddOffset($"object{i}Offsets");
            }

            // Object Parameters.
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                // Skip this loop if this object has no parameters.
                if (Data.Objects[i].Parameters.Count == 0)
                    continue;

                writer.FillOffset($"object{i}Offsets", true);

                for (int p = 0; p < Data.Objects[i].Parameters.Count; p++)
                {
                    switch(Data.Objects[i].Parameters[p].DataType.ToString())
                    {
                        case "System.Boolean":
                        {
                            writer.Write(0u);
                            writer.WriteBoolean32((bool)Data.Objects[i].Parameters[p].Data);
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.Int32":
                        {
                            writer.Write(1u);
                            writer.Write(Convert.ToInt32(Data.Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.Single":
                        {
                            writer.Write(2u);
                            writer.Write(Convert.ToSingle(Data.Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.String":
                        {
                            writer.Write(3u);
                                                                
                            // If the parameter's string is empty, add an offset to a null entry.
                            if (string.IsNullOrEmpty(Data.Objects[i].Parameters[p].Data.ToString()))
                            {
                                writer.AddOffset($"object{i}Parameter{p}NullString");
                            }
                            else
                            {
                                writer.AddString($"object{i}Parameter{p}String", Data.Objects[i].Parameters[p].Data.ToString());
                            }

                            writer.Write(1u);
                            writer.Write(0u);
                            writer.Write(Data.Objects[i].Parameters[p].Data.ToString().Length + 1);
                            break;
                        }

                        case "System.Numerics.Vector3":
                        {
                            writer.Write(4u);
                            writer.Write(VectorHelper.ParseVector3(Data.Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x04);
                            break;
                        }

                        case "System.UInt32":
                        {
                            writer.Write(6u);
                            writer.Write(Convert.ToUInt32(Data.Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        default:
                            throw new InvalidSetParameterType(Data.Objects[i].Parameters[p].DataType.ToString(), writer.BaseStream.Position);
                    }
                }
            }

            // Groups
            if (Data.Groups.Count != 0)
            {
                writer.FillOffset("groupTableOffset", true);
                for (int i = 0; i < Data.Groups.Count; i++)
                {
                    writer.AddString($"group{i}Name", Data.Groups[i].Name);

                    // If the group doesn't have a function, add an offset to a null entry.
                    if (string.IsNullOrEmpty(Data.Groups[i].Function))
                    {
                        writer.AddOffset($"group{i}NullFunction");
                    }
                    else
                    {
                        writer.AddString($"group{i}Function", Data.Groups[i].Function);
                    }

                    writer.Write(Data.Groups[i].Objects.Count);
                    writer.AddOffset($"group{i}ObjectList");
                }

                for (int i = 0; i < Data.Groups.Count; i++)
                {
                    // Skip this loop if this group has no objects associated with it (because that can happen apparently?)
                    if (Data.Groups[i].Objects.Count == 0)
                        continue;

                    writer.FillOffset($"group{i}ObjectList", true);

                    foreach (ulong ObjectID in Data.Groups[i].Objects)
                        writer.Write(ObjectID);
                }
            }

            /* ---------- Process null strings ---------- */

            // Objects.
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                if (string.IsNullOrEmpty(Data.Objects[i].Name))
                    writer.FillOffset($"object{i}NullName", true);

                for (int p = 0; p < Data.Objects[i].Parameters.Count; p++)
                {
                    if (Data.Objects[i].Parameters[p].DataType.ToString() == "System.String")
                    {
                        if (string.IsNullOrEmpty(Data.Objects[i].Parameters[p].Data.ToString()))
                        {
                            writer.FillOffset($"object{i}Parameter{p}NullString", true);
                            writer.WriteNulls(0x4);
                        }
                    }
                }
            }

            // Groups.
            for (int i = 0; i < Data.Groups.Count; i++)
            {
                if (string.IsNullOrEmpty(Data.Groups[i].Function))
                {
                    writer.FillOffset($"group{i}NullFunction", true);
                    writer.WriteNulls(0x4);
                }
            }

            writer.FinishWrite();
        }
    }
}
