using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Helpers;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    public class ExplosionPackage : FileBase
    {
        // TODO: Understand the unknowns, clean up code.

        public class Entry
        {
            public string EntryName;
            public uint UnknownUInt32_1;
            public float UnknownFloat_1;
            public float UnknownFloat_2;
            public float UnknownFloat_3;
            public float UnknownFloat_4;
            public float UnknownFloat_5;
            public float UnknownFloat_6;
            public uint UnknownUInt32_2;
            public uint UnknownUInt32_3;
            public string ParticleFile;
            public string ParticleName;
            public string SceneBank;
            public string SoundName;
            public string LightName;
        }

        public List<Entry> Entries = new List<Entry>();

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

            while (reader.BaseStream.Position < offsetTableLength)
            {
                Entry @object = new Entry();

                uint nameOffset = reader.ReadUInt32();
                @object.UnknownUInt32_1 = reader.ReadUInt32();
                @object.UnknownFloat_1 = reader.ReadSingle();
                @object.UnknownFloat_2 = reader.ReadSingle();
                @object.UnknownFloat_3 = reader.ReadSingle();
                @object.UnknownFloat_4 = reader.ReadSingle();
                @object.UnknownFloat_5 = reader.ReadSingle();
                @object.UnknownFloat_6 = reader.ReadSingle();
                @object.UnknownUInt32_2 = reader.ReadUInt32();
                @object.UnknownUInt32_3 = reader.ReadUInt32();
                uint particleFileOffset = reader.ReadUInt32();
                uint particleNameOffset = reader.ReadUInt32();
                uint SceneBankOffset = reader.ReadUInt32();
                uint soundNameOffset = reader.ReadUInt32();
                uint lightNameOffset = reader.ReadUInt32();

                reader.JumpAhead(12); //Padding? All nulls in official file. Might be worth experimenting with to see if anything changes.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                reader.JumpTo(nameOffset, true);
                @object.EntryName = reader.ReadNullTerminatedString();

                reader.JumpTo(particleFileOffset, true);
                @object.ParticleFile = reader.ReadNullTerminatedString();

                reader.JumpTo(particleNameOffset, true);
                @object.ParticleName = reader.ReadNullTerminatedString();

                reader.JumpTo(SceneBankOffset, true);
                @object.SceneBank = reader.ReadNullTerminatedString();

                reader.JumpTo(soundNameOffset, true);
                @object.SoundName = reader.ReadNullTerminatedString();

                reader.JumpTo(lightNameOffset, true);
                @object.LightName = reader.ReadNullTerminatedString();

                reader.JumpTo(position);

                Entries.Add(@object);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            // Write the objects.
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"entry{i}Name", Entries[i].EntryName);
                writer.Write(Entries[i].UnknownUInt32_1);
                writer.Write(Entries[i].UnknownFloat_1);
                writer.Write(Entries[i].UnknownFloat_2);
                writer.Write(Entries[i].UnknownFloat_3);
                writer.Write(Entries[i].UnknownFloat_4);
                writer.Write(Entries[i].UnknownFloat_5);
                writer.Write(Entries[i].UnknownFloat_6);
                writer.Write(Entries[i].UnknownUInt32_2);
                writer.Write(Entries[i].UnknownUInt32_3);
                writer.AddString($"entry{i}ParticleFile", Entries[i].ParticleFile);
                writer.AddString($"entry{i}ParticleName", Entries[i].ParticleName);
                writer.AddString($"entry{i}SceneBank", Entries[i].SceneBank);
                writer.AddString($"entry{i}SoundName", Entries[i].SoundName);
                writer.AddString($"entry{i}LightName", Entries[i].LightName);
                writer.WriteNulls(12);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("Explosion");

            // Object elements.
            foreach (Entry obj in Entries)
            {
                XElement objElem = new XElement("Explosion");
                XAttribute NameAttr = new XAttribute("ObjectName", obj.EntryName);
                XElement UInt1Elem = new XElement("UnknownUInt32_1", obj.UnknownUInt32_1);
                XElement Float1Elem = new XElement("UnknownFloat_1", obj.UnknownFloat_1);
                XElement Float2Elem = new XElement("UnknownFloat_2", obj.UnknownFloat_2);
                XElement Float3Elem = new XElement("UnknownFloat_3", obj.UnknownFloat_3);
                XElement Float4Elem = new XElement("UnknownFloat_4", obj.UnknownFloat_4);
                XElement Float5Elem = new XElement("UnknownFloat_5", obj.UnknownFloat_5);
                XElement Float6Elem = new XElement("UnknownFloat_6", obj.UnknownFloat_6);
                XElement UInt2Elem = new XElement("UnknownUInt32_2", obj.UnknownUInt32_2);
                XElement UInt3Elem = new XElement("UnknownUInt32_3", obj.UnknownUInt32_3);

                XAttribute ParticleBankAttr = new XAttribute("ParticleBank", obj.ParticleFile);
                XElement ParticleElem = new XElement("Particle", obj.ParticleName);
                ParticleElem.Add(ParticleBankAttr);

                XAttribute SceneBankAttr = new XAttribute("SceneBank", obj.SceneBank);
                XElement SoundElem = new XElement("Sound", obj.SoundName);
                SoundElem.Add(SceneBankAttr);

                XElement LightElem = new XElement("Light", obj.LightName);

                objElem.Add(NameAttr, UInt1Elem, Float1Elem, Float2Elem, Float3Elem, Float4Elem, Float5Elem, Float6Elem, UInt2Elem, UInt3Elem, ParticleElem, SoundElem, LightElem);
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

            // Loop through explosion nodes.
            foreach (XElement explosionElem in xml.Root.Elements("Explosion"))
            {
                // Read explosion values.
                Entry entry = new Entry
                {
                    EntryName = explosionElem.Attribute("ObjectName").Value,
                    UnknownUInt32_1 = uint.Parse(explosionElem.Element("UnknownUInt32_1").Value),
                    UnknownFloat_1 = float.Parse(explosionElem.Element("UnknownFloat_1").Value),
                    UnknownFloat_2 = float.Parse(explosionElem.Element("UnknownFloat_2").Value),
                    UnknownFloat_3 = float.Parse(explosionElem.Element("UnknownFloat_3").Value),
                    UnknownFloat_4 = float.Parse(explosionElem.Element("UnknownFloat_4").Value),
                    UnknownFloat_5 = float.Parse(explosionElem.Element("UnknownFloat_5").Value),
                    UnknownFloat_6 = float.Parse(explosionElem.Element("UnknownFloat_6").Value),
                    UnknownUInt32_2 = uint.Parse(explosionElem.Element("UnknownUInt32_2").Value),
                    UnknownUInt32_3 = uint.Parse(explosionElem.Element("UnknownUInt32_3").Value),
                    ParticleFile = explosionElem.Element("Particle").Attribute("ParticleBank").Value,
                    ParticleName = explosionElem.Element("Particle").Value,
                    SceneBank = explosionElem.Element("Sound").Attribute("SceneBank").Value,
                    SoundName = explosionElem.Element("Sound").Value,
                    LightName = explosionElem.Element("Light").Value
                };

                // Add object to Entries list.
                Entries.Add(entry);
            }
        }
    }
}
