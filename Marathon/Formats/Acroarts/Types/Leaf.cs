using Marathon.Formats.Acroarts.Types.Resources;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types
{
    public class Leaf : IBinarySerializableEx
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

        public string ModelAttachNodeName { get; set; }

        public int ModelType { get; set; }

        public uint UnknownField { get; set; }

        public float PrimitiveX0 { get; set; }

        public float PrimitiveY0 { get; set; }

        public float PrimitiveX1 { get; set; }

        public float PrimitiveY1 { get; set; }

        public ResourceType ResourceType { get; set; }

        public IResourceTable Resources { get; set; }

        public uint TrOpCtrlFlag { get; set; }

        public uint ModelCount { get; set; }

        public uint MotionCount { get; set; }

        public uint TextureCount { get; set; }

        public List<MomentumList> MomentumLists { get; set; } = [];

        public Leaf() { }

        public Leaf(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
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

            var modelAttachNodeNameOffset = in_reader.Read<uint>();

            if (modelAttachNodeNameOffset != 0)
            {
                in_reader.ReadAtOffset(in_reader.CalculateOffset(modelAttachNodeNameOffset),
                    () => ModelAttachNodeName = in_reader.ReadStringNullTerminated());
            }

            ModelType = in_reader.Read<int>();
            UnknownField = in_reader.Read<uint>();

            if (UnknownField > 0)
                Logger.Warning($"[Leaf] UnknownField is non-zero: {UnknownField}");

            PrimitiveX0 = in_reader.Read<float>();
            PrimitiveY0 = in_reader.Read<float>();
            PrimitiveX1 = in_reader.Read<float>();
            PrimitiveY1 = in_reader.Read<float>();
            ResourceType = in_reader.Read<ResourceType>();
            Resources = ResourceTableFactory.ReadResourceTableByType(in_reader, ResourceType);
            TrOpCtrlFlag = in_reader.Read<uint>();
            ModelCount = in_reader.Read<uint>();
            MotionCount = in_reader.Read<uint>();
            TextureCount = in_reader.Read<uint>();

            if (ModelCount > 0 || MotionCount > 0 || TextureCount > 0)
                Logger.Warning($"[Leaf] Model Count: {ModelCount}; Motion Count: {MotionCount}; Texture Count: {TextureCount}");

            var momentumListCount = in_reader.Read<uint>();
            var momentumListTableOffset = in_reader.Read<uint>();

            in_reader.JumpTo(in_reader.CalculateOffset(momentumListTableOffset));

            for (uint i = 0; i < momentumListCount; i++)
            {
                var momentumListOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(momentumListOffset), () =>
                {
                    MomentumLists.Add(new MomentumList(in_reader));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
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

            var modelAttachNodeNameOffset = 0L;

            if (string.IsNullOrEmpty(ModelAttachNodeName))
            {
                in_writer.WriteZero<uint>();
            }
            else
            {
                modelAttachNodeNameOffset = in_writer.Reserve<uint>();
            }

            in_writer.Write(ModelType);
            in_writer.Write(UnknownField);
            in_writer.Write(PrimitiveX0);
            in_writer.Write(PrimitiveY0);
            in_writer.Write(PrimitiveX1);
            in_writer.Write(PrimitiveY1);
            in_writer.Write(ResourceType);

            Resources.WriteInfo(in_writer);

            in_writer.Write(TrOpCtrlFlag);
            in_writer.Write(ModelCount);
            in_writer.Write(MotionCount);
            in_writer.Write(TextureCount);

            if (MomentumLists.Count <= 0)
            {
                in_writer.WriteZero<long>();

                Resources.WriteArray(in_writer);
            }
            else
            {
                in_writer.Write(MomentumLists.Count);
                var momentumListTableOffset = in_writer.Reserve<uint>();

                if (!string.IsNullOrEmpty(ModelAttachNodeName))
                {
                    in_writer.WriteReserved(modelAttachNodeNameOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    in_writer.WriteStringFixedLength(ModelAttachNodeName, ((ModelAttachNodeName.Length + 15) / 16) * 16);
                }

                in_writer.WriteReserved(momentumListTableOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);

                var momentumListOffsets = new List<long>();

                for (int i = 0; i < MomentumLists.Count; i++)
                    momentumListOffsets.Add(in_writer.Reserve<uint>());

                Resources.WriteArray(in_writer);

                for (int i = 0; i < MomentumLists.Count; i++)
                {
                    in_writer.WriteReserved(momentumListOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    MomentumLists[i].Write(in_writer);
                }
            }

            Resources.WriteData(in_writer);
        }
    }
}
