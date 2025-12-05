using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class AcroartsBinaryData : IChunk
    {
        public const string ID = "ABDT"; // "Acroarts Binary Data" (speculatory)

        public AcroartsBinaryTrunk Trunk { get; set; }

        public AcroartsBinaryData() { }

        public AcroartsBinaryData(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var offset = in_reader.Position;
            var header = in_reader.ReadObject<ChunkHeader>();

            //if (!header.ID.Equals(GetChunkID()))
                //throw new InvalidSignatureException(GetChunkID(), header.ID);

            var rootOffset = (offset + header.HeaderSize);
            in_reader.JumpTo(rootOffset);
            Trunk = new AcroartsBinaryTrunk(in_reader); 
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
