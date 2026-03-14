using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public class Branch : INode
    {
        public uint Flags { get; set; }

        public int ID { get; set; }

        public float ClipRange { get; set; }

        public float ClipZRange { get; set; }

        public float StartTime { get; set; }

        public float EndTime { get; set; }

        public uint CoordTarget { get; set; }

        public uint CoordBranchIndex { get; set; }

        public int CoordNode { get; set; }

        public uint CoordType { get; set; }

        public int MessageParam0 { get; set; } = -1;

        public int MessageParam1 { get; set; } = -1;

        public int SortGroup { get; set; }

        public int LoopCount { get; set; }

        public List<Leaf> Leaves { get; set; } = [];

        public Branch() { }

        public Branch(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Flags = in_reader.Read<uint>();
            ID = in_reader.Read<int>();
            ClipRange = in_reader.Read<float>();
            ClipZRange = in_reader.Read<float>();
            StartTime = in_reader.Read<float>();
            EndTime = in_reader.Read<float>();
            CoordTarget = in_reader.Read<uint>();
            CoordBranchIndex = in_reader.Read<uint>();
            CoordNode = in_reader.Read<int>();
            var coordNodeNameOffset = in_reader.Read<uint>(); // TODO: handle this.
            CoordType = in_reader.Read<uint>();
            MessageParam0 = in_reader.Read<int>();
            MessageParam1 = in_reader.Read<int>();
            var chainCount = in_reader.Read<uint>();
            var chainTableOffset = in_reader.Read<uint>();    // TODO: handle these.
            SortGroup = in_reader.Read<int>();
            LoopCount = in_reader.Read<int>();

            var leafCount = in_reader.Read<uint>();
            var leafTableOffset = in_reader.Read<uint>();

            in_reader.Seek(in_parentChunk.Offset + leafTableOffset, SeekOrigin.Begin);

            for (uint i = 0; i < leafCount; i++)
            {
                var leafOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_parentChunk.Offset + leafOffset, () =>
                {
                    Leaves.Add(new Leaf(in_reader, in_parentChunk));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            in_writer.Write(Flags);
            in_writer.Write(ID);
            in_writer.Write(ClipRange);
            in_writer.Write(ClipZRange);
            in_writer.Write(StartTime);
            in_writer.Write(EndTime);
            in_writer.Write(CoordTarget);
            in_writer.Write(CoordBranchIndex);
            in_writer.Write(CoordNode);
            in_writer.WriteZero<int>(); // TODO: coordNodeNameOffset
            in_writer.Write(CoordType);
            in_writer.Write(MessageParam0);
            in_writer.Write(MessageParam1);
            in_writer.WriteZero<long>(); // TODO: chainCount/chainTableOffset
            in_writer.Write(SortGroup);
            in_writer.Write(LoopCount);

            if (Leaves.Count <= 0)
            {
                in_writer.WriteZero<long>();
            }
            else
            {
                in_writer.Write(Leaves.Count);
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint)); // Leaf table offset.

                var leafOffsets = new List<long>();

                for (int i = 0; i < Leaves.Count; i++)
                    leafOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Leaves.Count; i++)
                {
                    in_writer.WriteReserved(leafOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                    Leaves[i].Write(in_writer, in_parentChunk);
                }
            }
        }
    }
}
