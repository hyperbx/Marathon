using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class MotionChunk : IChunk
    {
        public const string ID = "NXMO";

        public MotionType Type { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public List<SubMotion> SubMotions { get; set; } = [];

        public float FPS { get; set; }

        public uint[] Reserved { get; set; } = new uint[2];

        public MotionChunk() { }

        public MotionChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            Type = in_reader.Read<MotionType>();
            StartFrame = in_reader.Read<float>();
            EndFrame = in_reader.Read<float>();
            var subMotionCount = in_reader.Read<uint>();
            var subMotionOffset = in_reader.Read<uint>();
            FPS = in_reader.Read<float>();
            Reserved = in_reader.ReadArray<uint>(2);

            in_reader.JumpTo(InfoChunk.Size + subMotionOffset);

            for (int i = 0; i < subMotionCount; i++)
                SubMotions.Add(new SubMotion(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            foreach (var subMotion in SubMotions)
                subMotion.WriteKeyframes(in_writer);

            var subMotionsPos = (uint)(in_writer.Position - InfoChunk.Size);

            foreach (var subMotion in SubMotions)
                subMotion.Write(in_writer);

            var dataOffset = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Type);
            in_writer.Write(StartFrame);
            in_writer.Write(EndFrame);
            in_writer.Write(SubMotions.Count);
            var subMotionsOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(subMotionsOffset, subMotionsPos, false);
            in_writer.Write(FPS);
            in_writer.WriteArray(Reserved);
            in_writer.Align(16);

            var chunkSize = (uint)(in_writer.Position - header.GetChunkStart());

            header.FinishWrite(in_writer, chunkSize, dataOffset, 0);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }

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
