using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaMaterialColours
    {
        public Vector4 Diffuse { get; set; }

        public Vector4 Ambient { get; set; }

        public Vector4 Specular { get; set; }

        public Vector4 Emissive { get; set; }

        public float Power { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }

        public uint Offset { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            reader.JumpAhead(0x4);
            reader.JumpTo(reader.ReadUInt32(), true);
            reader.JumpAhead(0x8);
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);
            Diffuse = reader.ReadVector4();
            Ambient = reader.ReadVector4();
            Specular = reader.ReadVector4();
            Emissive = reader.ReadVector4();
            Power = reader.ReadSingle();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
        }

        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"ColourOffset{index}", (uint)writer.BaseStream.Position);
            writer.Write(Diffuse);
            writer.Write(Ambient);
            writer.Write(Specular);
            writer.Write(Emissive);
            writer.Write(Power);
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
        }

        public override bool Equals(object obj)
        {
            if (obj is NinjaMaterialColours)
                return Equals((NinjaMaterialColours)obj);
            return false;
        }
        public bool Equals(NinjaMaterialColours obj)
        {
            if (obj == null) return false;
            if (!EqualityComparer<uint>.Default.Equals(Offset, obj.Offset)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Offset);
            return hash;
        }
    }
}
