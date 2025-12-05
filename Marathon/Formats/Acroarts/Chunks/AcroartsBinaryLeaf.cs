using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public enum SpkSpangleBase : uint
    {
        SpangleObject = 0,
        SpangleCellSprite = 4,
        SpangleCamera = 5,
        SpangleScreen = 6,
        SpangleLight = 7,
        SpanglePrimitive = 8,
        SpangleParticle = 9,
    }

    public struct AcroartsBinaryLeafData : INode
    {
        public List<uint> ResourceIndices1;
        public List<uint> ResourceIndices2;
        public List<uint> ResourceIndices3;
        public List<uint> ResourceIndices4;

        public AcroartsBinaryLeafData(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var Offset = in_reader.Position;
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive

            var ResourceCount = in_reader.Read<uint>();
            var ResourceIndicesOffset = in_reader.Read<uint>();
            var ResourceCount2 = in_reader.Read<uint>();
            var ResourceIndicesOffset2 = in_reader.Read<uint>();
            var ResourceCount3 = in_reader.Read<uint>();
            var ResourceIndicesOffset3 = in_reader.Read<uint>();
            var ResourceCount4 = in_reader.Read<uint>();
            var ResourceIndicesOffset4 = in_reader.Read<uint>();

            ResourceIndices1 = in_reader.ReadArrayAtOffset<uint>(ResourceIndicesOffset + DefaultOffset, (int)ResourceCount).Select(offset =>
            {
                uint index = 0xFFFFFFFF;
                in_reader.ReadAtOffset(offset + DefaultOffset, () =>
                {
                    index = in_reader.Read<uint>();
                });
                return index;
            }).ToList();

            ResourceIndices2 = in_reader.ReadArrayAtOffset<uint>(ResourceIndicesOffset2 + DefaultOffset, (int)ResourceCount2).Select(offset =>
            {
                uint index = 0xFFFFFFFF;
                in_reader.ReadAtOffset(offset + DefaultOffset, () =>
                {
                    index = in_reader.Read<uint>();
                });
                return index;
            }).ToList();

            ResourceIndices3 = in_reader.ReadArrayAtOffset<uint>(ResourceIndicesOffset3 + DefaultOffset, (int)ResourceCount3).Select(offset =>
            {
                uint index = 0xFFFFFFFF;
                in_reader.ReadAtOffset(offset + DefaultOffset, () =>
                {
                    index = in_reader.Read<uint>();
                });
                return index;
            }).ToList();

            ResourceIndices4 = in_reader.ReadArrayAtOffset<uint>(ResourceIndicesOffset4 + DefaultOffset, (int)ResourceCount4).Select(offset =>
            {
                uint index = 0xFFFFFFFF;
                in_reader.ReadAtOffset(offset + DefaultOffset, () =>
                {
                    index = in_reader.Read<uint>();
                });
                return index;
            }).ToList();
        }

        public void Write(BINAWriter in_writer)
        {
            throw new NotImplementedException();
        }
    }

    public class AcroartsBinaryLeaf : INode
    {
        public List<byte> Data { get; set; }
        public List<byte> Data2 { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SpkSpangleBase Type { get; set; }

        [JsonIgnore]
        public ISpangle Spangle { get; set; }

        public List<AcroartsBinaryLeafMomRoot> MomRoots { get; set; } = new();

        public AcroartsBinaryLeaf() { }

        public AcroartsBinaryLeaf(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var Offset = in_reader.Position;
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive

            var offset = in_reader.Length;

            Data = in_reader.ReadArray<byte>(0x84).ToList();
            Type = in_reader.Read<SpkSpangleBase>();
            var leafData = new AcroartsBinaryLeafData(in_reader);

            switch (Type) 
            {
                case SpkSpangleBase.SpangleObject:
                    Spangle = new SpangleObject(in_reader,leafData);
                    break;
                case SpkSpangleBase.SpangleCellSprite:
                    Spangle = new SpangleCellSprite(in_reader, leafData);
                    break;
                case SpkSpangleBase.SpangleCamera:
                    Spangle = new SpangleCamera(in_reader, leafData);
                    break;
                case SpkSpangleBase.SpangleScreen:
                    Spangle = new SpangleScreen(in_reader, leafData);
                    break;
                case SpkSpangleBase.SpangleLight:
                    Spangle = new SpangleLight(in_reader, leafData);
                    break;
                case SpkSpangleBase.SpanglePrimitive:
                    Spangle = new SpanglePrimitive(in_reader, leafData);
                    break;
                case SpkSpangleBase.SpangleParticle:
                    Spangle = new SpangleParticle(in_reader, leafData);
                    break;
                default:
                    Logger.Warning($"UNKNOWN SpkSpangleType {Type} at {Offset:x}");
                    break;
            }

            Data2 = in_reader.ReadArray<byte>(0x10).ToList();
            var momCount = in_reader.Read<uint>();
            var momOffsets = in_reader.ReadArrayAtOffset<uint>(in_reader.Read<uint>() + DefaultOffset, (int)momCount);
            for (int i = 0; i < momCount; i++)
            {
                var momOffset = momOffsets[i] + DefaultOffset;
                in_reader.JumpTo(momOffset);
                MomRoots.Add(new AcroartsBinaryLeafMomRoot(in_reader));
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
