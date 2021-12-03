using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaLight
    {
        public uint Type { get; set; }

        public uint UnknownUInt32_1 { get; set; }

        public Vector3 UnknownVector3_1 { get; set; }

        public Vector3 UnknownVector3_2 { get; set; }

        public Vector3 UnknownVector3_3 { get; set; }
        public float UnknownFloat_1 { get; set; }


        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            Type = reader.ReadUInt32();
            uint LightOffset = reader.ReadUInt32();

            reader.JumpTo(LightOffset, true);
            UnknownUInt32_1 = reader.ReadUInt32();
            UnknownVector3_1 = reader.ReadVector3();
            UnknownVector3_2 = reader.ReadVector3();
            UnknownVector3_3 = reader.ReadVector3();
            UnknownFloat_1 = reader.ReadSingle();
        }

        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXLI");
            writer.Write("SIZE");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            uint LightPosition = (uint)writer.BaseStream.Position;
            writer.Write(UnknownUInt32_1);
            writer.Write(UnknownVector3_1);
            writer.Write(UnknownVector3_2);
            writer.Write(UnknownVector3_3);
            writer.Write(UnknownFloat_1);

            writer.FillOffset("dataOffset", true);
            writer.Write(Type);
            writer.AddOffset($"LightData", 0);
            writer.Write(LightPosition - 0x20);

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
