using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaMaterialLogic
    {
        public bool Blend { get; set; }

        public NinjaNext_BlendMode SRCBlend { get; set; }

        public NinjaNext_BlendMode DSTBlend { get; set; }

        public uint BlendFactor { get; set; }

        public NinjaNext_BlendOperation BlendOperation { get; set; }

        public NinjaNext_LogicOperation LogicOperation { get; set; }

        public bool Alpha { get; set; }

        public NinjaNext_CMPFunction AlphaFunction { get; set; }

        public uint AlphaRef { get; set; }

        public bool ZComparison { get; set; }

        public NinjaNext_CMPFunction ZComparisonFunction { get; set; }

        public bool ZUpdate { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }

        public uint Reserved3 { get; set; }

        public uint Offset { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            reader.JumpAhead(0x4);
            reader.JumpTo(reader.ReadUInt32(), true);
            reader.JumpAhead(0xC);
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);
            Blend = reader.ReadBoolean(0x04);
            SRCBlend = (NinjaNext_BlendMode)reader.ReadUInt32();
            DSTBlend = (NinjaNext_BlendMode)reader.ReadUInt32();
            BlendFactor = reader.ReadUInt32();
            BlendOperation = (NinjaNext_BlendOperation)reader.ReadUInt32();
            LogicOperation = (NinjaNext_LogicOperation)reader.ReadUInt32();
            Alpha = reader.ReadBoolean(0x04);
            AlphaFunction = (NinjaNext_CMPFunction)reader.ReadUInt32();
            AlphaRef = reader.ReadUInt32();
            ZComparison = reader.ReadBoolean(0x04);
            ZComparisonFunction = (NinjaNext_CMPFunction)reader.ReadUInt32();
            ZUpdate = reader.ReadBoolean(0x04);
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
            Reserved3 = reader.ReadUInt32();
        }

        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"LogicOffset{index}", (uint)writer.BaseStream.Position);
            writer.Write(Blend);
            writer.FixPadding();
            writer.Write((uint)SRCBlend);
            writer.Write((uint)DSTBlend);
            writer.Write(BlendFactor);
            writer.Write((uint)BlendOperation);
            writer.Write((uint)LogicOperation);
            writer.Write(Alpha);
            writer.FixPadding();
            writer.Write((uint)AlphaFunction);
            writer.Write(AlphaRef);
            writer.Write(ZComparison);
            writer.FixPadding();
            writer.Write((uint)ZComparisonFunction);
            writer.Write(ZUpdate);
            writer.FixPadding();
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
            writer.Write(Reserved3);
        }

        public override bool Equals(object obj)
        {
            if (obj is NinjaMaterialLogic)
                return Equals((NinjaMaterialLogic)obj);
            return false;
        }
        public bool Equals(NinjaMaterialLogic obj)
        {
            if (obj == null) return false;
            if (!EqualityComparer<bool>.Default.Equals(Blend, obj.Blend)) return false;
            if (!EqualityComparer<NinjaNext_BlendMode>.Default.Equals(SRCBlend, obj.SRCBlend)) return false;
            if (!EqualityComparer<NinjaNext_BlendMode>.Default.Equals(DSTBlend, obj.DSTBlend)) return false;
            if (!EqualityComparer<uint>.Default.Equals(BlendFactor, obj.BlendFactor)) return false;
            if (!EqualityComparer<NinjaNext_BlendOperation>.Default.Equals(BlendOperation, obj.BlendOperation)) return false;
            if (!EqualityComparer<NinjaNext_LogicOperation>.Default.Equals(LogicOperation, obj.LogicOperation)) return false;
            if (!EqualityComparer<bool>.Default.Equals(Alpha, obj.Alpha)) return false;
            if (!EqualityComparer<NinjaNext_CMPFunction>.Default.Equals(AlphaFunction, obj.AlphaFunction)) return false;
            if (!EqualityComparer<uint>.Default.Equals(AlphaRef, obj.AlphaRef)) return false;
            if (!EqualityComparer<bool>.Default.Equals(ZComparison, obj.ZComparison)) return false;
            if (!EqualityComparer<NinjaNext_CMPFunction>.Default.Equals(ZComparisonFunction, obj.ZComparisonFunction)) return false;
            if (!EqualityComparer<bool>.Default.Equals(ZUpdate, obj.ZUpdate)) return false;
            if (!EqualityComparer<uint>.Default.Equals(Reserved0, obj.Reserved0)) return false;
            if (!EqualityComparer<uint>.Default.Equals(Reserved1, obj.Reserved1)) return false;
            if (!EqualityComparer<uint>.Default.Equals(Reserved2, obj.Reserved2)) return false;
            if (!EqualityComparer<uint>.Default.Equals(Reserved3, obj.Reserved3)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash ^= EqualityComparer<bool>.Default.GetHashCode(Blend);
            hash ^= EqualityComparer<NinjaNext_BlendMode>.Default.GetHashCode(SRCBlend);
            hash ^= EqualityComparer<NinjaNext_BlendMode>.Default.GetHashCode(DSTBlend);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(BlendFactor);
            hash ^= EqualityComparer<NinjaNext_BlendOperation>.Default.GetHashCode(BlendOperation);
            hash ^= EqualityComparer<NinjaNext_LogicOperation>.Default.GetHashCode(LogicOperation);
            hash ^= EqualityComparer<bool>.Default.GetHashCode(Alpha);
            hash ^= EqualityComparer<NinjaNext_CMPFunction>.Default.GetHashCode(AlphaFunction);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(AlphaRef);
            hash ^= EqualityComparer<bool>.Default.GetHashCode(ZComparison);
            hash ^= EqualityComparer<NinjaNext_CMPFunction>.Default.GetHashCode(ZComparisonFunction);
            hash ^= EqualityComparer<bool>.Default.GetHashCode(ZUpdate);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Reserved0);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Reserved1);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Reserved2);
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Reserved3);
            return hash;
        }
    }
}
