// Text.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2019 GerbilSoft
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
using System.Xml.Linq;
using Marathon.IO.Headers;
using Marathon.IO.Helpers;
using Marathon.IO.Exceptions;
using System.Collections.Generic;

namespace Marathon.IO.Formats.SonicNext
{
    /// <summary>
    /// File base for the Sonic '06 MST format.
    /// </summary>
    public class Text : FileBase
    {
        public class Entry
        {
            public string Name, Text, Placeholder = string.Empty;
        }

        public const string Signature = "WTXT", Extension = ".mst";

        public string Name;
        public List<Entry> Entries = new List<Entry>();

        public override void Load(Stream fileStream)
        {
            // BINA Header
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature) throw new InvalidSignatureException(Signature, signature);

            // Store offsets for the string table.
            uint stringTableOffset = reader.ReadUInt32(); // Location of the table of internal string names (including the name) in this file.
            uint stringCount = reader.ReadUInt32();       // Count of internal string enteries in this file.

            // Get name of MST.
            long namePos = reader.BaseStream.Position;
            reader.JumpTo(stringTableOffset, true);
            Name = reader.ReadNullTerminatedString();
            reader.JumpTo(namePos);

            for (int i = 0; i < stringCount; i++)
            {
                Entry entry = new Entry();

                // Store offsets for later.
                uint nameOffset = reader.ReadUInt32();
                uint textOffset = reader.ReadUInt32();
                uint placeholderOffset = reader.ReadUInt32();

                // Stores the current position to jump back to later.
                long pos = reader.BaseStream.Position;

                // Jump to offsets and read null terminated strings.
                reader.JumpTo(nameOffset, true);
                entry.Name = reader.ReadNullTerminatedString();
                reader.JumpTo(textOffset, true);
                entry.Text = reader.ReadNullTerminatedStringUTF16();

                // Not all entries have a placeholder value, entires that lack one have this offset set to zero.
                if (placeholderOffset != 0)
                {
                    reader.JumpTo(placeholderOffset, true);
                    entry.Placeholder = reader.ReadNullTerminatedString();
                }
                
                // Save text entry into the Entries list.
                Entries.Add(entry);

                // Return to the previously stored position.
                reader.JumpTo(pos);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteSignature(Signature);
            writer.AddString("Name", Name);
            writer.Write(Entries.Count);

            // Write offsets and string table names for each entry in this file.
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"nameOffset{i}", $"{Entries[i].Name}");
                writer.AddOffset($"textOffset{i}");
                writer.AddOffset($"placeholderOffset{i}");
            }

            // Fill text offsets in with the approriate entry's text value.
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.FillInOffset($"textOffset{i}", true);
                writer.WriteNullTerminatedStringUTF16(Entries[i].Text);
            }

            // Fill placeholder offsets in with the approriate entry's placeholder value.
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Placeholder != string.Empty)
                {
                    writer.FillInOffset($"placeholderOffset{i}", true);
                    writer.WriteNullTerminatedString(Entries[i].Placeholder);
                }
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filePath)
        {
            // Root element.
            XElement rootElem = new XElement("Text");
            XAttribute rootNameAttr = new XAttribute("Name", Name);
            rootElem.Add(rootNameAttr);

            // String elements.
            for (int i = 0; i < Entries.Count; i++)
            {
                // Escape the \f and \n characters.
                string text = Entries[i].Text.Replace("\f", "\\f").Replace("\n", "\\n");

                // Create XML elements.
                XElement message           = new XElement("Message", text);
                XAttribute indexAttr       = new XAttribute("Index", i);
                XAttribute nameAttr        = new XAttribute("Name", Entries[i].Name);
                XAttribute placeholderAttr = new XAttribute("Placeholder", Entries[i].Placeholder);

                // Add elements to the XML.
                message.Add(indexAttr, nameAttr, placeholderAttr);
                rootElem.Add(message);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filePath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML and get Name value.
            XDocument xml = XDocument.Load(filepath);
            Name = xml.Root.Attribute("Name").Value;

            // Loop through message nodes.
            foreach (var msgElement in xml.Root.Elements("Message"))
            {
                Entry entry = new Entry
                {
                    Name = msgElement.Attribute("Name").Value,
                    Placeholder = msgElement.Attribute("Placeholder").Value,

                    // Parse the escaped \f and \n characters.
                    Text = msgElement.Value.Replace("\\f", "\f").Replace("\\n", "\n")
                };

                //Add to list of Entries in this file.
                Entries.Add(entry);
            }
        }
    }
}
