using Marathon.IO;
using Marathon.IO.Types;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorGoal : IMomentumParamSet
    {
        public ColorInfoSet[] Steps { get; set; } = new ColorInfoSet[4];

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorGoal() { }

        public MaterialColorGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            for (int i = 0; i < Steps.Length; i++)
            {
                var offset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(offset), () =>
                {
                    Steps[i] = new ColorInfoSet(in_reader);
                });
            }

            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var offsets = new long[Steps.Length];

            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(ColorBlendMode);

            for (int i = 0; i < Steps.Length; i++)
            {
                in_writer.WriteReserved(offsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                Steps[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 7;
        }

        public class ColorInfoSet : List<ColorInfo>, IMomentumParamSet
        {
            private List<long> _arrayPtrOffsets = [];

            public ColorInfoSet() { }

            public ColorInfoSet(BinaryObjectReaderEx in_reader)
            {
                Read(in_reader);
            }

            public void Read(BinaryObjectReaderEx in_reader)
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        var paramCount = in_reader.Read<uint>();
                        var paramOffset = in_reader.Read<uint>();

                        in_reader.ReadAtOffset(in_reader.CalculateOffset(paramOffset), () =>
                        {
                            Add(new ColorInfo(in_reader));
                        });
                    });
                }
            }

            public void Write(BinaryObjectWriterEx in_writer)
            {
                in_writer.Write(Count);
                in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

                for (int i = 0; i < Count; i++)
                    _arrayPtrOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Count; i++)
                {
                    in_writer.WriteReserved(_arrayPtrOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    in_writer.Write(this[i].GetParamCount());
                    in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));
                    this[i].Write(in_writer);
                }
            }

            public uint GetParamCount()
            {
                return (uint)Count;
            }
        }

        public class ColorInfo : IMomentumParamSet
        {
            public Color<float, RGBA> Color { get; set; }

            public uint UnknownField1 { get; set; }

            public uint UnknownField2 { get; set; }

            public int UnknownField3 { get; set; }

            public ColorInfo() { }

            public ColorInfo(BinaryObjectReaderEx in_reader)
            {
                Read(in_reader);
            }

            public void Read(BinaryObjectReaderEx in_reader)
            {
                Color = in_reader.ReadObject<Color<float, RGBA>>();
                UnknownField1 = in_reader.Read<uint>();
                UnknownField2 = in_reader.Read<uint>();
                UnknownField3 = in_reader.Read<int>();
            }

            public void Write(BinaryObjectWriterEx in_writer)
            {
                in_writer.WriteObject(Color);
                in_writer.Write(UnknownField1);
                in_writer.Write(UnknownField2);
                in_writer.Write(UnknownField3);
            }

            public uint GetParamCount()
            {
                return 7;
            }
        }
    }
}
