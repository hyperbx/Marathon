using libHSON;
using Marathon.Exceptions;
using Marathon.Extensions;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

// Format names:        Stage Set
// Format references:   Sonicteam::Prop::StageSetManagerRunner, LoadStageSet
// Format designers:    Sonic Team
// Format researchers:  Darío, Knuxfan24, Radfordhound
//
// Format research references:
// - https://github.com/DarioSamo/libgens-sonicglvl/blob/master/src/LibS06/S06Set.cpp (used with permission)

namespace Marathon.Formats.Placement
{
    /// <summary>
    /// Support for *.set files; used for object layouts.
    /// </summary>
    public class StageSet : FileBase
    {
        private const string _extension = ".set"; // "SET"

        /// <summary>
        /// The name of this stage set.
        /// </summary>
        public string Name { get; set; } = "test";

        /// <summary>
        /// The objects in this stage set.
        /// </summary>
        public List<StageSetObject> Objects { get; set; } = [];

        /// <summary>
        /// The groups in this stage set.
        /// </summary>
        public List<StageSetObjectGroup> Groups { get; set; } = [];

        /// <summary>
        /// The actors that describe the objects and their parameters (required for exporting).
        /// </summary>
        public List<Actor> Actors { get; set; } = [];

        public override string Extension => _extension;

        public StageSet() { }

        public StageSet(string in_path) : base(in_path) { }

        public StageSet(Stream in_stream) : base(in_stream) { }

        public StageSet(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            // Always null.
            reader.JumpAhead(0x0C);

            Name = reader.ReadStringFixedLength(0x20);

            var objectCount = reader.Read<uint>();
            var objectTableOffset = reader.Read<uint>();
            var groupCount = reader.Read<uint>();
            var groupTableOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + objectTableOffset);

            for (int i = 0; i < objectCount; i++)
            {
                var @object = new StageSetObject();

                var objectNameOffset = reader.Read<uint>();
                var objectTypeOffset = reader.Read<uint>();

                reader.JumpAhead(0x03); // TODO: unknown, always the same value (40 00 00).
                @object.StartInactive = reader.Read<bool>();
                reader.JumpAhead(0x0C); // Always null.

                @object.Position = reader.Read<Vector3>();
                @object.DrawDistance = reader.Read<float>();
                @object.Rotation = reader.Read<Quaternion>();
                
                var parameterCount = reader.Read<uint>();
                var parameterOffset = reader.Read<uint>();

                if (objectNameOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + objectNameOffset, () => @object.Name = reader.ReadStringNullTerminated());

                if (objectTypeOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + objectTypeOffset, () => @object.Type = reader.ReadStringNullTerminated());

                var pos = reader.Position;

                if (parameterOffset != 0 && parameterCount > 0)
                {
                    reader.JumpTo(BINAHeader.Size + parameterOffset);

                    for (int j = 0; j < parameterCount; j++)
                    {
                        var param = new StageSetObjectParameter()
                        {
                            Type = reader.Read<StageSetDataType>()
                        };

                        switch (param.Type)
                        {
                            case StageSetDataType.Boolean:
                                param.Value = reader.Read<uint>() != 0;
                                reader.JumpAhead(12);
                                break;

                            case StageSetDataType.Int32:
                            case StageSetDataType.Object:
                                param.Value = reader.Read<int>();
                                reader.JumpAhead(12);
                                break;

                            case StageSetDataType.Single:
                                param.Value = reader.Read<float>();
                                reader.JumpAhead(12);
                                break;

                            case StageSetDataType.String:
                            {
                                var stringOffset = reader.Read<uint>();
                                reader.ReadAtOffset(BINAHeader.Size + stringOffset, () => param.Value = reader.ReadStringNullTerminated());
                                reader.JumpAhead(12); // Always 1, 0, then the string length plus null terminator.
                                break;
                            }

                            case StageSetDataType.Vector3:
                                param.Value = reader.Read<Vector3>();
                                reader.JumpAhead(4);
                                break;

                            default:
                                throw new InvalidSetParameterType((uint)param.Type, reader.Position - 4);
                        }

                        @object.Parameters.Add(param);
                    }
                }

                reader.JumpTo(pos);

                Objects.Add(@object);
            }

            reader.JumpTo(BINAHeader.Size + groupTableOffset);

            for (int i = 0; i < groupCount; i++)
            {
                var group = new StageSetObjectGroup();

                var groupNameOffset = reader.Read<uint>();
                var groupFunctionOffset = reader.Read<uint>();
                var groupIndexCount = reader.Read<uint>();
                var groupIndexListOffset = reader.Read<uint>();

                if (groupNameOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + groupNameOffset, () => group.Name = reader.ReadStringNullTerminated());

                if (groupFunctionOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + groupFunctionOffset, () => group.Function = reader.ReadStringNullTerminated());

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + groupIndexListOffset);

                for (int j = 0; j < groupIndexCount; j++)
                    group.Objects.Add(reader.Read<ulong>());

                reader.JumpTo(pos);

                Groups.Add(group);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteNullBytes(12); // Always null.
            writer.WriteStringFixedLength(Name.Truncate(0x20), 0x20);
            writer.Write(Objects.Count);
            writer.Reserve<uint>("ObjectTableOffset");
            writer.Write(Groups.Count);

            if (Groups.Count <= 0)
            {
                writer.Write(0);
            }
            else
            {
                writer.Reserve<uint>("GroupTableOffset");
            }

            writer.WriteReserved("ObjectTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Objects.Count; i++)
            {
                var @object = Objects[i];

                // If the object's name is empty, add an offset to a null entry.
                if (string.IsNullOrEmpty(@object.Name))
                {
                    writer.Reserve<uint>($"Object{i}Name");
                }
                else
                {
                    writer.WriteStringOffset(@object.Name);
                }

                writer.WriteStringOffset(@object.Type);
                writer.WriteBytes([0x40, 0x00, 0x00]);
                writer.Write(@object.StartInactive);
                writer.WriteNullBytes(12);
                writer.Write(@object.Position);
                writer.Write(@object.DrawDistance);
                writer.Write(@object.Rotation);
                writer.Write(@object.Parameters.Count);

                if (@object.Parameters.Count <= 0)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Reserve<uint>($"Object{i}ParametersOffset");
                }
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].Parameters.Count <= 0)
                    continue;

                writer.WriteReserved($"Object{i}ParametersOffset", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Objects[i].Parameters.Count; j++)
                {
                    var type = Objects[i].Parameters[j].Type;

                    writer.Write(type);

                    switch (type)
                    {
                        case StageSetDataType.Boolean:
                            writer.Write((bool)Objects[i].Parameters[j].Value ? 1 : 0);
                            writer.WriteNullBytes(12);
                            break;

                        case StageSetDataType.Int32:
                        case StageSetDataType.Object:
                            writer.Write((int)Objects[i].Parameters[j].Value);
                            writer.WriteNullBytes(12);
                            break;

                        case StageSetDataType.Single:
                            writer.Write((float)Objects[i].Parameters[j].Value);
                            writer.WriteNullBytes(12);
                            break;

                        case StageSetDataType.String:
                        {
                            // If the parameter's name is empty, add an offset to a null entry.
                            if (string.IsNullOrEmpty(Objects[i].Parameters[j].Value.ToString()))
                            {
                                writer.Reserve<uint>($"Object{i}Parameter{j}String");
                            }
                            else
                            {
                                writer.WriteStringOffset(Objects[i].Parameters[j].Value.ToString());
                            }

                            writer.Write(1);
                            writer.Write(0);
                            writer.Write(Objects[i].Parameters[j].Value.ToString().Length + 1);

                            break;
                        }

                        case StageSetDataType.Vector3:
                            writer.Write((Vector3)Objects[i].Parameters[j].Value);
                            writer.WriteNullBytes(4);
                            break;

                        default:
                            throw new InvalidSetParameterType(Objects[i].Parameters[j].Type.ToString(), writer.Position);
                    }
                }
            }

            if (Groups.Count > 0)
            {
                writer.WriteReserved("GroupTableOffset", (uint)writer.Position - BINAHeader.Size);

                for (int i = 0; i < Groups.Count; i++)
                {
                    var group = Groups[i];

                    writer.WriteStringOffset(group.Name);

                    // If the group's function is empty, add an offset to a null entry.
                    if (string.IsNullOrEmpty(group.Function))
                    {
                        writer.Reserve<uint>($"Group{i}Function");
                    }
                    else
                    {
                        writer.WriteStringOffset(group.Function);
                    }

                    writer.Write(group.Objects.Count);

                    if (group.Objects.Count <= 0)
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Reserve<uint>($"Group{i}IndexListOffset");
                    }
                }

                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].Objects.Count <= 0)
                        continue;

                    writer.WriteReserved($"Group{i}IndexListOffset", (uint)writer.Position - BINAHeader.Size);

                    foreach (var id in Groups[i].Objects)
                        writer.Write(id);
                }
            }

            // Process null object strings.
            for (int i = 0; i < Objects.Count; i++)
            {
                if (string.IsNullOrEmpty(Objects[i].Name))
                    writer.WriteReserved($"Object{i}Name", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Objects[i].Parameters.Count; j++)
                {
                    if (Objects[i].Parameters[j].Type != StageSetDataType.String)
                        continue;

                    if (!string.IsNullOrEmpty(Objects[i].Parameters[j].Value.ToString()))
                        continue;

                    writer.WriteReserved($"Object{i}Parameter{j}String", (uint)writer.Position - BINAHeader.Size);
                    writer.Write(0);
                }
            }

            // Process null group strings.
            for (int i = 0; i < Groups.Count; i++)
            {
                if (!string.IsNullOrEmpty(Groups[i].Function))
                    continue;

                writer.WriteReserved($"Group{i}Function", (uint)writer.Position - BINAHeader.Size);
                writer.Write(0);
            }

            writer.FinishWrite();
        }

        public override void Import(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            FromHsonProject(Project.FromFile(in_path));
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.EnsureExtension(path, ".hson"));

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            var name = FileSystemHelper.TruncateAllExtensions(Path.GetFileName(in_path));

            ToHsonProject(name).Save(in_path, jsonOptions: new() { Indented = true });
        }

        public bool AddTemplatesFromFile(string in_path)
        {
            switch (Path.GetExtension(in_path))
            {
                case ".prop":
                    Actors.AddRange(TemplateFactory.ImportProp(in_path));
                    return true;

                case ".json":
                    Actors.AddRange(TemplateFactory.ImportJson(in_path));
                    return true;
            }

            return false;
        }

        public void FromHsonProject(Project in_hsonProject)
        {
            if (Actors.Count <= 0)
                throw new Exception("Actor templates are required for HSON projects.");

            var hsonGroups = new List<libHSON.Object>();
            var objectIDs = new Dictionary<Guid, int>();

            for (int i = in_hsonProject.Objects.Count - 1; i >= 0; i--)
            {
                var hsonObject = in_hsonProject.Objects[i];

                if (hsonObject.Type != "group")
                    continue;

                hsonGroups.Add(hsonObject);
                in_hsonProject.Objects.Remove(in_hsonProject.Objects[i]);
            }

            hsonGroups.Reverse();

            for (int i = 0; i < in_hsonProject.Objects.Count; i++)
            {
                var hsonObject = in_hsonProject.Objects[i];
                var actor = Actors.Find(x => x.Name == hsonObject.Type);

                objectIDs.Add(hsonObject.Id, i);

                Objects.Add(StageSetObject.FromHsonObject(hsonObject, actor));
            }

            // Resolve object IDs.
            for (int i = 0; i < in_hsonProject.Objects.Count; i++)
            {
                var hsonObject = in_hsonProject.Objects[i];
                var actor = Actors.Find(x => x.Name == hsonObject.Type);

                for (int j = 0; j < hsonObject.LocalParameters.Count; j++)
                {
                    var hsonParam = hsonObject.LocalParameters.ElementAt(j);

                    if (actor.Parameters[j].Type != StageSetDataType.Object)
                        continue;

                    if (!Guid.TryParse(hsonParam.Value.ValueString, out var out_guid))
                        continue;

                    if (objectIDs.TryGetValue(out_guid, out int out_id))
                    {
                        Objects[i].Parameters[j].Value = out_id;
                    }
                    else
                    {
                        Objects[i].Parameters[j].Value = -1;
                    }
                }
            }

            foreach (var hsonGroup in hsonGroups)
                Groups.Add(StageSetObjectGroup.FromHsonObject(hsonGroup, objectIDs));
        }

        public void FromHsonProject(string in_json)
        {
            FromHsonProject(Project.FromData(Encoding.UTF8.GetBytes(in_json)));
        }

        public Project ToHsonProject(string in_name = "", string in_description = "")
        {
            if (Actors.Count <= 0)
                throw new Exception("Actor templates are required for HSON projects.");

            if (string.IsNullOrEmpty(in_name))
            {
                if (string.IsNullOrEmpty(Location))
                {
                    in_name = string.IsNullOrEmpty(Name) ? "Untitled" : Name;
                }
                else
                {
                    in_name = FileSystemHelper.TruncateAllExtensions(Path.GetFileName(Location));
                }
            }

            if (string.IsNullOrEmpty(in_description))
                in_description = Name;

            var hsonProject = new Project
            {
                Metadata = new ProjectMetadata
                {
                    Name = in_name,
                    Description = in_description
                }
            };

            foreach (var @object in Objects)
                hsonProject.Objects.Add(@object.ToHsonObject(Actors.Find(x => x.Name == @object.Type)));

            // Resolve object references.
            for (int i = 0; i < Objects.Count; i++)
            {
                var @object = Objects[i];

                for (int j = 0; j < @object.Parameters.Count; j++)
                {
                    var param = @object.Parameters[j];

                    if (param.Type != StageSetDataType.Object)
                        continue;

                    if ((int)param.Value < 0)
                        continue;

                    var hsonParam = hsonProject.Objects[i].LocalParameters.ElementAt(j);

                    hsonParam.Value.ValueString = hsonProject.Objects[(int)param.Value].Id.ToString("B");
                }
            }

            foreach (var group in Groups)
                hsonProject.Objects.Add(group.ToHsonObject(hsonProject));

            return hsonProject;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class StageSetObject
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of this object's actor.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Determines whether the object should be inactive to be initialised later.
        /// </summary>
        public bool StartInactive { get; set; }

        /// <summary>
        /// The location of this object.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The distance the player must be in range for the object to be drawn.
        /// </summary>
        public float DrawDistance { get; set; }

        /// <summary>
        /// The rotation of this object.
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// The parameters pertaining to the object type.
        /// </summary>
        public List<StageSetObjectParameter> Parameters { get; set; } = [];

        public libHSON.Object ToHsonObject(Actor in_actor)
        {
            var hsonObject = new libHSON.Object
            (
                name: Name,
                type: Type,
                position: Position,
                rotation: Rotation
            );

            hsonObject.LocalCustomProperties.Add("startInactive", new libHSON.Parameter(StartInactive));
            hsonObject.LocalCustomProperties.Add("drawDistance", new libHSON.Parameter(DrawDistance));

            for (int i = 0; i < Parameters.Count; i++)
            {
                // The set format allows any amount of parameters
                // to be written here, regardless of what the prop
                // libraries have stored.
                if (i >= in_actor.Parameters.Count)
                    break;

                var param = Parameters[i];
                var hsonParam = new libHSON.Parameter();

                switch (param.Type)
                {
                    case StageSetDataType.Boolean:
                        hsonParam.ValueBoolean = (bool)param.Value;
                        break;

                    case StageSetDataType.Int32:
                        hsonParam.ValueSignedInteger = (int)param.Value;
                        break;

                    case StageSetDataType.Single:
                        hsonParam.ValueFloatingPoint = (float)param.Value;
                        break;

                    case StageSetDataType.String:
                        hsonParam.ValueString = (string)param.Value;
                        break;

                    case StageSetDataType.Vector3:
                        var v = (Vector3)param.Value;
                        hsonParam = new([new(v.X), new(v.Y), new(v.Z)]);
                        break;

                    case StageSetDataType.Object:
                        hsonParam.ValueString = Guid.Empty.ToString("B");
                        break;

                    default:
                        throw new ArgumentException($"Invalid parameter type: {param.Type}");
                }

                hsonObject.LocalParameters.Add(in_actor.Parameters[i].Name, hsonParam);
            }

            return hsonObject;
        }

        public static StageSetObject FromHsonObject(libHSON.Object in_hsonObject, Actor in_actor)
        {
            var result = new StageSetObject()
            {
                Name = in_hsonObject.Name,
                Type = in_hsonObject.Type,
                Position = in_hsonObject.HasSpecifiedPosition ? in_hsonObject.SpecifiedPosition.Value : in_hsonObject.LocalPosition,
                Rotation = in_hsonObject.HasSpecifiedRotation ? in_hsonObject.SpecifiedRotation.Value : in_hsonObject.LocalRotation
            };

            if (in_hsonObject.LocalCustomProperties.TryGetValue("startInactive", out var out_startInactive))
                result.StartInactive = out_startInactive.ValueBoolean;

            if (in_hsonObject.LocalCustomProperties.TryGetValue("drawDistance", out var out_drawDistance))
                result.DrawDistance = (float)out_drawDistance.ValueFloatingPoint;

            for (int i = 0; i < in_hsonObject.LocalParameters.Count; i++)
            {
                var hsonParam = in_hsonObject.LocalParameters.Values.ElementAt(i);

                var param = new StageSetObjectParameter()
                {
                    Type = in_actor.Parameters[i].Type
                };

                switch (param.Type)
                {
                    case StageSetDataType.Boolean:
                        param.Value = hsonParam.ValueBoolean;
                        break;

                    case StageSetDataType.Int32:
                        param.Value = (int)hsonParam.ValueSignedInteger;
                        break;

                    case StageSetDataType.Single:
                        param.Value = (float)hsonParam.ValueFloatingPoint;
                        break;

                    case StageSetDataType.String:
                        param.Value = hsonParam.ValueString;
                        break;

                    case StageSetDataType.Vector3:
                    {
                        var v = hsonParam.ValueArray;

                        if (v.Count < 3)
                            throw new InvalidDataException($"The Vector3 array had less fields than expected: {v.Count}");

                        if (v.Count > 3)
                            throw new InvalidDataException($"The Vector3 array has more fields than expected: {v.Count}");

                        param.Value = new Vector3
                        (
                            (float)v[0].ValueFloatingPoint,
                            (float)v[1].ValueFloatingPoint,
                            (float)v[2].ValueFloatingPoint
                        );

                        break;
                    }
                }

                result.Parameters.Add(param);
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class StageSetObjectParameter
    {
        /// <summary>
        /// The value of this parameter.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// The value's data type.
        /// </summary>
        public StageSetDataType Type { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class StageSetObjectGroup
    {
        private const float _hsonObjectOffsetY = 20.0f * 100.0f;

        /// <summary>
        /// The name of this group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The function in Lua called when the group is complete.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// The object indices that need to be destroyed for this group to be considered complete.
        /// </summary>
        public List<ulong> Objects { get; set; } = [];

        public StageSetObjectGroup() { }

        public StageSetObjectGroup(string in_name, string in_function = "", List<ulong> in_objects = null)
        {
            Name = in_name;
            Function = in_function;
            Objects = in_objects ?? [];
        }

        public libHSON.Object ToHsonObject(Project in_hsonProject)
        {
            var hsonObject = new libHSON.Object
            (
                name: Name,
                type: "group"
            );

            hsonObject.LocalCustomProperties.Add("function", new libHSON.Parameter(Function));

            var guids = new List<libHSON.Parameter>();

            if (Objects.Count > 0)
            {
                var positions = new List<Vector3>();
                var rotations = new List<Quaternion>();

                foreach (var id in Objects)
                {
                    if (in_hsonProject.Objects.Count < (int)id)
                        continue;

                    var hsonObjFromId = in_hsonProject.Objects[(int)id];

                    guids.Add(new libHSON.Parameter(hsonObjFromId.Id.ToString("B")));

                    if (hsonObjFromId.HasSpecifiedPosition)
                        positions.Add(hsonObjFromId.SpecifiedPosition.Value);

                    if (hsonObjFromId.HasSpecifiedRotation)
                        rotations.Add(hsonObjFromId.SpecifiedRotation.Value);
                }

                var centre = VectorHelper.Centre(positions);
                {
                    centre.Y += _hsonObjectOffsetY;
                }

                // Position this group object at the centre of its children.
                hsonObject.LocalPosition = centre;
                hsonObject.LocalRotation = VectorHelper.Average(rotations);
            }

            hsonObject.LocalCustomProperties.Add("objects", new libHSON.Parameter(guids));

            return hsonObject;
        }

        public static StageSetObjectGroup FromHsonObject(libHSON.Object in_hsonObject, Dictionary<Guid, int> in_objectIDs)
        {
            var result = new StageSetObjectGroup(in_hsonObject.Name);

            if (in_hsonObject.LocalCustomProperties.TryGetValue("function", out var out_function))
                result.Function = out_function.ValueString;

            if (!in_hsonObject.LocalCustomProperties.TryGetValue("objects", out var out_objects))
                return result;

            foreach (var @object in out_objects.ValueArray)
            {
                if (!Guid.TryParse(@object.ValueString, out var out_guid))
                    continue;

                result.Objects.Add((ulong)in_objectIDs[out_guid]);
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
