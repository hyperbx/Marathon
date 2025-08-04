using Amicitia.IO.Binary;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class MaterialLogic
    {
        public bool Blend { get; set; }

        public BlendMode SourceBlend { get; set; }

        public BlendMode DestinationBlend { get; set; }

        public uint BlendFactor { get; set; }

        public BlendOperation BlendOperation { get; set; }

        public LogicOperation LogicOperation { get; set; }

        public bool Alpha { get; set; }

        public CompareFunction AlphaFunction { get; set; }

        public uint AlphaRef { get; set; }

        public bool ZCompare { get; set; }

        public CompareFunction ZCompareFunction { get; set; }

        public bool ZUpdate { get; set; }

        public uint[] Reserved { get; set; } = new uint[4];

        public MaterialLogic() { }

        public MaterialLogic(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Blend = in_reader.Read<uint>() != 0;
            SourceBlend = in_reader.Read<BlendMode>();
            DestinationBlend = in_reader.Read<BlendMode>();
            BlendFactor = in_reader.Read<uint>();
            BlendOperation = in_reader.Read<BlendOperation>();
            LogicOperation = in_reader.Read<LogicOperation>();
            Alpha = in_reader.Read<uint>() != 0;
            AlphaFunction = in_reader.Read<CompareFunction>();
            AlphaRef = in_reader.Read<uint>();
            ZCompare = in_reader.Read<uint>() != 0;
            ZCompareFunction = in_reader.Read<CompareFunction>();
            ZUpdate = in_reader.Read<uint>() != 0;
            Reserved = in_reader.ReadArray<uint>(4);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Blend ? 1 : 0);
            in_writer.Write(SourceBlend);
            in_writer.Write(DestinationBlend);
            in_writer.Write(BlendFactor);
            in_writer.Write(BlendOperation);
            in_writer.Write(LogicOperation);
            in_writer.Write(Alpha ? 1 : 0);
            in_writer.Write(AlphaFunction);
            in_writer.Write(AlphaRef);
            in_writer.Write(ZCompare ? 1 : 0);
            in_writer.Write(ZCompareFunction);
            in_writer.Write(ZUpdate ? 1 : 0);
            in_writer.WriteArray(Reserved);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not MaterialLogic out_logic)
                return false;

            return out_logic.Blend == Blend &&
                   out_logic.SourceBlend == SourceBlend &&
                   out_logic.DestinationBlend == DestinationBlend &&
                   out_logic.BlendFactor == BlendFactor &&
                   out_logic.BlendOperation == BlendOperation &&
                   out_logic.LogicOperation == LogicOperation &&
                   out_logic.Alpha == Alpha &&
                   out_logic.AlphaFunction == AlphaFunction &&
                   out_logic.AlphaRef == AlphaRef &&
                   out_logic.ZCompare == ZCompare &&
                   out_logic.ZCompareFunction == ZCompareFunction &&
                   out_logic.ZUpdate == ZUpdate;
        }
    }
}
