using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO;

namespace Marathon.Formats.Text
{
    /// <summary>
    /// The data structure pertaining to each entry for the <see cref="PictureFont"/> format.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// The name of the picture.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The X position of the top-left corner of the picture.
        /// </summary>
        public ushort X { get; set; }

        /// <summary>
        /// The Y position of the top-left corner of the picture.
        /// </summary>
        public ushort Y { get; set; }

        /// <summary>
        /// The width of the picture.
        /// </summary>
        public ushort Width { get; set; }

        /// <summary>
        /// The height of the picture.
        /// </summary>
        public ushort Height { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the PFT format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining placeholder images for the <see cref="MessageTable"/> format.</para>
    /// </summary>
    public class PictureFont : FileBase
    {
        public PictureFont() { }

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

        public const string Signature = "FNTP",
                            Extension = ".pft";

        public string Texture;

        public List<Picture> Entries = new List<Picture>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            uint texturePos = reader.ReadUInt32();
            uint placeholderEntries = reader.ReadUInt32();

            reader.JumpTo(reader.ReadUInt32(), true);
            long position = reader.BaseStream.Position;

            reader.JumpTo(texturePos, true);
            Texture = reader.ReadNullTerminatedString();

            reader.JumpTo(position, false);
            for (uint i = 0; i < placeholderEntries; i++)
            {
                uint placeholderEntry = reader.ReadUInt32();

                Picture fontPicture = new()
                {
                    X = reader.ReadUInt16(),
                    Y = reader.ReadUInt16(),
                    Width = reader.ReadUInt16(),
                    Height = reader.ReadUInt16()
                };

                position = reader.BaseStream.Position;

                reader.JumpTo(placeholderEntry, true);
                fontPicture.Name = reader.ReadNullTerminatedString();
                reader.JumpTo(position, false);

                Entries.Add(fontPicture);
            }
        }

        public override void Save(Stream stream)
        {
            BINAHeader header = new();
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.AddString("textureName", Texture);

            writer.Write((uint)Entries.Count);
            writer.AddOffset("placeholderEntriesPos");
            writer.FillInOffset("placeholderEntriesPos", true);

            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"placeholderName{i}", Entries[i].Name);
                writer.Write(Entries[i].X);
                writer.Write(Entries[i].Y);
                writer.Write(Entries[i].Width);
                writer.Write(Entries[i].Height);
            }

            writer.FinishWrite();
        }

        public void ExportXML(string filePath)
        {
            XElement rootElem = new("PFT");

            XElement typeElem = new("Texture");
            typeElem.Add(new XAttribute("File", Texture));

            for (int i = 0; i < Entries.Count; i++)
            {
                XElement pictureElem = new("Picture", Entries[i].Name);
                pictureElem.Add(new XAttribute("X", Entries[i].X));
                pictureElem.Add(new XAttribute("Y", Entries[i].Y));
                pictureElem.Add(new XAttribute("Width", Entries[i].Width));
                pictureElem.Add(new XAttribute("Height", Entries[i].Height));

                typeElem.Add(pictureElem);
            }

            rootElem.Add(typeElem);

            XDocument xml = new(rootElem);
            xml.Save(filePath);
        }

        public void ImportXML(string filePath)
        {
            XDocument xml = XDocument.Load(filePath);

            foreach (XElement textureElem in xml.Root.Elements("Texture"))
            {
                Texture = textureElem.Attribute("File").Value;

                foreach (XElement pictureElem in textureElem.Elements("Picture"))
                {
                    Picture entry = new()
                    {
                        Name = pictureElem.Value,
                        X = ushort.Parse(pictureElem.Attribute("X").Value),
                        Y = ushort.Parse(pictureElem.Attribute("Y").Value),
                        Width = ushort.Parse(pictureElem.Attribute("Width").Value),
                        Height = ushort.Parse(pictureElem.Attribute("Height").Value)
                    };

                    Entries.Add(entry);
                }
            }
        }
    }
}