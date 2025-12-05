using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class AcroartsBinaryBranch : INode
    {
        public List<byte> Data { get; set; }
        public List<AcroartsBinaryLeaf> Leafs { get; set; } = new();
        public AcroartsBinaryBranch() { }

        public AcroartsBinaryBranch(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive
            Data = in_reader.ReadArray<byte>(0x44).ToList();
            var leafCount = in_reader.Read<uint>();
            var leafOffsets = in_reader.ReadArrayAtOffset<uint>(in_reader.Read<uint>() + DefaultOffset, (int)leafCount);
            for (int i = 0; i < leafCount; i++)
            {
                var leafOffset = leafOffsets[i] + DefaultOffset;
                in_reader.JumpTo(leafOffset);
                Leafs.Add(new AcroartsBinaryLeaf(in_reader));
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
