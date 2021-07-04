using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using Marathon.IO;
using Marathon.Helpers;
using Newtonsoft.Json;

namespace Marathon.Formats.Placement
{
    public class SetObject
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool StartInactive { get; set; }

        public Vector3 Position { get; set; }

        public float DrawDistance { get; set; }

        public Quaternion Rotation { get; set; }

        public List<SetParameter> Parameters = new();

        public override string ToString() => Name;
    }

    public class SetParameter
    {
        public object Data { get; set; }

        public Type DataType { get; set; }

        public override string ToString() => Data.ToString();
    }

    public class SetGroup
    {
        public string Name { get; set; }

        public string Function { get; set; }

        public List<ulong> Objects = new();

        public override string ToString() => Name;
    }

    public class ObjectPlacement : FileBase
    {
        public ObjectPlacement() { }

        public ObjectPlacement(string file)
        {
            switch (StringHelper.GetFullExtension(file))
            {
                case ".objects.json":
                    JsonDeserialise(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public string Name;

        public List<SetObject> Objects = new();

        public List<SetGroup> Groups = new();

        public override void Load(Stream fileStream)
        {
            // Header.
            BINAReader reader = new BINAReader(fileStream);

            reader.JumpAhead(0x0C); // Always 0. Padding?
            Name = new string(reader.ReadChars(32)).Replace("\0", ""); // Usually test, but not always.

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
                SetObject setObject = new();

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
                {
                    reader.JumpTo(objectNameOffset, true);
                    setObject.Name = reader.ReadNullTerminatedString();
                }

                if (objectTypeOffset != 0)
                {
                    reader.JumpTo(objectTypeOffset, true);
                    setObject.Type = reader.ReadNullTerminatedString();
                }

                // Parameters.
                if (parameterCount != 0)
                {
                    reader.JumpTo(parameterOffset, true);
                    for (int p = 0; p < parameterCount; p++)
                    {
                        // Initialise new SetParameter.
                        SetParameter setParameter = new SetParameter();

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
                                throw new Exception($"Got invalid data type {parameterType} in Object Parameter at position {reader.BaseStream.Position - 4}...");
                        }

                        setObject.Parameters.Add(setParameter);
                    }
                }

                // Add this object to the list of objects then jump back to the previously saved position.
                Objects.Add(setObject);
                reader.JumpTo(position);
            }

            // Groups.
            reader.JumpTo(groupTableOffset, true);
            for (int i = 0; i < groupCount; i++)
            {
                // Initialise new SetGroup.
                SetGroup setGroup = new SetGroup();

                // Store offsets for this group and the amount of objects within it.
                uint groupNameOffset       = reader.ReadUInt32();
                uint groupFunctionOffset   = reader.ReadUInt32();
                uint groupObjectCount      = reader.ReadUInt32();
                uint groupObjectListOffset = reader.ReadUInt32();

                long position = reader.BaseStream.Position; // Save our current position in the stream so we can jump back after reading the name, type and list.

                // Read this group's name and type.
                if (groupNameOffset != 0)
                {
                    reader.JumpTo(groupNameOffset, true);
                    setGroup.Name = reader.ReadNullTerminatedString();
                }
                if (groupFunctionOffset != 0)
                {
                    reader.JumpTo(groupFunctionOffset, true);
                    setGroup.Function = reader.ReadNullTerminatedString();
                }

                // Read this group's object list
                reader.JumpTo(groupObjectListOffset, true);
                for (int o = 0; o < groupObjectCount; o++)
                {
                    setGroup.Objects.Add(reader.ReadUInt64());
                }

                // Add this group to the list of groups then jump back to the previously saved position.
                Groups.Add(setGroup);
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream fileStream)
        {
            // Header.
            BINAWriter writer = new BINAWriter(fileStream);

            writer.WriteNulls(0x0C);

            // Write the name, but default to 'test' because Sonic Team.
            writer.Write(string.Concat(Name == null ? "test".Take(32) : Name.Take(32)).PadRight(32, '\0'));

            // Basic Data Table.
            writer.Write(Objects.Count);
            writer.AddOffset("objectTableOffset");
            writer.Write(Groups.Count);
            writer.AddOffset("groupTableOffset");

            // Objects.
            writer.FillInOffset("objectTableOffset", true);
            for (int i = 0; i < Objects.Count; i++)
            {
                writer.AddString($"object{i}Name", Objects[i].Name);
                writer.AddString($"object{i}Type", Objects[i].Type);

                writer.Write((byte)0x40);
                writer.WriteNulls(0x02);
                writer.Write(Objects[i].StartInactive);
                writer.WriteNulls(0x0C);

                writer.Write(Objects[i].Position);
                writer.Write(Objects[i].DrawDistance);
                writer.Write(Objects[i].Rotation);

                writer.Write(Objects[i].Parameters.Count);
                writer.AddOffset($"object{i}Offsets");
            }

            // Object Parameters.
            for (int i = 0; i < Objects.Count; i++)
            {
                // Skip this loop if this object has no parameters.
                if (Objects[i].Parameters.Count == 0) { continue; }
                writer.FillInOffset($"object{i}Offsets", true);

                for (int p = 0; p < Objects[i].Parameters.Count; p++)
                {
                    switch(Objects[i].Parameters[p].DataType.ToString())
                    {
                        case "System.Boolean":
                        {
                            writer.Write(0u);
                            writer.WriteBoolean32((bool)Objects[i].Parameters[p].Data);
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.Int32":
                        {
                            writer.Write(1u);
                            writer.Write(Convert.ToInt32(Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.Single":
                        {
                            writer.Write(2u);
                            writer.Write(Convert.ToSingle(Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        case "System.String":
                        {
                            writer.Write(3u);
                                                                
                            // If the parameter's string is empty, add an offset to a Dud Entry because SETs work that way. Because Sonic Team.
                            if (Objects[i].Parameters[p].Data.ToString() == "")
                            {
                                writer.AddOffset($"object{i}Parameter{p}DudString");
                            }
                            else
                            {
                                writer.AddString($"object{i}Parameter{p}String", Objects[i].Parameters[p].Data.ToString());
                            }

                            writer.Write(1u);
                            writer.Write(0u);
                            writer.Write(Objects[i].Parameters[p].Data.ToString().Length + 1);
                            break;
                        }

                        case "System.Numerics.Vector3":
                        {
                            writer.Write(4u);
                            writer.Write(VectorHelper.ParseVector3(Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x04);
                            break;
                        }

                        case "System.UInt32":
                        {
                            writer.Write(6u);
                            writer.Write(Convert.ToUInt32(Objects[i].Parameters[p].Data));
                            writer.WriteNulls(0x0C);
                            break;
                        }

                        default:
                            throw new Exception($"Got invalid data type {Objects[i].Parameters[p].DataType.ToString()} in Object Parameter at position {writer.BaseStream.Position}...");
                    }
                }
            }

            // Groups
            if (Groups.Count != 0)
            {
                writer.FillInOffset("groupTableOffset", true);
                for (int i = 0; i < Groups.Count; i++)
                {
                    writer.AddString($"group{i}Name", Groups[i].Name);

                    // If the group doesn't have a function, add an offset to a Dud Entry because SETs work that way. Because Sonic Team.
                    if (Groups[i].Function == "")
                    {
                        writer.AddOffset($"group{i}DudFunction");
                    }
                    else
                    {
                        writer.AddString($"group{i}Function", Groups[i].Function);
                    }

                    writer.Write(Groups[i].Objects.Count);
                    writer.AddOffset($"group{i}ObjectList");
                }

                for (int i = 0; i < Groups.Count; i++)
                {
                    // Skip this loop if this group has no objects associated with it (because that can happen apparently?)
                    if (Groups[i].Objects.Count == 0)
                        continue;

                    writer.FillInOffset($"group{i}ObjectList", true);

                    foreach (ulong ObjectID in Groups[i].Objects)
                        writer.Write(ObjectID);
                }
            }

            // Dud Strings.
            // Objects.
            for (int i = 0; i < Objects.Count; i++)
            {
                for (int p = 0; p < Objects[i].Parameters.Count; p++)
                {
                    if (Objects[i].Parameters[p].DataType.ToString() == "System.String")
                    {
                        if (Objects[i].Parameters[p].Data.ToString() == "")
                        {
                            writer.FillInOffset($"object{i}Parameter{p}DudString", true);
                            writer.WriteNulls(0x4);
                        }
                    }
                }
            }

            // Groups.
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Function == "")
                {
                    writer.FillInOffset($"group{i}DudFunction", true);
                    writer.WriteNulls(0x4);
                }
            }

            writer.FinishWrite();
        }

        public override void JsonSerialise(string filePath)
        {
            File.WriteAllText(StringHelper.ReplaceFilename(filePath, StringHelper.AppendToFileName(filePath, ".objects")), JsonConvert.SerializeObject(Objects, Formatting.Indented));
            File.WriteAllText(StringHelper.ReplaceFilename(filePath, StringHelper.AppendToFileName(filePath, ".groups")), JsonConvert.SerializeObject(Groups, Formatting.Indented));
        }

        public override void JsonDeserialise(string filePath)
        {
            string groups = $"{StringHelper.RemoveFullExtension(filePath)}.groups.json";

            Objects.AddRange(JsonConvert.DeserializeObject<List<SetObject>>(File.ReadAllText(filePath)));

            if (File.Exists(groups))
                Groups.AddRange(JsonConvert.DeserializeObject<List<SetGroup>>(File.ReadAllText(groups)));
        }
    }
}
