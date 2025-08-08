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
                SubMotions.Add(new(in_reader));
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

            header.FinishWrite(in_writer, dataOffset, 0);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
