// PictureFont.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Text
{
    /// <summary>
    /// <para>File base for the PFT format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining placeholder images for <see cref="MessageTable"/> files.</para>
    /// </summary>
    public class PictureFont : FileBase
    {
        public PictureFont(string file)
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

        public class SubImage
        {
            public string Placeholder;
            public ushort X, Y, Width, Height;
        }

        public const string Signature = "FNTP", Extension = ".pft";

        public string Texture;

        public List<SubImage> Entries = new List<SubImage>();

        public override void Load(Stream stream)
        {
            // Header
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            uint texturePos = reader.ReadUInt32();
            uint placeholderEntries = reader.ReadUInt32();

            reader.JumpTo(reader.ReadUInt32(), true);
            long position = reader.BaseStream.Position;

            // Texture
            reader.JumpTo(texturePos, true);
            Texture = reader.ReadNullTerminatedString();

            reader.JumpTo(position, false);

            // Placeholders
            for (uint i = 0; i < placeholderEntries; i++)
            {
                uint placeholderEntry = reader.ReadUInt32();

                SubImage fontPicture = new SubImage
                {
                    X = reader.ReadUInt16(),
                    Y = reader.ReadUInt16(),
                    Width = reader.ReadUInt16(),
                    Height = reader.ReadUInt16()
                };

                position = reader.BaseStream.Position;

                reader.JumpTo(placeholderEntry, true);
                fontPicture.Placeholder = reader.ReadNullTerminatedString();
                reader.JumpTo(position, false);

                Entries.Add(fontPicture);
            }
        }

        public override void Save(Stream stream)
        {
            // Header
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer   = new BINAWriter(stream, header);
            writer.WriteSignature(Signature);

            // Texture
            writer.AddString("textureName", Texture);

            // Placeholders
            writer.Write((uint)Entries.Count);
            writer.AddOffset("placeholderEntriesPos");
            writer.FillInOffset("placeholderEntriesPos", false);

            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"placeholderName{i}", Entries[i].Placeholder);
                writer.Write(Entries[i].X);
                writer.Write(Entries[i].Y);
                writer.Write(Entries[i].Width);
                writer.Write(Entries[i].Height);
            }

            writer.FinishWrite(header);
        }

        public void ExportXML(string filePath)
        {
            // Header
            XElement rootElem = new XElement("PFT");

            // Texture
            XElement typeElem   = new XElement("Texture");
            XAttribute typeAttr = new XAttribute("File", Texture);
            typeElem.Add(typeAttr);

            // Placeholders
            for (int i = 0; i < Entries.Count; i++)
            {
                XElement pictureElem = new XElement("Picture", Entries[i].Placeholder);
                pictureElem.Add(new XAttribute("X", Entries[i].X));
                pictureElem.Add(new XAttribute("Y", Entries[i].Y));
                pictureElem.Add(new XAttribute("Width", Entries[i].Width));
                pictureElem.Add(new XAttribute("Height", Entries[i].Height));
                typeElem.Add(pictureElem);
            }

            rootElem.Add(typeElem);

            XDocument xml = new XDocument(rootElem);
            xml.Save(filePath);
        }

        public void ImportXML(string filePath)
        {
            XDocument xml = XDocument.Load(filePath);

            // Texture
            foreach (XElement textureElem in xml.Root.Elements("Texture"))
            {
                Texture = textureElem.Attribute("File").Value;

                // Placeholders
                foreach (XElement pictureElem in textureElem.Elements("Picture"))
                {
                    SubImage entry = new SubImage();
                    ushort.TryParse(pictureElem.Attribute("X").Value, out entry.X);
                    ushort.TryParse(pictureElem.Attribute("Y").Value, out entry.Y);
                    ushort.TryParse(pictureElem.Attribute("Width").Value, out entry.Width);
                    ushort.TryParse(pictureElem.Attribute("Height").Value, out entry.Height);
                    entry.Placeholder = pictureElem.Value;
                    Entries.Add(entry);
                }
            }
        }
    }
}