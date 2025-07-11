using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Event
{
    /// <summary>
    /// Support for *.tev files; used for timing audio and particle effects with Ninja animations.
    /// </summary>
    public class TimeEvent : FileBase
    {
        private const string _extension = ".tev"; // "Time EVent"
        private const string _signature = ".TEV"; // "Time EVent"

        public TimeEvent() { }

        public TimeEvent(string in_path) : base(in_path) { }

        /// <summary>
        /// The reference animation file.
        /// </summary>
        public string Motion { get; set; }

        /// <summary>
        /// The events in this file.
        /// </summary>
        public List<TimeEventData> Events { get; set; } = [];

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            reader.CheckSignature(_signature);

            // Always null.
            reader.JumpAhead(4);

            var motionPathOffset = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + motionPathOffset, () => Motion = reader.ReadStringNullTerminated());

            var eventCount = reader.Read<uint>();
            var eventTableOffset = reader.Read<uint>();

            for (int i = 0; i < eventCount; i++)
            {
                var data = new TimeEventData();

                // Always null.
                reader.JumpAhead(4);

                var targetNodeOffset = reader.Read<uint>();
                var resourceOffset = reader.Read<uint>();
                var resourceNameOffset = reader.Read<uint>();

                data.StartTime = reader.Read<float>();
                data.EndTime = reader.Read<float>();
                data.UnknownField1 = reader.Read<uint>();
                data.Position = reader.Read<Vector3>();
                data.UnknownField2 = reader.Read<Vector3>();

                reader.ReadAtOffset(BINAHeader.Size + targetNodeOffset, () => data.TargetNode = reader.ReadStringNullTerminated());
                reader.ReadAtOffset(BINAHeader.Size + resourceOffset, () => data.Resource = reader.ReadStringNullTerminated());
                reader.ReadAtOffset(BINAHeader.Size + resourceNameOffset, () => data.ResourceName = reader.ReadStringNullTerminated());

                Events.Add(data);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);
        
            writer.WriteSignature(_signature);
            writer.Write(0);
            writer.CreateStringField("MotionNameOffset", Motion);
            writer.Write(Events.Count);
            writer.CreateNamedField("EventTableOffset");
            writer.WriteNamedField("EventTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Events.Count; i++)
            {
                writer.Write(0);
                writer.CreateStringField($"TargetNode{i}", Events[i].TargetNode);
                writer.CreateStringField($"Resource{i}", Events[i].Resource);
                writer.CreateStringField($"ResourceName{i}", Events[i].ResourceName);
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
            return Motion;
        }
    }

    public class TimeEventData
    {
        /// <summary>
        /// The name of the node to target.
        /// </summary>
        public string TargetNode { get; set; }

        /// <summary>
        /// The name of the resource file to load from.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// The name of the resource to pair with this animation.
        /// </summary>
        public string ResourceName { get; set; }

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

        public override string ToString()
        {
            return ResourceName;
        }
    }
}