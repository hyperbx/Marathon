// AssetPackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Radfordhound
 * Copyright (c) 2020 Knuxfan24
 * Copyright (c) 2020 HyperBE32
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
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// <para>File base for the PKG format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining specific assets with friendly names.</para>
    /// </summary>
    public class AssetPackage : FileBase
    {
        public AssetPackage() { }
        public AssetPackage(string file)
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

        public class TypeEntry
        {
            public string TypeName;
            public List<FileEntry> Files = new List<FileEntry>();
        }

        public class FileEntry
        {
            public string FriendlyName, FilePath;
        }

        public const string Extension = ".pkg";

        public List<TypeEntry> Types = new List<TypeEntry>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            // File related header stuff.
            uint fileCount = reader.ReadUInt32();
            uint fileEntriesPos = reader.ReadUInt32();

            // Type related header stuff.
            uint typeCount = reader.ReadUInt32();
            uint typeEntriesPos = reader.ReadUInt32();

            // Read Types.
            reader.JumpTo(typeEntriesPos, true);
            for (int i = 0; i < typeCount; i++)
            {
                TypeEntry type = new TypeEntry();

                // Store offsets for later.
                uint namePos = reader.ReadUInt32();
                uint typeFileCount = reader.ReadUInt32();
                uint filesPos = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to namePos and read null terminated string for type name.
                reader.JumpTo(namePos, true);
                type.TypeName = reader.ReadNullTerminatedString();

                reader.JumpTo(filesPos, true);

                // Read objects within this type.
                for (int f = 0; f < typeFileCount; f++)
                {
                    FileEntry file = new FileEntry();
                    uint friendlyNamePos = reader.ReadUInt32();
                    uint filePathPos = reader.ReadUInt32();

                    // Jump to offsets and read strings for the file name and path.
                    long iteratorPosition = reader.BaseStream.Position;
                    reader.JumpTo(friendlyNamePos, true);
                    file.FriendlyName = reader.ReadNullTerminatedString();
                    reader.JumpTo(filePathPos, true);
                    file.FilePath = reader.ReadNullTerminatedString();

                    // Save file entry and return to the previously stored position.
                    type.Files.Add(file);
                    reader.JumpTo(iteratorPosition);
                }

                Types.Add(type);
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(stream, header);

            // Calculate what we should set the File Count to in the finalised file.
            uint filesCount = 0;
            for (int i = 0; i < Types.Count; i++)
            {
                for (int c = 0; c < Types[i].Files.Count; c++)
                {
                    filesCount++;
                }
            }
            writer.Write(filesCount);

            writer.AddOffset("fileEntriesPos");
            writer.Write(Types.Count);
            writer.AddOffset("typeEntriesPos");

            // Wrtie Type Data.
            writer.FillInOffset("typeEntriesPos", true);
            for (int i = 0; i < Types.Count; i++)
            {
                writer.AddString($"typeName{i}", Types[i].TypeName);
                writer.Write(Types[i].Files.Count);
                writer.AddOffset($"typeFilesOffset{i}");
            }

            // Write File Data.
            writer.FillInOffset("fileEntriesPos", true);
            int objectNum = 0;
            for (int i = 0; i < Types.Count; i++)
            {
                writer.FillInOffset($"typeFilesOffset{i}", true);
                for (int f = 0; f < Types[i].Files.Count; f++)
                {
                    writer.AddString($"friendlyName{objectNum}", Types[i].Files[f].FriendlyName);
                    writer.AddString($"filePath{objectNum}", Types[i].Files[f].FilePath);
                    objectNum++;
                }
            }

            // Write the footer.
            writer.FinishWrite(header);
        }

        public void ExportXML(string filePath)
        {
            // Root Node.
            XElement rootElem = new XElement("PKG");

            for (int i = 0; i < Types.Count; i++)
            {
                // Type Node.
                XElement typeElem = new XElement("Type");
                typeElem.Add(new XAttribute("Name", Types[i].TypeName));

                // File Nodes.
                for (int f = 0; f < Types[i].Files.Count; f++)
                {
                    XElement fileElem = new XElement("File", Types[i].Files[f].FilePath);
                    fileElem.Add(new XAttribute("FriendlyName", Types[i].Files[f].FriendlyName));
                    typeElem.Add(fileElem);
                }

                // Add Type Node to Root Node.
                rootElem.Add(typeElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filePath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML.
            XDocument xml = XDocument.Load(filepath);

            // Loop through the Type Nodes.
            foreach (XElement typeElem in xml.Root.Elements("Type"))
            {
                // Create Type Entry with the Name set to the value of the Name Attribute Node.
                TypeEntry typeEntry = new TypeEntry { TypeName = typeElem.Attribute("Name").Value };

                // Loop through the File Nodes in this Type Node and create a File Entry for each.
                foreach (XElement fileElem in typeElem.Elements("File"))
                {
                    FileEntry fileEntry = new FileEntry {
                        FriendlyName = fileElem.Attribute("FriendlyName").Value,
                        FilePath = fileElem.Value
                    };

                    typeEntry.Files.Add(fileEntry);
                }

                Types.Add(typeEntry);
            }
        }
    }
}