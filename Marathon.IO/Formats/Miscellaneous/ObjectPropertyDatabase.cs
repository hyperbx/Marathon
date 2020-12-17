// ObjectPropertyDatabase.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Knuxfan24
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

using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// <para>File base for the PROP format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining object properties.</para>
    /// </summary>
    public class ObjectPropertyDatabase : FileBase
    {
        public ObjectPropertyDatabase(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".xml":
                    ImportXML(file);
                    break;
                default:
                    Load(file);
                    break;
            }
        }

        public class Entry
        {
            public string Name;
            public List<Parameter> Parameters = new List<Parameter>();
        }

        public class Parameter
        {
            public string Name;
            public uint Type;
        }

        public string Name;
        public List<Entry> Entries = new List<Entry>();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            reader.JumpAhead(12); // Skip extra nulls.

            Name = new string(reader.ReadChars(32)); // Prop's name.
            uint objectCount = reader.ReadUInt32();  // Amount of objects in this Prop.
            uint offsetTable = reader.ReadUInt32();  // Position of this Prop's offset table (likely always the same as the data is right after it).

            // Get objects.
            for(int i = 0; i < objectCount; i++)
            {
                Entry entry = new Entry();
                uint objectNameOffset = reader.ReadUInt32();
                uint parameterCount   = reader.ReadUInt32();
                uint parameterOffset  = reader.ReadUInt32();

                reader.JumpAhead(8); // Always 0.

                long position = reader.BaseStream.Position;

                // Read object's name.
                reader.JumpTo(objectNameOffset, true);
                entry.Name = reader.ReadNullTerminatedString();

                // Get parameters.
                for(int p = 0; p < parameterCount; p++)
                {
                    reader.JumpTo(parameterOffset + (p * 0x18), true);

                    Parameter parameter = new Parameter()
                    {
                        Name = new string(reader.ReadChars(16)),
                        Type = reader.ReadUInt32()
                    };

                    entry.Parameters.Add(parameter);
                }

                reader.JumpTo(position, false);
                Entries.Add(entry);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteNulls(12);

            writer.Write(string.Concat(Name.Take(32)));
            writer.Write(Entries.Count);

            writer.AddOffset("offsetTable");

            // Objects.
            writer.FillInOffset("offsetTable", true);

            for(int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"object{i}Name", Entries[i].Name);
                writer.Write(Entries[i].Parameters.Count);

                if (Entries[i].Parameters.Count == 0)
                    writer.WriteNulls(4);
                else
                    writer.AddOffset($"object{i}ParameterOffset");

                writer.WriteNulls(8);
            }

            // Parameters.
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Parameters.Count != 0)
                {
                    writer.FillInOffset($"object{i}ParameterOffset", true);
                    for (int p = 0; p < Entries[i].Parameters.Count; p++)
                    {
                        writer.Write(string.Concat(Entries[i].Parameters[p].Name.Take(16)));
                        writer.Write(Entries[i].Parameters[p].Type);
                        writer.Write(p);
                    }
                }
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("Prop");

            // Replace empty characters with nothing.
            XAttribute propNameAttr = new XAttribute("Name", Name.Replace("\0", ""));
            rootElem.Add(propNameAttr);

            // Object elements.
            foreach (Entry entry in Entries)
            {
                XElement objElem = new XElement("Object");
                XAttribute objNameAttr = new XAttribute("Name", entry.Name.Replace("\0", ""));
                objElem.Add(objNameAttr);

                // Parameter Elements.
                foreach(Parameter parameter in entry.Parameters)
                {
                    XElement paramElem = new XElement("Parameter", parameter.Name.Replace("\0", ""));
                    XAttribute paramTypeElem = new XAttribute("Type", parameter.Type);
                    paramElem.Add(paramTypeElem);
                    objElem.Add(paramElem);
                }

                rootElem.Add(objElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML and get Name value.
            XDocument xml = XDocument.Load(filepath);
            Name = xml.Root.Attribute("Name").Value.PadRight(32, '\0');

            // Loop through object nodes.
            foreach (XElement objElem in xml.Root.Elements("Object"))
            {
                Entry entry = new Entry
                {
                    Name = objElem.Attribute("Name").Value
                };

                // Loop through object's parameter nodes.
                foreach (XElement paramElem in objElem.Elements("Parameter"))
                {
                    Parameter parameter = new Parameter
                    {
                        Name = paramElem.Value.PadRight(16, '\0'),
                        Type = uint.Parse(paramElem.Attribute("Type").Value)
                    };

                    entry.Parameters.Add(parameter);
                }

                Entries.Add(entry);
            }
        }
    }
}
