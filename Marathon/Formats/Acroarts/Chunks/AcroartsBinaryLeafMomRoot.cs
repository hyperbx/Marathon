using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class AcroartsBinaryLeafMomRoot : INode
    {
        public uint Index { get; set; }
        public List<byte> Data { get; set; }
        public List<byte> Data2 { get; set; }

        public List<AcroartsBinaryLeafMomNode> MomNodes { get; set; } = new();

        public AcroartsBinaryLeafMomRoot() { }

        public AcroartsBinaryLeafMomRoot(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive

            Data = in_reader.ReadArray<byte>(0x4).ToList();
            Index = in_reader.Read<uint>();
            Data2 = in_reader.ReadArray<byte>(0x10).ToList();

            var momNodeCount = in_reader.Read<uint>();
            var momNodeOffsets = in_reader.ReadArrayAtOffset<uint>(in_reader.Read<uint>() + DefaultOffset, (int)momNodeCount);
            for (int i = 0; i < momNodeCount; i++)
            {
                var momNodeOffset = momNodeOffsets[i] + DefaultOffset;
                in_reader.JumpTo(momNodeOffset);
                MomNodes.Add(new AcroartsBinaryLeafMomNode(in_reader));
            }

        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
