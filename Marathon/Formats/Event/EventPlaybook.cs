using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Event Playbook
// Format references:   Sonicteam::EventPlaybook
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Event
{
    /// <summary>
    /// Support for *.epb files; used for defining cutscene properties.
    /// </summary>
    public class EventPlaybook : FileBase
    {
        private const string _extension = ".epb"; // "Event PlayBook"
        private const string _signature = ".EPB"; // "Event PlayBook"
        private const uint _version = 0x20060700; // 2006 July, Revision 0

        /// <summary>
        /// The events in this playbook.
        /// </summary>
        public List<EventPlaybookData> Events { get; set; } = [];

        public EventPlaybookData this[int in_index]
        {
            get => Events[in_index];
            set => Events[in_index] = value;
        }

        public EventPlaybookData this[string in_name]
        {
            get => Events.Find(x => x.Name == in_name);
        }

        public EventPlaybook() { }

        public EventPlaybook(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            var version = reader.Read<uint>();
            var eventCount = reader.Read<uint>();
            var eventTableOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + eventTableOffset);

            for (int i = 0; i < eventCount; i++)
            {
                EventPlaybookData @event = new();

                var nameOffset = reader.Read<uint>();
                var folderOffset = reader.Read<uint>();
                
                @event.Length = reader.Read<uint>();
                @event.Position = reader.Read<Vector3>();
                @event.Rotation = reader.Read<Vector3>();
                
                var terrainOffset = reader.Read<uint>();
                var sceneParametersOffset = reader.Read<uint>();
                var sceneBankOffset = reader.Read<uint>();
                var particleContainerOffset = reader.Read<uint>();
                var subtitlesOffset = reader.Read<uint>();

                var pos = reader.Position;

                if (nameOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + nameOffset, () => @event.Name = reader.ReadStringNullTerminated());

                if (folderOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + folderOffset, () => @event.Folder = reader.ReadStringNullTerminated());

                if (terrainOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + terrainOffset, () => @event.Terrain = reader.ReadStringNullTerminated());

                if (sceneParametersOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + sceneParametersOffset, () => @event.SceneParameters = reader.ReadStringNullTerminated());

                if (sceneBankOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + sceneBankOffset, () => @event.SoundBank = reader.ReadStringNullTerminated());

                if (particleContainerOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + particleContainerOffset, () => @event.ParticleContainer = reader.ReadStringNullTerminated());

                if (subtitlesOffset != 0)
                    reader.ReadAtOffset(BINAHeader.Size + subtitlesOffset, () => @event.Subtitles = reader.ReadStringNullTerminated());

                reader.JumpTo(pos);

                Events.Add(@event);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.Write(Events.Count);
            writer.Reserve<uint>("EventTableOffset");
            writer.WriteReserved("EventTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Events.Count; i++)
            {
                writer.WriteStringOffset(Events[i].Name);
                writer.WriteStringOffset(Events[i].Folder);
                writer.Write(Events[i].Length);
                writer.Write(Events[i].Position);
                writer.Write(Events[i].Rotation);
                writer.WriteStringOffset(Events[i].Terrain);
                writer.WriteStringOffset(Events[i].SceneParameters);
                writer.WriteStringOffset(Events[i].SoundBank);
                writer.WriteStringOffset(Events[i].ParticleContainer);
                writer.WriteStringOffset(Events[i].Subtitles);
            }

            writer.FinishWrite();
        }
    }

    public class EventPlaybookData
    {
        /// <summary>
        /// The name of this event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location of this event's resources.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// The location of this event's terrain.
        /// </summary>
        public string Terrain { get; set; }

        /// <summary>
        /// The location of this event's scene parameter script.
        /// </summary>
        public string SceneParameters { get; set; }

        /// <summary>
        /// The location of this event's sound bank.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The location of this event's particle container.
        /// </summary>
        public string ParticleContainer { get; set; }

        /// <summary>
        /// The location of this event's message table for subtitles.
        /// </summary>
        public string Subtitles { get; set; }

        /// <summary>
        /// The length of this event in frames.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// The position of this event's objects relative to the origin point.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The rotation of this event's objects.
        /// </summary>
        public Vector3 Rotation { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}