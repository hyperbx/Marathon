using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Miscellaneous
{
    public class TimeEvent : FileBase
    {
        // TODO: Experiment with the values. Cleanup Code.
        public class Entry
        {
            public string TargetNode;
            public string ParticleBank;
            public string Particle;
            public Vector2 UnknownVector2_1; // These values seem to always be 1 off each other, not sure if they should be decoupled or not.
            public bool UnknownBoolean_1;
            public Vector3 ParticlePosition;
            public Vector3 UnknownVector3_2;
        }

        public string XNM;
        public List<Entry> Entries = new List<Entry>();

        public const string Signature = ".TEV", Extension = ".tev";
        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature) throw new InvalidSignatureException(Signature, signature);

            reader.JumpAhead(4); // Always null in official files.
            uint XNMOffset = reader.ReadUInt32();

            // Read XNM associated with this time event file.            
            long position = reader.BaseStream.Position; // Save poision so we can jump back.
            reader.JumpTo(XNMOffset, true);             // Jump to XNMOffset.
            XNM = reader.ReadNullTerminatedString();    // Read XNM string.
            reader.JumpTo(position);                    // Return to previous position.

            uint EntryCount       = reader.ReadUInt32();
            uint EventTableOffset = reader.ReadUInt32(); // Offset to the table of entries in this time event file..

            // Jump to the Event Table's Offset for safety's sake, should always be ox20 really.
            reader.JumpTo(EventTableOffset, true);

            // Loop through entries
            for (int i = 0; i < EntryCount; i++)
            {
                Entry entry = new Entry();

                reader.JumpAhead(4); // Always null in official files.
                uint TargetNodeOffset   = reader.ReadUInt32();
                uint ParticleBankOffset = reader.ReadUInt32();
                uint ParticleOffset     = reader.ReadUInt32();
                entry.UnknownVector2_1  = reader.ReadVector2();
                reader.JumpAhead(1); // Always null in official files. Padding?
                entry.UnknownBoolean_1 = reader.ReadBoolean();
                reader.JumpAhead(2); // Always null in official files. Padding?
                entry.ParticlePosition  = reader.ReadVector3();
                entry.UnknownVector3_2  = reader.ReadVector3();

                // Store current position to jump back to for the next entry.
                position = reader.BaseStream.Position;

                reader.JumpTo(TargetNodeOffset, true);
                entry.TargetNode = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBankOffset, true);
                entry.ParticleBank = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleOffset, true);
                entry.Particle = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next entry..
                reader.JumpTo(position);

                // Save event entry into the Entries list.
                Entries.Add(entry);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteSignature(Signature);
            writer.WriteNulls(4);
            writer.AddString("XNMOffset", XNM);
            writer.Write(Entries.Count);
            writer.AddOffset("EntriesOffset");

            // Write the event entries.
            writer.FillInOffset("EntriesOffset", true);
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.WriteNulls(4);
                writer.AddString($"TargetNode{i}", Entries[i].TargetNode);
                writer.AddString($"ParticleBank{i}", Entries[i].ParticleBank);
                writer.AddString($"Particle{i}", Entries[i].Particle);
                writer.Write(Entries[i].UnknownVector2_1);
                writer.WriteNulls(1);
                writer.Write(Entries[i].UnknownBoolean_1);
                writer.WriteNulls(2);
                writer.Write(Entries[i].ParticlePosition);
                writer.Write(Entries[i].UnknownVector3_2);
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        // Clean this Exporter up, lots of bollocks.
        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("TimeEvent");
            XAttribute xnmAttr = new XAttribute("XNM", XNM);

            // Event elements.
            foreach (Entry entry in Entries)
            {
                XElement eventElem = new XElement("Event");
                XElement UnknownString1Elem = new XElement("TargetNode", entry.TargetNode);
                XAttribute ParticleBankAttr = new XAttribute("ParticleBank", entry.ParticleBank);
                XElement ParticleElem = new XElement("Particle", entry.Particle);
                ParticleElem.Add(ParticleBankAttr);

                XElement UnknownVector1Elem = new XElement("UnknownVector2_1");
                XElement Unknown1XElem = new XElement("X", entry.UnknownVector2_1.X);
                XElement Unknown1YElem = new XElement("Y", entry.UnknownVector2_1.Y);
                UnknownVector1Elem.Add(Unknown1XElem, Unknown1YElem);

                XElement UShort1Elem = new XElement("UnknownBoolean_1", entry.UnknownBoolean_1);

                XElement UnknownVector2Elem = new XElement("ParticlePosition");
                XElement Unknown2XElem = new XElement("X", entry.ParticlePosition.X);
                XElement Unknown2YElem = new XElement("Y", entry.ParticlePosition.Y);
                XElement Unknown2ZElem = new XElement("Z", entry.ParticlePosition.Z);
                UnknownVector2Elem.Add(Unknown2XElem, Unknown2YElem, Unknown2ZElem);

                XElement UnknownVector3Elem = new XElement("UnknownVector3_3");
                XElement Unknown3XElem = new XElement("X", entry.UnknownVector3_2.X);
                XElement Unknown3YElem = new XElement("Y", entry.UnknownVector3_2.Y);
                XElement Unknown3ZElem = new XElement("Z", entry.UnknownVector3_2.Z);
                UnknownVector3Elem.Add(Unknown3XElem, Unknown3YElem, Unknown3ZElem);

                eventElem.Add(xnmAttr, UnknownString1Elem, ParticleElem, UnknownVector1Elem, UShort1Elem, UnknownVector2Elem, UnknownVector3Elem);
                rootElem.Add(eventElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }
    }
}