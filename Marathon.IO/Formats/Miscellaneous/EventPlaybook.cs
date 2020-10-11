// EventPlaybook.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Miscellaneous
{
    public class EventPlaybook : FileBase
    {
        public class Event
        {
            public string Name,
                          Folder,       // TODO: Make sure this is actually a folder reference of some sort.
                          Terrain,
                          SceneLua,
                          SceneBank,    // TODO: Make sure this is actually a reference to an SBK.
                          ParticleList,
                          SubtitleMST;

            public uint EventLength;

            public Vector3 Position,
                           Rotation;
        }

        public const string Extension = ".epb";

        public List<Event> Entries = new List<Event>();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Extension.ToUpper())
                throw new InvalidSignatureException(Extension.ToUpper(), signature);

            uint UnknownUInt32_1 = reader.ReadUInt32();  // TODO: Figure out what this controls, as nulling it out broke all in game scenes.
            uint EventCount = reader.ReadUInt32();       // Number of Events in the Playbook.
            uint EventTableOffset = reader.ReadUInt32(); // Offset to this table of Events in the Playbook.

            // Jump to the Event Table's Offset for safety's sake, should always be ox30 really.
            reader.JumpTo(EventTableOffset, true);

            // Loop through based on EventCount.
            for (int i = 0; i < EventCount; i++)
            {
                Event @event = new Event();
                uint eventNameOffset = reader.ReadUInt32();     // Offset to this event's name.
                uint eventFolderOffset = reader.ReadUInt32();   // Offset to this event's folder.
                @event.EventLength = reader.ReadUInt32();       // The length of this event in frames.
                @event.Position = reader.ReadVector3();         // The position this event's objects are placed relative to.
                @event.Rotation = reader.ReadVector3();         // The rotation of this event's objects.
                uint terrainOffset = reader.ReadUInt32();       // Offset to this event's terrain.
                uint sceneLuaOffset = reader.ReadUInt32();      // Offset to this event's scene lua.
                uint sceneBankOffset = reader.ReadUInt32();     // Offset to this event's scene bank.
                uint particleListOffset = reader.ReadUInt32();  // Offset to this event's particle list.
                uint subtitleMSTOffset = reader.ReadUInt32();   // Offset to this event's MST file.

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values, needs to check if the offset isn't 0 as it reads the .EPB signature otherwise.
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

            writer.WriteSignature(Extension.ToUpper());
            writer.Write(537265920u); // TODO: Unhardcode this once we figure out what it does.
            writer.Write(Entries.Count);
            writer.AddOffset("EventTableOffset");

            // Write the events.
            writer.FillInOffset("EventTableOffset", true);
            for (int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"Event{i}Name", Entries[i].Name);
                writer.AddString($"Event{i}Folder", Entries[i].Folder);
                writer.Write(Entries[i].EventLength);
                writer.Write(Entries[i].Position);
                writer.Write(Entries[i].Rotation);
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

            // Event elements.
            foreach (Event @event in Entries)
            {
                XElement eventElem = new XElement("Event");
                XElement NameElem = new XElement("Name", @event.Name);
                XElement FolderElem = new XElement("Folder", @event.Folder);
                XElement EventLengthElem = new XElement("EventLength", @event.EventLength);
                
                //Position
                XElement Position = new XElement("Position");
                XElement PositionX = new XElement("X", @event.Position.X);
                XElement PositionY = new XElement("Y", @event.Position.Y);
                XElement PositionZ = new XElement("Z", @event.Position.Z);
                Position.Add(PositionX, PositionY, PositionZ);

                // Rotation
                XElement Rotation = new XElement("Rotation");
                XElement RotationX = new XElement("X", @event.Rotation.X);
                XElement RotationY = new XElement("Y", @event.Rotation.Y);
                XElement RotationZ = new XElement("Z", @event.Rotation.Z);
                Rotation.Add(RotationX, RotationY, RotationZ);

                XElement TerrainElem = new XElement("Terrain", @event.Terrain);
                XElement SceneLuaElem = new XElement("SceneLua", @event.SceneLua);
                XElement SceneBankElem = new XElement("SceneBank", @event.SceneBank);
                XElement ParticleListElem = new XElement("ParticleList", @event.ParticleList);
                XElement SubtitleMSTElem = new XElement("SubtitleMST", @event.SubtitleMST);

                eventElem.Add(NameElem, FolderElem, EventLengthElem, Position, Rotation, TerrainElem, SceneLuaElem, SceneBankElem, ParticleListElem, SubtitleMSTElem);

                rootElem.Add(eventElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML.
            XDocument xml = XDocument.Load(filepath);

            // Loop through event nodes.
            foreach (XElement eventElem in xml.Root.Elements("Event"))
            {
                // Read event values.
                Event @event = new Event
                {
                    Name = eventElem.Element("Name").Value,
                    Folder = eventElem.Element("Folder").Value,
                    EventLength = uint.Parse(eventElem.Element("EventLength").Value),
                    Terrain = eventElem.Element("Terrain").Value,
                    SceneLua = eventElem.Element("SceneLua").Value,
                    SceneBank = eventElem.Element("SceneBank").Value,
                    ParticleList = eventElem.Element("ParticleList").Value,
                    SubtitleMST = eventElem.Element("SubtitleMST").Value
                };

                // Position
                @event.Position.X = float.Parse(eventElem.Element("Position").Element("X").Value);
                @event.Position.Y = float.Parse(eventElem.Element("Position").Element("Y").Value);
                @event.Position.Z = float.Parse(eventElem.Element("Position").Element("Z").Value);

                // Rotation
                @event.Rotation.X = float.Parse(eventElem.Element("Rotation").Element("X").Value);
                @event.Rotation.Y = float.Parse(eventElem.Element("Rotation").Element("Y").Value);
                @event.Rotation.Z = float.Parse(eventElem.Element("Rotation").Element("Z").Value);

                // Add event to Entries list.
                Entries.Add(@event);
            }
        }
    }
}
