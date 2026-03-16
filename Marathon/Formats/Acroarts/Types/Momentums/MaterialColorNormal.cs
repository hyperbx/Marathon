using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorNormal : IMomentumParamSet
    {
        public Colour<float, ARGB> Color { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorNormal() { }

        public MaterialColorNormal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Color = in_reader.ReadObject<Colour<float, ARGB>>();
            SetGeneralColor = in_reader.Read<uint>() != 0;
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.WriteObject(Color);
            in_writer.Write(SetGeneralColor ? 1 : 0);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 6;
        }
    }
}
