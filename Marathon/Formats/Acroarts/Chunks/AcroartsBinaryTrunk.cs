using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class AcroartsBinaryTrunk : INode
    {
        public uint Version { get; set; }
        public List<byte> Data { get; set; }
        public List<AcroartsBinaryBranch> Branches { get; set; } = new();
        public AcroartsBinaryTrunk() { }

        public AcroartsBinaryTrunk(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive
            Version = in_reader.Read<uint>();

            if (Version != AckResource.Version)
                throw new InvalidSignatureException(AckResource.Version, Version);

            Data = in_reader.ReadArray<byte>(0x20).ToList();

            var branchCount = in_reader.Read<uint>();
            var branchOffsets = in_reader.ReadArrayAtOffset<uint>(in_reader.Read<uint>() + DefaultOffset, (int)branchCount);
            for (int i = 0; i < branchCount; i++)
            {
                var branchOffset = branchOffsets[i] + DefaultOffset;
                in_reader.JumpTo(branchOffset);
                Branches.Add(new AcroartsBinaryBranch(in_reader));
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
