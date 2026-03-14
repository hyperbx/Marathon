using Marathon.Formats.Acroarts.Chunks;
using Marathon.Formats.Acroarts.Types.Resources;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public class Leaf : INode
    {
        public uint Flags { get; set; }

        public int ID { get; set; } = -1;

        public uint CoordType { get; set; } = 1;

        public float LifeTimeMin { get; set; }

        public float LifeTimeMax { get; set; }

        public float SleepTime { get; set; }

        public float DelayTimeMax { get; set; }

        public float GenInterval { get; set; }

        public float GenLifeTime { get; set; }

        public float GenRate { get; set; }

        public float GenCountMin { get; set; }

        public float GenCountMax { get; set; }

        public float LODDistStart { get; set; }

        public float LODDistEnd { get; set; }

        public float LODRate { get; set; }

        public float LODCountMin { get; set; }

        public float LODCountMax { get; set; }

        public int MessageParam0 { get; set; }

        public int MessageParam1 { get; set; }

        public float ClipRange { get; set; }

        public float ClipZNearRange { get; set; }

        public float ClipZFarRange { get; set; } = -1.0f;

        public int BlendMode { get; set; }

        public uint ToLeafCoordTarget { get; set; }

        public uint ToLeafCoordType { get; set; }

        public uint ToLeafCoordNode { get; set; }

        public uint ModelAttachIndex { get; set; }

        public int ModelType { get; set; }

        public float PrimitiveX0 { get; set; }

        public float PrimitiveY0 { get; set; }

        public float PrimitiveX1 { get; set; }

        public float PrimitiveY1 { get; set; }

        public ResourceType ResourceType { get; set; }

        public IResourceTable Resource { get; set; }

        public uint TrOpCtrlFlag { get; set; }

        public List<MomentumList> MomentumLists { get; set; } = [];

        public Leaf() { }

        public Leaf(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Flags = in_reader.Read<uint>();
            ID = in_reader.Read<int>();
            CoordType = in_reader.Read<uint>();
            LifeTimeMin = in_reader.Read<float>();
            LifeTimeMax = in_reader.Read<float>();
            SleepTime = in_reader.Read<float>();
            DelayTimeMax = in_reader.Read<float>();
            GenInterval = in_reader.Read<float>();
            GenLifeTime = in_reader.Read<float>();
            GenRate = in_reader.Read<float>();
            GenCountMin = in_reader.Read<float>();
            GenCountMax = in_reader.Read<float>();
            LODDistStart = in_reader.Read<float>();
            LODDistEnd = in_reader.Read<float>();
            LODRate = in_reader.Read<float>();
            LODCountMin = in_reader.Read<float>();
            LODCountMax = in_reader.Read<float>();
            MessageParam0 = in_reader.Read<int>();
            MessageParam1 = in_reader.Read<int>();
            ClipRange = in_reader.Read<float>();
            ClipZNearRange = in_reader.Read<float>();
            ClipZFarRange = in_reader.Read<float>();
            BlendMode = in_reader.Read<int>();
            ToLeafCoordTarget = in_reader.Read<uint>();
            ToLeafCoordType = in_reader.Read<uint>();
            ToLeafCoordNode = in_reader.Read<uint>();
            ModelAttachIndex = in_reader.Read<uint>();
            ModelType = in_reader.Read<int>();

            // TODO: test this.
            var unkField = in_reader.Read<uint>();

            PrimitiveX0 = in_reader.Read<float>();
            PrimitiveY0 = in_reader.Read<float>();
            PrimitiveX1 = in_reader.Read<float>();
            PrimitiveY1 = in_reader.Read<float>();
            ResourceType = in_reader.Read<ResourceType>();
            Resource = ResourceTableFactory.ReadResourceTableByType(in_reader, in_parentChunk, ResourceType);
            TrOpCtrlFlag = in_reader.Read<uint>();

            // TODO: check these.
            var modelCount = in_reader.Read<uint>();
            var motionCount = in_reader.Read<uint>();
            var textureCount = in_reader.Read<uint>();

            var momentumListCount = in_reader.Read<uint>();
            var momentumListTableOffset = in_reader.Read<uint>();

            in_reader.Seek(in_parentChunk.Offset + momentumListTableOffset, SeekOrigin.Begin);

            for (uint i = 0; i < momentumListCount; i++)
            {
                var momentumListOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_parentChunk.Offset + momentumListOffset, () =>
                {
                    MomentumLists.Add(new MomentumList(in_reader, in_parentChunk));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            in_writer.Write(Flags);
            in_writer.Write(ID);
            in_writer.Write(CoordType);
            in_writer.Write(LifeTimeMin);
            in_writer.Write(LifeTimeMax);
            in_writer.Write(SleepTime);
            in_writer.Write(DelayTimeMax);
            in_writer.Write(GenInterval);
            in_writer.Write(GenLifeTime);
            in_writer.Write(GenRate);
            in_writer.Write(GenCountMin);
            in_writer.Write(GenCountMax);
            in_writer.Write(LODDistStart);
            in_writer.Write(LODDistEnd);
            in_writer.Write(LODRate);
            in_writer.Write(LODCountMin);
            in_writer.Write(LODCountMax);
            in_writer.Write(MessageParam0);
            in_writer.Write(MessageParam1);
            in_writer.Write(ClipRange);
            in_writer.Write(ClipZNearRange);
            in_writer.Write(ClipZFarRange);
            in_writer.Write(BlendMode);
            in_writer.Write(ToLeafCoordTarget);
            in_writer.Write(ToLeafCoordType);
            in_writer.Write(ToLeafCoordNode);
            in_writer.Write(ModelAttachIndex);
            in_writer.Write(ModelType);
            in_writer.WriteZero<int>(); // TODO
            in_writer.Write(PrimitiveX0);
            in_writer.Write(PrimitiveY0);
            in_writer.Write(PrimitiveX1);
            in_writer.Write(PrimitiveY1);
            in_writer.Write(ResourceType);

            Resource.WriteInfo(in_writer, in_parentChunk);

            in_writer.Write(TrOpCtrlFlag);
            in_writer.WriteZero<int>(); // TODO: modelCount
            in_writer.WriteZero<int>(); // TODO: motionCount
            in_writer.WriteZero<int>(); // TODO: textureCount

            if (MomentumLists.Count <= 0)
            {
                in_writer.WriteZero<long>();

                Resource.WriteArray(in_writer, in_parentChunk);
            }
            else
            {
                in_writer.Write(MomentumLists.Count);
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint)); // Momentum list table offset.

                var momentumListOffsets = new List<long>();

                for (int i = 0; i < MomentumLists.Count; i++)
                    momentumListOffsets.Add(in_writer.Reserve<uint>());

                Resource.WriteArray(in_writer, in_parentChunk);

                for (int i = 0; i < MomentumLists.Count; i++)
                {
                    in_writer.WriteReserved(momentumListOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                    MomentumLists[i].Write(in_writer, in_parentChunk);
                }
            }

            Resource.WriteData(in_writer, in_parentChunk);
        }
    }
}
