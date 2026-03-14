using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class TrunkChunk : IChunk
    {
        public const string ID = "ABDT"; // "Acroarts Binary Data Trunk"

        public long Offset { get; set; }

        public uint Flags { get; set; }

        public int TrunkID { get; set; } = -1;

        public float ClipRange { get; set; } = -1.0f;

        public float ClipZRange { get; set; } = -1.0f;

        public float StartTime { get; set; }

        public float EndTime { get; set; } = -1.0f;

        public List<Branch> Branches { get; set; } = [];

        public TrunkChunk() { }

        public TrunkChunk(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Offset = in_reader.Position;

            var chunkHeader = in_reader.ReadObject<ChunkHeader>();

            if (!chunkHeader.ID.Equals(ID))
                throw new InvalidSignatureException(ID, chunkHeader.ID);

            in_reader.JumpTo(Offset + chunkHeader.HeaderSize);

            var version = in_reader.Read<uint>();

            if (version != AckResource.Version)
                throw new InvalidSignatureException(AckResource.Version, version);

            Flags = in_reader.Read<uint>();
            TrunkID = in_reader.Read<int>();
            ClipRange = in_reader.Read<float>();
            ClipZRange = in_reader.Read<float>();
            StartTime = in_reader.Read<float>();
            EndTime = in_reader.Read<float>();

            var simpleNoticeCount = in_reader.Read<uint>();
            var simpleNoticeTableOffset = in_reader.Read<uint>();

            // TODO: handle simple notice table.

            var branchCount = in_reader.Read<uint>();
            var branchTableOffset = in_reader.Read<uint>();

            in_reader.JumpTo(in_parentChunk.Offset + branchTableOffset);
            
            for (uint i = 0; i < branchCount; i++)
            {
                var branchOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_parentChunk.Offset + branchOffset, () =>
                {
                    Branches.Add(new Branch(in_reader, in_parentChunk));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Offset = in_writer.Position;

            var chunkHeader = new ChunkHeader(in_writer, ID);

            in_writer.Write(AckResource.Version);
            in_writer.Write(Flags);
            in_writer.Write(TrunkID);
            in_writer.Write(ClipRange);
            in_writer.Write(ClipZRange);
            in_writer.Write(StartTime);
            in_writer.Write(EndTime);

            // TODO: simple notice tables.
            in_writer.WriteZero<long>();

            if (Branches.Count <= 0)
            {
                in_writer.WriteZero<long>();
            }
            else
            {
                in_writer.Write(Branches.Count);
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint)); // Branch table offset.

                var branchOffsets = new List<long>();

                for (int i = 0; i < Branches.Count; i++)
                    branchOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Branches.Count; i++)
                {
                    in_writer.WriteReserved(branchOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                    Branches[i].Write(in_writer, in_parentChunk);
                }
            }

            in_writer.Align(16);

            chunkHeader.FinishWrite(in_writer);
        }
    }
}
