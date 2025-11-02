using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Time Event
// Format references:   Sonicteam::TimeEvent
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Event
{
    /// <summary>
    /// Support for *.tev files; used for timing audio and particle effects with Ninja animations.
    /// </summary>
    public class TimeEvent : FileBase
    {
        private const string _extension = ".tev"; // "Time EVent"
        private const string _signature = ".TEV"; // "Time EVent"

        /// <summary>
        /// The reference animation file.
        /// </summary>
        public string Animation { get; set; }

        /// <summary>
        /// The events in this file.
        /// </summary>
        public List<TimeEventData> Events { get; set; } = [];

        public override string Extension => _extension;

        public TimeEventData this[int in_index]
        {
            get => Events[in_index];
            set => Events[in_index] = value;
        }

        public TimeEvent() { }

        public TimeEvent(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            // Always null.
            reader.JumpAhead(4);

            var animationPathOffset = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + animationPathOffset,
                () => Animation = reader.ReadStringNullTerminated());

            var eventCount = reader.Read<uint>();
            var eventTableOffset = reader.Read<uint>();

            for (int i = 0; i < eventCount; i++)
            {
                var data = new TimeEventData();

                // Always null.
                reader.JumpAhead(4);

                var nodeNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nodeNameOffset,
                    () => data.Node = reader.ReadStringNullTerminated());

                var particleContainerNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerNameOffset,
                    () => data.ParticleContainerName = reader.ReadStringNullTerminated());

                var particleNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameOffset,
                    () => data.ParticleName = reader.ReadStringNullTerminated());

                data.StartTime = reader.Read<float>();
                data.EndTime = reader.Read<float>();
                data.UnknownField1 = reader.Read<uint>();
                data.Position = reader.Read<Vector3>();
                data.UnknownField2 = reader.Read<Vector3>();

                Events.Add(data);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);
        
            writer.WriteSignature(_signature);
            writer.Write(0);
            writer.WriteStringOffset(Animation);
            writer.Write(Events.Count);
            writer.Reserve<uint>("EventTableOffset");
            writer.WriteReserved("EventTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Events.Count; i++)
            {
                writer.Write(0);
                writer.WriteStringOffset(Events[i].Node);
                writer.WriteStringOffset(Events[i].ParticleContainerName);
                writer.WriteStringOffset(Events[i].ParticleName);
                writer.Write(Events[i].StartTime);
                writer.Write(Events[i].EndTime);
                writer.Write(Events[i].UnknownField1);
                writer.Write(Events[i].Position);
                writer.Write(Events[i].UnknownField2);
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Animation;
        }
    }

    public class TimeEventData
    {
        /// <summary>
        /// The name of the node to target.
        /// </summary>
        public string Node { get; set; }

        /// <summary>
        /// The name of the resource file to load from.
        /// </summary>
        public string ParticleContainerName { get; set; }

        /// <summary>
        /// The name of the resource to pair with this animation.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// The time during the animation this event starts at.
        /// </summary>
        public float StartTime { get; set; }

        /// <summary>
        /// The time during the animation this event ends at.
        /// </summary>
        public float EndTime { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField1 { get; set; }

        /// <summary>
        /// The position of this event relative to the object.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public Vector3 UnknownField2 { get; set; }
    }
}