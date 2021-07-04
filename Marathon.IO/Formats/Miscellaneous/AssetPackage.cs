// AssetPackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Radfordhound
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
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

        public class AssetType
        {
            public string Name { get; set; }

            public List<AssetFile> Files = new List<AssetFile>();
        }

        public class AssetFile
        {
            public string Name { get; set; }

            public string File { get; set; }
        }

        public const string Extension = ".pkg";

        public List<AssetType> Types = new List<AssetType>();

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
                AssetType type = new AssetType();

                // Store offsets for later.
                uint namePos       = reader.ReadUInt32();
                uint typeFileCount = reader.ReadUInt32();
                uint filesPos      = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to namePos and read null terminated string for type name.
                reader.JumpTo(namePos, true);
                type.Name = reader.ReadNullTerminatedString();

                reader.JumpTo(filesPos, true);

                // Read objects within this type.
                for (int f = 0; f < typeFileCount; f++)
                {
                    AssetFile file = new AssetFile();
                    uint friendlyNamePos = reader.ReadUInt32();
                    uint filePathPos = reader.ReadUInt32();

                    // Jump to offsets and read strings for the file name and path.
                    long iteratorPosition = reader.BaseStream.Position;

                    reader.JumpTo(friendlyNamePos, true);
                    file.Name = reader.ReadNullTerminatedString();

                    reader.JumpTo(filePathPos, true);
                    file.File = reader.ReadNullTerminatedString();

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

            // Storage for calculated file count.
            uint filesCount = 0;

            // Calculate what we should set the file count to.
            for (int i = 0; i < Types.Count; i++)
            {
                for (int c = 0; c < Types[i].Files.Count; c++)
                {
                    filesCount++;
                }
            }

            // Write file count.
            writer.Write(filesCount);

            // Store offset for file entries.
            writer.AddOffset("fileEntriesPos");

            // Write type count.
            writer.Write(Types.Count);

            // Store offset for type entries.
            writer.AddOffset("typeEntriesPos");

            // Fill in types offset just before we write the type data.
            writer.FillInOffset("typeEntriesPos", true);

            // Write type data.
            for (int i = 0; i < Types.Count; i++)
            {
                writer.AddString($"typeName{i}", Types[i].Name);
                writer.Write(Types[i].Files.Count);
                writer.AddOffset($"typeFilesOffset{i}");
            }

            // Fill in files offset just before we write the file data.
            writer.FillInOffset("fileEntriesPos", true);

            // Storage for number of files written.
            int objectNum = 0;

            // Write file data.
            for (int i = 0; i < Types.Count; i++)
            {
                writer.FillInOffset($"typeFilesOffset{i}", true);

                for (int f = 0; f < Types[i].Files.Count; f++)
                {
                    writer.AddString($"friendlyName{objectNum}", Types[i].Files[f].Name);
                    writer.AddString($"filePath{objectNum}", Types[i].Files[f].File);

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
                // Type node.
                XElement typeElem = new XElement("Type");
                typeElem.Add(new XAttribute("Name", Types[i].Name));

                // File nodes.
                for (int f = 0; f < Types[i].Files.Count; f++)
                {
                    XElement fileElem = new XElement("File", Types[i].Files[f].File);
                    fileElem.Add(new XAttribute("Name", Types[i].Files[f].Name));
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
                AssetType typeEntry = new AssetType
                {
                    Name = typeElem.Attribute("Name").Value
                };

                // Loop through the File Nodes in this Type Node and create a File Entry for each.
                foreach (XElement fileElem in typeElem.Elements("File"))
                {
                    AssetFile fileEntry = new AssetFile
                    {
                        Name = fileElem.Attribute("Name").Value,
                        File = fileElem.Value
                    };

                    typeEntry.Files.Add(fileEntry);
                }

                Types.Add(typeEntry);
            }
        }
    }
}