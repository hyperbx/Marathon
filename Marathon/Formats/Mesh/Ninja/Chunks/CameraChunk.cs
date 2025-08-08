using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class CameraChunk : IChunk
    {
        public const string ID = "NXCA";

        public CameraType Type { get; set; }

        public object Camera { get; set; }

        public CameraChunk() { }

        public CameraChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            Type = in_reader.Read<CameraType>();

            var cameraOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + cameraOffset);

            Camera = CameraFactory.ReadCameraByType(in_reader, Type);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var cameraPos = (uint)(in_writer.Position - InfoChunk.Size);

            CameraFactory.WriteCameraByType(in_writer, Type, Camera);

            var infoPos = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Type);
            var cameraOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(cameraOffset, cameraPos, false);
            in_writer.Align(16);

            header.FinishWrite(in_writer, infoPos);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
