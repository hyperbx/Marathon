using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaCamera
    {
        public uint Type { get; set; }

        public Vector3 UnknownVector3_1 { get; set; }

        public Vector3 UnknownVector3_2 { get; set; }

        public Vector3 UnknownVector3_3 { get; set; }

        public Vector3 UnknownVector3_4 { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            Type = reader.ReadUInt32();
            uint CameraOffset = reader.ReadUInt32();

            reader.JumpTo(CameraOffset, true);
            UnknownVector3_1 = reader.ReadVector3();
            UnknownVector3_2 = reader.ReadVector3();
            UnknownVector3_3 = reader.ReadVector3();
            UnknownVector3_4 = reader.ReadVector3();
        }

        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXCA");
            writer.Write("SIZE");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            uint CameraPosition = (uint)writer.BaseStream.Position;
            writer.Write(UnknownVector3_1);
            writer.Write(UnknownVector3_2);
            writer.Write(UnknownVector3_3);
            writer.Write(UnknownVector3_4);

            writer.FillOffset("dataOffset", true);
            writer.Write(Type);
            writer.AddOffset($"CameraData", 0);
            writer.Write(CameraPosition - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // Chunk Size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 0x04;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
