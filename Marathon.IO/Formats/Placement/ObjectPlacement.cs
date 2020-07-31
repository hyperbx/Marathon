// ObjectPlacement.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Radfordhound
 * Copyright (c) 2020 Knuxfan24
 * Copyright (c) 2020 HyperPolygon64
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;
using Marathon.IO.Helpers;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Placement
{
    /// <summary>
    /// File base for the Sonic '06 SET format.
    /// </summary>
    public class ObjectPlacement : FileBase
    {
        public class SetObject
        {
            public int ID;             // For convenience when writing XMLs.

            public string Name,        // Name of the object.
                          Type;        // Object type.

            public bool StartInactive; // Whether this object is always visible or has to be spawned by a Group.
            
            public float DrawDistance; // Maximum object draw distance.

            public Vector3 Position = new Vector3();       // Position of the object.
            public Quaternion Rotation = new Quaternion(); // Quaternion rotation of the object.
            
            public List<SetParameter> Parameters = new List<SetParameter>(); // Object parameters.
        }

        public class SetParameter
        {
            public object Data;        // Parameter data.
            
            public Type DataType;      // Data type of this parameter.
            
            public uint StringPadding; // TODO: Unknown - figure out if there's a pattern with these four bytes and replicate it properly.
        }

        public class SetGroup
        {
            public string Name,     // Name of the group.
                          Function; // Lua function executed when triggered.

            public List<uint> ObjectIDs = new List<uint>(); // Target IDs for grouped objects.
        }

        public const string Extension = ".set";

        public string Name;
        public List<SetObject> Objects = new List<SetObject>();
        public List<SetGroup> Groups = new List<SetGroup>();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            reader.JumpAhead(0xC);
            Name = new string(reader.ReadChars(32)); // TODO: Verify this is actually a 32 byte long char array.

            // Store object offsets.
            uint objectCount = reader.ReadUInt32();
            uint objectTableOffset = reader.ReadUInt32();

            // Store group offsets.
            uint groupCount = reader.ReadUInt32();
            uint groupTableOffset = reader.ReadUInt32();

            // Objects
            reader.JumpTo(objectTableOffset, true);
            for (int i = 0; i < objectCount; i++)
            {
                // Store offsets for the string table.
                uint objectNameOffset = reader.ReadUInt32();
                uint objectTypeOffset = reader.ReadUInt32();

                SetObject @object = new SetObject() { ID = i };

                reader.JumpAhead(3);  // Skip the 40 00 00 bytes at the start of the object, as these seem to NEVER change.

                @object.StartInactive = reader.ReadBoolean();

                reader.JumpAhead(12); // Skip the remaining bytes which I believe to all be padding, as they're always 00?

                @object.Position     = reader.ReadVector3();
                @object.DrawDistance = reader.ReadSingle();
                @object.Rotation     = reader.ReadQuaternion();

                // Store count of parameters for this object and their location in the file.
                uint parameterCount  = reader.ReadUInt32();
                uint parameterOffset = reader.ReadUInt32();

                long pos = reader.BaseStream.Position;

                // Read object name.
                reader.JumpTo(objectNameOffset, true);
                @object.Name = reader.ReadNullTerminatedString();

                // Read object type.
                reader.JumpTo(objectTypeOffset, true);
                @object.Type = reader.ReadNullTerminatedString();

                // Object Parameters
                reader.JumpTo(parameterOffset, true);

                for (int p = 0; p < parameterCount; p++)
                {
                    // Figure out the parameters data type.
                    uint parameterType = reader.ReadUInt32();

                    // Create new SET parameter.
                    SetParameter parameter = new SetParameter();

                    switch (parameterType)
                    {
                        case 0: // Boolean
                            parameter.DataType = typeof(bool);
                            parameter.Data = reader.ReadUInt32() == 1; // TODO: What does this actually do? It was written like this in HedgeLib but I don't understand why.

                            reader.JumpAhead(12); // TODO: Unknown - always 0 in official files?
                            break;

                        case 1: // Int32
                            parameter.DataType = typeof(int);
                            parameter.Data = reader.ReadInt32();

                            reader.JumpAhead(12); // TODO: Unknown - always 0 in official files?
                            break;

                        case 2: // Single
                            parameter.DataType = typeof(float);
                            parameter.Data = reader.ReadSingle();

                            reader.JumpAhead(12); // TODO: Unknown - always 0 in official files?
                            break;

                        case 3: // String
                            parameter.DataType = typeof(string);
                            uint stringOffset = reader.ReadUInt32();
                            long stringParameterPosition = reader.BaseStream.Position;
                            reader.JumpTo(stringOffset, true);
                            parameter.Data = reader.ReadNullTerminatedString();
                            reader.JumpTo(stringParameterPosition);

                            reader.JumpAhead(4); // TODO: Unknown - always 1 in official files?
                            reader.JumpAhead(4); // TODO: Unknown - always 0 in official files?

                            parameter.StringPadding = reader.ReadUInt32(); // TODO: Figure out if there's a pattern with these four bytes and replicate it properly.
                            break;

                        case 4: // Vector3
                            parameter.DataType = typeof(Vector3);
                            parameter.Data = reader.ReadVector3();

                            reader.JumpAhead(4); // TODO: Unknown - always 0 in official files?
                            break;

                        case 6: // UInt32
                            parameter.DataType = typeof(uint);
                            parameter.Data = reader.ReadUInt32();

                            reader.JumpAhead(12); // TODO: Unknown - always 0 in official files?
                            break;

                        default:
                            throw new InvalidSetParameterType(parameterType, reader.BaseStream.Position - 4);
                    }

                    @object.Parameters.Add(parameter);
                }

                // Save current object.
                Objects.Add(@object);

                // Return to the previously stored position.
                reader.JumpTo(pos);
            }

            // Groups
            reader.JumpTo(groupTableOffset, true);

            for (int i = 0; i < groupCount; i++)
            {
                // Create new SET group.
                SetGroup group = new SetGroup();

                // Store offsets for this group and the amount of objects within it.
                uint groupNameOffset       = reader.ReadUInt32();
                uint groupTypeOffset       = reader.ReadUInt32();
                uint groupObjectCount      = reader.ReadUInt32();
                uint groupObjectListOffset = reader.ReadUInt32();

                long pos = reader.BaseStream.Position;

                // Read group name.
                reader.JumpTo(groupNameOffset, true);
                group.Name = reader.ReadNullTerminatedString();

                // Read group type.
                reader.JumpTo(groupTypeOffset, true);
                group.Function = reader.ReadNullTerminatedString();

                // Group Object List
                reader.JumpTo(groupObjectListOffset, true);

                for (int o = 0; o < groupObjectCount; o++)
                {
                    reader.JumpAhead(4); // TODO: Unknown - either these IDs take up 8 bytes somehow or these 4 are just padding?
                    group.ObjectIDs.Add(reader.ReadUInt32());
                }

                // Save current group.
                Groups.Add(group);

                // Return to the previously stored position.
                reader.JumpTo(pos);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteNulls(0xC);

            if (Name != null) writer.Write(string.Concat(Name.Take(32))); // TODO: Verify this is actually a 32 byte long char array.
            else
            {
                // TODO: Make sure this writes the right amount.
                writer.WriteNullTerminatedString("test"); // 'test' seems to be the default name in the official files, only a few don't use it.
                writer.WriteNulls(0x1B);
            }

            // Write object offsets.
            writer.Write(Objects.Count);
            writer.AddOffset("objectTableOffset");

            // Write group count.
            writer.Write(Groups.Count);

            // Write group offset.
            if (Groups.Count != 0)
                writer.AddOffset("groupTableOffset");

            // If there are no groups, just write nothing.
            else
                writer.WriteNulls(4);

            // Objects
            writer.FillInOffset("objectTableOffset", true);

            for(int i = 0; i < Objects.Count; i++)
            {
                // Write object name and type.
                writer.AddString($"objectNameOffset{i}", Objects[i].Name, false);
                writer.AddString($"objectTypeOffset{i}", Objects[i].Type, false);

                // Write the 40 00 00 part.
                writer.Write((byte)64);
                writer.WriteNulls(2);

                // Write the StartInactive bool.
                writer.Write(Objects[i].StartInactive);

                // Write the remaining 12 null bytes.
                writer.WriteNulls(12);

                writer.Write(Objects[i].Position);
                writer.Write(Objects[i].DrawDistance);
                writer.Write(Objects[i].Rotation);
                writer.Write(Objects[i].Parameters.Count);

                // Don't add a parameter table offset if this object has none.
                if (Objects[i].Parameters.Count != 0)
                    writer.AddOffset($"objectParameterOffset{i}");
                else
                    writer.WriteNulls(4);
            }

            // Object Parameters
            int stringParamCount = 0;

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].Parameters.Count != 0)
                {
                    writer.FillInOffset($"objectParameterOffset{i}", true);

                    foreach (SetParameter parameter in Objects[i].Parameters)
                    {
                        switch (parameter.DataType.ToString()) // Read the parameters data type.
                        {
                            case "System.Boolean":
                                writer.Write(0);
                                writer.Write(((bool)parameter.Data) ? 1 : 0); // TODO: What does this actually do? It was written like this in HedgeLib but I don't understand why.

                                writer.WriteNulls(12);
                                break;

                            case "System.Int32":
                                writer.Write(1);
                                writer.Write((int)parameter.Data);

                                writer.WriteNulls(12);
                                break;

                            case "System.Single":
                                writer.Write(2);
                                writer.Write((float)parameter.Data);

                                writer.WriteNulls(12);
                                break;

                            case "System.String":
                                writer.Write(3);
                                writer.AddString($"stringParamOffset{stringParamCount}", (string)parameter.Data, false);
                                writer.Write(1);
                                writer.Write(0);

                                writer.Write(parameter.StringPadding); // TODO: Figure out if there's a pattern with these four bytes and replicate it properly.
                                stringParamCount++;
                                break;

                            case "Marathon.IO.Vector3":
                                writer.Write(4);
                                writer.Write((Vector3)parameter.Data);

                                writer.WriteNulls(4);
                                break;

                            case "System.UInt32":
                                writer.Write(6);
                                writer.Write((uint)parameter.Data);

                                writer.WriteNulls(12);
                                break;

                            default:
                                throw new Exception($"Got invalid data type {parameter.DataType} in Object Parameter at position {i}...");
                        }
                    }
                }
            }

            // Groups
            if (Groups.Count != 0)
            {
                writer.FillInOffset("groupTableOffset", true);

                for (int i = 0; i < Groups.Count; i++)
                {
                    writer.AddString($"groupNameOffset{i}", Groups[i].Name, false);
                    writer.AddString($"groupTypeOffset{i}", Groups[i].Function, false);
                    writer.Write(Groups[i].ObjectIDs.Count);
                    writer.AddOffset($"groupObjectList{i}");
                }

                for (int i = 0; i < Groups.Count; i++)
                {
                    writer.FillInOffset($"groupObjectList{i}", true);

                    for (int o = 0; o < Groups[i].ObjectIDs.Count; o++)
                    {
                        writer.Write(0); // Either these IDs take up 8 bytes somehow or these 4 are just padding?
                        writer.Write(Groups[i].ObjectIDs[o]);
                    }
                }
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filePath)
        {
            // Root element.
            XElement rootElem = new XElement("Placement");

            // Replace empty characters with nothing.
            XAttribute rootNameAttr = new XAttribute("Name", Name.Replace("\0", ""));
            rootElem.Add(rootNameAttr);

            // Objects
            foreach (SetObject @object in Objects)
            {
                // Create XML Nodes.
                XElement objNode = new XElement("Object");

                XAttribute objType = new XAttribute("Type", @object.Type);
                XAttribute objID   = new XAttribute("ID", @object.ID);

                XElement objName          = new XElement("Name", @object.Name);
                XElement objStartInactive = new XElement("StartInactive", @object.StartInactive);

                // Position.
                XElement objPosition  = new XElement("Position");
                XElement objPositionX = new XElement("X", @object.Position.X);
                XElement objPositionY = new XElement("Y", @object.Position.Y);
                XElement objPositionZ = new XElement("Z", @object.Position.Z);
                objPosition.Add(objPositionX, objPositionY, objPositionZ);

                // Draw Distance.
                XElement objDrawDistance = new XElement("DrawDistance", @object.DrawDistance);

                // Rotation.
                XElement objRotation  = new XElement("Rotation");
                XElement objRotationX = new XElement("X", @object.Rotation.X);
                XElement objRotationY = new XElement("Y", @object.Rotation.Y);
                XElement objRotationZ = new XElement("Z", @object.Rotation.Z);
                XElement objRotationW = new XElement("W", @object.Rotation.W);
                objRotation.Add(objRotationX, objRotationY, objRotationZ, objRotationW);

                // Parameters.
                XElement objParameters = new XElement("Parameters");

                foreach(SetParameter parameter in @object.Parameters)
                {
                    XElement parameterElem = new XElement("Parameter", parameter.Data);

                    XAttribute parameterDataType = new XAttribute("Type", parameter.DataType);

                    parameterElem.Add(parameterDataType);
                    objParameters.Add(parameterElem);
                }

                // Add Nodes to appropriate XML Elements.
                objNode.Add(objType, objID, objName, objStartInactive, objPosition, objDrawDistance, objRotation, objParameters);
                rootElem.Add(objNode);
            }

            foreach(SetGroup group in Groups)
            {
                // Create XML Nodes.
                XElement groupNode = new XElement("Group");
                XElement groupName = new XElement("Name", group.Name);
                XElement groupType = new XElement("Type", group.Function);

                // List of Object IDs
                XElement groupObjectsNode = new XElement("Objects");

                foreach (uint objectID in group.ObjectIDs)
                {
                    XElement groupEntry = new XElement("Object", objectID);
                    groupObjectsNode.Add(groupEntry);
                }

                // Add Nodes to appropriate XML Elements.
                groupNode.Add(groupName, groupType, groupObjectsNode);
                rootElem.Add(groupNode);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filePath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML and get Name value.
            XDocument xml = XDocument.Load(filepath);
            Name = xml.Root.Attribute("Name").Value.PadRight(32, '\0');

            // Loop through object nodes.
            foreach (XElement objElem in xml.Root.Elements("Object"))
            {
                SetObject Object = new SetObject()
                {
                    Name = objElem.Element("Name").Value,
                    Type = objElem.Attribute("Type").Value,
                    StartInactive = bool.Parse(objElem.Element("StartInactive").Value),
                    DrawDistance = float.Parse(objElem.Element("DrawDistance").Value)
                };

                // Position
                Object.Position.X = float.Parse(objElem.Element("Position").Element("X").Value);
                Object.Position.Y = float.Parse(objElem.Element("Position").Element("Y").Value);
                Object.Position.Z = float.Parse(objElem.Element("Position").Element("Z").Value);

                // Rotation
                Object.Rotation.X = float.Parse(objElem.Element("Rotation").Element("X").Value);
                Object.Rotation.Y = float.Parse(objElem.Element("Rotation").Element("Y").Value);
                Object.Rotation.Z = float.Parse(objElem.Element("Rotation").Element("Z").Value);
                Object.Rotation.W = float.Parse(objElem.Element("Rotation").Element("W").Value);

                foreach(XElement paramElem in objElem.Elements("Parameters").Elements("Parameter"))
                {
                    SetParameter parameter = new SetParameter();

                    string dataType = paramElem.Attribute("Type").Value;

                    switch (dataType)
                    {
                        case "System.Boolean":
                            parameter.DataType = typeof(bool);
                            parameter.Data = bool.Parse(paramElem.Value);
                            break;

                        case "System.Int32":
                            parameter.DataType = typeof(int);
                            parameter.Data = int.Parse(paramElem.Value);
                            break;

                        case "System.Single":
                            parameter.DataType = typeof(float);
                            parameter.Data = float.Parse(paramElem.Value);
                            break;

                        case "System.String":
                            parameter.DataType = typeof(string);
                            parameter.Data = paramElem.Value;
                            break;

                        case "Marathon.IO.Vector3":
                            parameter.DataType = typeof(Vector3);
                            string[] vectorArray = paramElem.Value.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Split(',');
                            parameter.Data = new Vector3(float.Parse(vectorArray[0]), float.Parse(vectorArray[1]), float.Parse(vectorArray[2]));
                            break;

                        case "System.UInt32":
                            parameter.DataType = typeof(uint);
                            parameter.Data = uint.Parse(paramElem.Value);
                            break;

                        default:
                            throw new Exception($"Got invalid data type {paramElem.Value} in Object Parameter...");
                    }

                    Object.Parameters.Add(parameter);
                }

                Objects.Add(Object);
            }

            // Loop through object nodes.
            foreach (XElement groupElem in xml.Root.Elements("Group"))
            {
                SetGroup group = new SetGroup()
                {
                    Name = groupElem.Element("Name").Value,
                    Function = groupElem.Element("Type").Value
                };

                foreach(XElement groupObject in groupElem.Elements("Objects").Elements("Object"))
                    group.ObjectIDs.Add(uint.Parse(groupObject.Value));

                Groups.Add(group);
            }
        }
    }
}
