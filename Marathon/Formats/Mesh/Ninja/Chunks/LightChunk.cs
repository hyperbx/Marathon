using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class LightChunk : IChunk
    {
        public const string ID = "NXLI"; // Ninja directX LIght

        public LightType Type { get; set; }

        public object Light { get; set; }

        public LightChunk() { }

        public LightChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            Type = in_reader.Read<LightType>();

            var lightOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + lightOffset);

            Light = LightFactory.ReadLightByType(in_reader, Type);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var lightPos = (uint)(in_writer.Position - InfoChunk.Size);

            LightFactory.WriteLightByType(in_writer, Type, Light);

            var infoPos = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Type);
            var lightOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(lightOffset, lightPos, false);
            in_writer.Align(16);

            header.FinishWrite(in_writer, infoPos);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
