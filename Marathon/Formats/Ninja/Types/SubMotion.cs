using Marathon.Formats.Ninja.Chunks;
using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Types
{
    public class SubMotion
    {
        private uint _keyframeOffset;

        public SubMotionType Type { get; set; }

        public SubMotionInterpolationType InterpolationType { get; set; }

        public int NodeIndex { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public float StartKeyframe { get; set; }

        public float EndKeyframe { get; set; }

        public List<object> Keyframes { get; set; } = [];

        public SubMotion() { }

        public SubMotion(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<SubMotionType>();
            InterpolationType = in_reader.Read<SubMotionInterpolationType>();
            NodeIndex = in_reader.Read<int>();
            StartFrame = in_reader.Read<float>();
            EndFrame = in_reader.Read<float>();
            StartKeyframe = in_reader.Read<float>();
            EndKeyframe = in_reader.Read<float>();

            var keyframeCount = in_reader.Read<uint>();
            var keyframeSize = in_reader.Read<uint>();
            var keyframeOffset = in_reader.Read<uint>();

            var pos = in_reader.Position;

            in_reader.JumpTo(InfoChunk.Size + keyframeOffset);

            for (int i = 0; i < keyframeCount; i++)
                Keyframes.Add(KeyframeFactory.ReadKeyframeByType(in_reader, Type));

            in_reader.JumpTo(pos);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(InterpolationType);
            in_writer.Write(NodeIndex);
            in_writer.Write(StartFrame);
            in_writer.Write(EndFrame);
            in_writer.Write(StartKeyframe);
            in_writer.Write(EndKeyframe);
            in_writer.Write(Keyframes.Count);
            in_writer.Write(KeyframeFactory.GetKeyframeSize(Type));
            var keyframeOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(keyframeOffset, _keyframeOffset, false);
        }

        public void WriteKeyframes(BinaryObjectWriterEx in_writer)
        {
            _keyframeOffset = (uint)(in_writer.Position - InfoChunk.Size);

            foreach (var keyframe in Keyframes)
                KeyframeFactory.WriteKeyframeByType(in_writer, Type, keyframe);
        }
    }
}
