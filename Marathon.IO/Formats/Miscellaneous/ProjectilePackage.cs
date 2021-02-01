// ProjectilePackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
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
    /// <para>File base for the ShotParameter.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for projectile properties.</para>
    /// </summary>
    public class ProjectilePackage : FileBase
    {
        public ProjectilePackage() { }

        public ProjectilePackage(string file)
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

        public class ShotParameter
        {
            public string Name,
                          Model,
                          UnknownString_1, // Only used by the Egg Buster missile?
                          UnknownString_2, // Only used by the Egg Buster missile?
                          Explosion,
                          ParticleBank1,
                          Particle1,
                          SceneBank,
                          Sound,
                          ParticleBank2,
                          Particle2,
                          ParticleBank3,
                          Particle3,
                          UnknownString_3; // Either empty or misfired stuff.

            public uint UnknownUInt32_1,
                        UnknownUInt32_2,
                        UnknownUInt32_3;

            public float UnknownFloat_1,
                         UnknownFloat_2,
                         UnknownFloat_3,
                         UnknownFloat_4,
                         UnknownFloat_5,
                         UnknownFloat_6,
                         UnknownFloat_7,
                         UnknownFloat_8,
                         UnknownFloat_9;
        }

        public const string Extension = ".bin";

        public List<ShotParameter> Entries = new List<ShotParameter>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the first string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            // Read stream until we've reached the end of the offset table.
            while (reader.BaseStream.Position < offsetTableLength)
            {
                // 26 chunks?
                ShotParameter entry = new ShotParameter();
                uint NameOffset = reader.ReadUInt32();
                uint ModelOffset = reader.ReadUInt32();
                uint UnknownString_1Offset = reader.ReadUInt32();
                uint UnknownString_2Offset = reader.ReadUInt32();
                entry.UnknownUInt32_1 = reader.ReadUInt32();
                entry.UnknownFloat_1 = reader.ReadSingle();
                entry.UnknownFloat_2 = reader.ReadSingle();
                entry.UnknownUInt32_2 = reader.ReadUInt32();
                entry.UnknownFloat_3 = reader.ReadSingle();
                entry.UnknownFloat_4 = reader.ReadSingle();
                entry.UnknownFloat_5 = reader.ReadSingle();
                entry.UnknownFloat_6 = reader.ReadSingle();
                entry.UnknownFloat_7 = reader.ReadSingle();
                entry.UnknownUInt32_3 = reader.ReadUInt32();
                entry.UnknownFloat_8 = reader.ReadSingle();
                entry.UnknownFloat_9 = reader.ReadSingle();
                uint ExplosionOffset = reader.ReadUInt32();
                uint ParticleBank1Offset = reader.ReadUInt32();
                uint Particle1Offset = reader.ReadUInt32();
                uint SceneBankOffset = reader.ReadUInt32();
                uint SoundOffset = reader.ReadUInt32();
                uint ParticleBank2Offset = reader.ReadUInt32();
                uint Particle2Offset = reader.ReadUInt32();
                uint ParticleBank3Offset = reader.ReadUInt32();
                uint Particle3Offset = reader.ReadUInt32();
                uint UnknownString_3Offset = reader.ReadUInt32();

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                reader.JumpTo(NameOffset, true);
                entry.Name = reader.ReadNullTerminatedString();

                reader.JumpTo(ModelOffset, true);
                entry.Model = reader.ReadNullTerminatedString();

                reader.JumpTo(UnknownString_1Offset, true);
                entry.UnknownString_1 = reader.ReadNullTerminatedString();

                reader.JumpTo(UnknownString_2Offset, true);
                entry.UnknownString_2 = reader.ReadNullTerminatedString();

                reader.JumpTo(ExplosionOffset, true);
                entry.Explosion = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBank1Offset, true);
                entry.ParticleBank1 = reader.ReadNullTerminatedString();

                reader.JumpTo(Particle1Offset, true);
                entry.Particle1 = reader.ReadNullTerminatedString();

                reader.JumpTo(SceneBankOffset, true);
                entry.SceneBank = reader.ReadNullTerminatedString();

                reader.JumpTo(SoundOffset, true);
                entry.Sound = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBank2Offset, true);
                entry.ParticleBank2 = reader.ReadNullTerminatedString();

                reader.JumpTo(Particle2Offset, true);
                entry.Particle2 = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBank3Offset, true);
                entry.ParticleBank3 = reader.ReadNullTerminatedString();

                reader.JumpTo(Particle3Offset, true);
                entry.Particle3 = reader.ReadNullTerminatedString();

                reader.JumpTo(UnknownString_3Offset, true);
                entry.UnknownString_3 = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next object.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Entries.Add(entry);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            // Write the objects.
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"entry{i}Name", Entries[i].Name);
                writer.AddString($"entry{i}Model", Entries[i].Model);
                writer.AddString($"entry{i}UnknownString_1", Entries[i].UnknownString_1);
                writer.AddString($"entry{i}UnknownString_2", Entries[i].UnknownString_2);
                writer.Write(Entries[i].UnknownUInt32_1);
                writer.Write(Entries[i].UnknownFloat_1);
                writer.Write(Entries[i].UnknownFloat_2);
                writer.Write(Entries[i].UnknownUInt32_2);
                writer.Write(Entries[i].UnknownFloat_3);
                writer.Write(Entries[i].UnknownFloat_4);
                writer.Write(Entries[i].UnknownFloat_5);
                writer.Write(Entries[i].UnknownFloat_6);
                writer.Write(Entries[i].UnknownFloat_7);
                writer.Write(Entries[i].UnknownUInt32_3);
                writer.Write(Entries[i].UnknownFloat_8);
                writer.Write(Entries[i].UnknownFloat_9);
                writer.AddString($"entry{i}Explosion", Entries[i].Explosion);
                writer.AddString($"entry{i}ParticleBank1", Entries[i].ParticleBank1);
                writer.AddString($"entry{i}Particle1", Entries[i].Particle1);
                writer.AddString($"entry{i}SceneBank", Entries[i].SceneBank);
                writer.AddString($"entry{i}Sound", Entries[i].Sound);
                writer.AddString($"entry{i}ParticleBank2", Entries[i].ParticleBank2);
                writer.AddString($"entry{i}Particle2", Entries[i].Particle2);
                writer.AddString($"entry{i}ParticleBank2", Entries[i].ParticleBank3);
                writer.AddString($"entry{i}Particle3", Entries[i].Particle3);
                writer.AddString($"entry{i}UnknownString_3", Entries[i].UnknownString_3);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("ShotParameter");

            // Object elements.
            foreach (ShotParameter entry in Entries)
            {
                XElement objElem    = new XElement("Parameter");
                XAttribute NameAttr = new XAttribute("Name", entry.Name);
                XElement ModelElem  = new XElement("Model", entry.Model);
                XElement UnknownString_1Elem = new XElement("UnknownString_1", entry.UnknownString_1);
                XElement UnknownString_2Elem = new XElement("UnknownString_2", entry.UnknownString_2);
                XElement UnknownUInt32_1Elem = new XElement("UnknownUInt32_1", entry.UnknownUInt32_1);
                XElement UnknownFloat_1Elem  = new XElement("UnknownFloat_1", entry.UnknownFloat_1);
                XElement UnknownFloat_2Elem  = new XElement("UnknownFloat_2", entry.UnknownFloat_2);
                XElement UnknownUInt32_2Elem = new XElement("UnknownUInt32_2", entry.UnknownUInt32_2);
                XElement UnknownFloat_3Elem  = new XElement("UnknownFloat_3", entry.UnknownFloat_3);
                XElement UnknownFloat_4Elem  = new XElement("UnknownFloat_4", entry.UnknownFloat_4);
                XElement UnknownFloat_5Elem  = new XElement("UnknownFloat_5", entry.UnknownFloat_5);
                XElement UnknownFloat_6Elem  = new XElement("UnknownFloat_6", entry.UnknownFloat_6);
                XElement UnknownFloat_7Elem  = new XElement("UnknownFloat_7", entry.UnknownFloat_7);
                XElement UnknownUInt32_3Elem = new XElement("UnknownUInt32_3", entry.UnknownUInt32_3);
                XElement UnknownFloat_8Elem  = new XElement("UnknownFloat_8", entry.UnknownFloat_8);
                XElement UnknownFloat_9Elem  = new XElement("UnknownFloat_9", entry.UnknownFloat_9);
                XElement ExplosionElem       = new XElement("Explosion", entry.Explosion);

                XAttribute ParticleBank1Attr = new XAttribute("ParticleBank1", entry.ParticleBank1);
                XElement Particle1Elem = new XElement("Particle1", entry.Particle1);

                Particle1Elem.Add(ParticleBank1Attr);

                XAttribute SceneBankAttr = new XAttribute("SceneBank", entry.SceneBank);
                XElement SoundElem = new XElement("Sound", entry.Sound);

                SoundElem.Add(SceneBankAttr);

                XAttribute ParticleBank2Attr = new XAttribute("ParticleBank2", entry.ParticleBank2);
                XElement Particle2Elem = new XElement("Particle2", entry.Particle2);

                Particle2Elem.Add(ParticleBank2Attr);

                XAttribute ParticleBank3Attr = new XAttribute("ParticleBank3", entry.ParticleBank3);
                XElement Particle3Elem = new XElement("Particle3", entry.Particle3);

                Particle3Elem.Add(ParticleBank3Attr);

                XElement UnknownString_3Elem = new XElement("UnknownString_3", entry.UnknownString_3);

                objElem.Add
                (
                    NameAttr, ModelElem, UnknownString_1Elem, UnknownString_2Elem, UnknownUInt32_1Elem, UnknownFloat_1Elem,
                    UnknownFloat_2Elem, UnknownUInt32_2Elem, UnknownFloat_3Elem, UnknownFloat_4Elem, UnknownFloat_5Elem,
                    UnknownFloat_6Elem, UnknownFloat_7Elem, UnknownUInt32_3Elem, UnknownFloat_8Elem, UnknownFloat_9Elem,
                    ExplosionElem, Particle1Elem, SoundElem, Particle2Elem, Particle3Elem, UnknownString_3Elem
                );

                rootElem.Add(objElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML.
            XDocument xml = XDocument.Load(filepath);

            // Loop through object nodes.
            foreach (XElement objectElem in xml.Root.Elements("Parameter"))
            {
                // Read object values.
                ShotParameter @object = new ShotParameter
                {
                    Name  = objectElem.Attribute("Name").Value,
                    Model = objectElem.Element("Model").Value,
                    UnknownString_1 = objectElem.Element("UnknownString_1").Value,
                    UnknownString_2 = objectElem.Element("UnknownString_2").Value,
                    UnknownUInt32_1 = uint.Parse(objectElem.Element("UnknownUInt32_1").Value),
                    UnknownFloat_1  = float.Parse(objectElem.Element("UnknownFloat_1").Value),
                    UnknownFloat_2  = float.Parse(objectElem.Element("UnknownFloat_2").Value),
                    UnknownUInt32_2 = uint.Parse(objectElem.Element("UnknownUInt32_2").Value),
                    UnknownFloat_3  = float.Parse(objectElem.Element("UnknownFloat_3").Value),
                    UnknownFloat_4  = float.Parse(objectElem.Element("UnknownFloat_4").Value),
                    UnknownFloat_5  = float.Parse(objectElem.Element("UnknownFloat_5").Value),
                    UnknownFloat_6  = float.Parse(objectElem.Element("UnknownFloat_6").Value),
                    UnknownFloat_7  = float.Parse(objectElem.Element("UnknownFloat_7").Value),
                    UnknownUInt32_3 = uint.Parse(objectElem.Element("UnknownUInt32_3").Value),
                    UnknownFloat_8  = float.Parse(objectElem.Element("UnknownFloat_8").Value),
                    UnknownFloat_9  = float.Parse(objectElem.Element("UnknownFloat_9").Value),
                    Explosion = objectElem.Element("Explosion").Value,
                    Particle1 = objectElem.Element("Particle1").Value,
                    ParticleBank1 = objectElem.Element("Particle1").Attribute("ParticleBank1").Value,
                    Sound = objectElem.Element("Sound").Value,
                    SceneBank = objectElem.Element("Sound").Attribute("SceneBank").Value,
                    Particle2 = objectElem.Element("Particle2").Value,
                    ParticleBank2 = objectElem.Element("Particle2").Attribute("ParticleBank2").Value,
                    Particle3 = objectElem.Element("Particle3").Value,
                    ParticleBank3   = objectElem.Element("Particle3").Attribute("ParticleBank3").Value,
                    UnknownString_3 = objectElem.Element("UnknownString_3").Value
                };

                // Add object to Entries list.
                Entries.Add(@object);
            }
        }
    }
}