using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Miscellaneous
{
    public class EventPlayBook : FileBase
    {
        // TODO: XML Importing, figure out the unknown values, clean up and comment the code.
        public class Event
        {
            public string Name;
            public string Folder;
            public uint UnknownUInt32_1;
            public float UnknownFloat_1;
            public float UnknownFloat_2;
            public float UnknownFloat_3;
            public float UnknownFloat_4;
            public float UnknownFloat_5;
            public float UnknownFloat_6;
            public string Terrain;
            public string SceneLua;
            public string SceneBank;
            public string ParticleList;
            public string SubtitleMST;
        }

        public const string Signature = ".EPB", Extension = ".epb";

        public List<Event> Entries = new List<Event>();
        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature) throw new InvalidSignatureException(Signature, signature);

            uint UnknownUInt32_1 = reader.ReadUInt32();
            uint EventCount = reader.ReadUInt32();
            uint EventTableOffset = reader.ReadUInt32();

            // Jump to the Path Table's Offset for safety's sake, should always be ox30 really.
            reader.JumpTo(EventTableOffset, true);

            // Loop through based on PathTableCount.
            for (int i = 0; i < EventCount; i++)
            {
                Event @event = new Event();
                uint eventNameOffset = reader.ReadUInt32();
                uint eventFolderOffset = reader.ReadUInt32();
                @event.UnknownUInt32_1 = reader.ReadUInt32();
                @event.UnknownFloat_1 = reader.ReadSingle();
                @event.UnknownFloat_2 = reader.ReadSingle();
                @event.UnknownFloat_3 = reader.ReadSingle();
                @event.UnknownFloat_4 = reader.ReadSingle();
                @event.UnknownFloat_5 = reader.ReadSingle();
                @event.UnknownFloat_6 = reader.ReadSingle();
                uint terrainOffset = reader.ReadUInt32();
                uint sceneLuaOffset = reader.ReadUInt32();
                uint sceneBankOffset = reader.ReadUInt32();
                uint particleListOffset = reader.ReadUInt32();
                uint subtitleMSTOffset = reader.ReadUInt32();

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values, needs to check if the offset isn't 0 as it reads the .EPB signature otherwise..
                if (eventNameOffset != 0)
                {
                    reader.JumpTo(eventNameOffset, true);
                    @event.Name = reader.ReadNullTerminatedString();
                }

                if (eventFolderOffset != 0)
                {
                    reader.JumpTo(eventFolderOffset, true);
                    @event.Folder = reader.ReadNullTerminatedString();
                }

                if (terrainOffset != 0)
                {
                    reader.JumpTo(terrainOffset, true);
                    @event.Terrain = reader.ReadNullTerminatedString();
                }

                if (sceneLuaOffset != 0)
                {
                    reader.JumpTo(sceneLuaOffset, true);
                    @event.SceneLua = reader.ReadNullTerminatedString();
                }

                if (sceneBankOffset != 0)
                {
                    reader.JumpTo(sceneBankOffset, true);
                    @event.SceneBank = reader.ReadNullTerminatedString();
                }

                if (particleListOffset != 0)
                {
                    reader.JumpTo(particleListOffset, true);
                    @event.ParticleList = reader.ReadNullTerminatedString();
                }

                if (subtitleMSTOffset != 0)
                {
                    reader.JumpTo(subtitleMSTOffset, true);
                    @event.SubtitleMST = reader.ReadNullTerminatedString();
                }

                // Jump back to the saved position to read the next event.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Entries.Add(@event);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteSignature(Signature);
            writer.Write(537265920u);
            writer.Write(Entries.Count);
            writer.AddOffset("EventTableOffset");

            // Write the objects.
            writer.FillInOffset("EventTableOffset", true);
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"Event{i}Name", Entries[i].Name);
                writer.AddString($"Event{i}Folder", Entries[i].Folder);
                writer.Write(Entries[i].UnknownUInt32_1);
                writer.Write(Entries[i].UnknownFloat_1);
                writer.Write(Entries[i].UnknownFloat_2);
                writer.Write(Entries[i].UnknownFloat_3);
                writer.Write(Entries[i].UnknownFloat_4);
                writer.Write(Entries[i].UnknownFloat_5);
                writer.Write(Entries[i].UnknownFloat_6);
                writer.AddString($"Event{i}Terrain", Entries[i].Terrain);
                writer.AddString($"Event{i}SceneLua", Entries[i].SceneLua);
                writer.AddString($"Event{i}SceneBank", Entries[i].SceneBank);
                writer.AddString($"Event{i}ParticleList", Entries[i].ParticleList);
                writer.AddString($"Event{i}SubtitleMST", Entries[i].SubtitleMST);
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("EventPlayBook");

            // Object elements.
            foreach (Event @event in Entries)
            {
                XElement eventElem = new XElement("Event");
                XElement NameElem = new XElement("Name", @event.Name);
                XElement FolderElem = new XElement("Folder", @event.Folder);
                XElement UnknownUInt1Elem = new XElement("UnknownUInt32_1", @event.UnknownUInt32_1);
                XElement UnknownFloat1Elem = new XElement("UnknownFloat_1", @event.UnknownFloat_1);
                XElement UnknownFloat2Elem = new XElement("UnknownFloat_2", @event.UnknownFloat_2);
                XElement UnknownFloat3Elem = new XElement("UnknownFloat_3", @event.UnknownFloat_3);
                XElement UnknownFloat4Elem = new XElement("UnknownFloat_4", @event.UnknownFloat_4);
                XElement UnknownFloat5Elem = new XElement("UnknownFloat_5", @event.UnknownFloat_5);
                XElement UnknownFloat6Elem = new XElement("UnknownFloat_6", @event.UnknownFloat_6);
                XElement TerrainElem = new XElement("Terain", @event.Terrain);
                XElement SceneLuaElem = new XElement("SceneLua", @event.SceneLua);
                XElement SceneBankElem = new XElement("SceneBank", @event.SceneBank);
                XElement ParticleListElem = new XElement("ParticleList", @event.ParticleList);
                XElement SubtitleMSTElem = new XElement("SubtitleMST", @event.SubtitleMST);

                eventElem.Add(NameElem, FolderElem, UnknownUInt1Elem, UnknownFloat1Elem, UnknownFloat2Elem, UnknownFloat3Elem, UnknownFloat4Elem, UnknownFloat5Elem, UnknownFloat6Elem, TerrainElem, SceneLuaElem, SceneBankElem, ParticleListElem, SubtitleMSTElem);

                rootElem.Add(eventElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }
    }
}
