using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class AcroartsBinaryArchive : IChunk
    {
        public const string ID = "ABDA"; // "Acroarts Binary Archive" (speculatory)

        public List<AcroartsBinaryData> Data { get; set; } = new();

        public AcroartsBinaryArchive() { }

        public AcroartsBinaryArchive(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var DefaultPosition = 0x50;

            var header = in_reader.ReadObject<ChunkHeader>();
            /*
            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);
            */

            var version = in_reader.Read<uint>();

            if (version != AckResource.Version)
                throw new InvalidSignatureException(AckResource.Version, version);

            var abdtChunkCount = in_reader.Read<uint>();
            var abdaChunkLength = in_reader.Read<uint>();
            var unkField1 = in_reader.Read<uint>();

            List<uint> abdtChunkOffsets = new();

            for (int i = 0; i < abdtChunkCount; i++)
            {
                //64 bit pointer, 32 bit part only
                var abdtChunkOffset = in_reader.Read<uint>();
                in_reader.Skip(4);
                abdtChunkOffsets.Add(abdtChunkOffset);
            }

            for (int i = 0; i < abdtChunkCount; i++)
            {
                var offsetBinaryData = abdtChunkOffsets[i] + DefaultPosition;
                in_reader.JumpTo(offsetBinaryData);
                Data.Add(new AcroartsBinaryData(in_reader));
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
