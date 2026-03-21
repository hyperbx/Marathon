using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorRandomGoal : IMomentumParamSet
    {
        public ColorInfoSet[] Steps { get; set; } = new ColorInfoSet[4];

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public MaterialColorRandomGoal() { }

        public MaterialColorRandomGoal(BinaryObjectReaderEx in_reader)
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
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var offsets = new long[Steps.Length];

            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);

            for (int i = 0; i < Steps.Length; i++)
            {
                in_writer.WriteReserved(offsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                Steps[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 8;
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
            public float UnknownField1 { get; set; }

            public float UnknownField2 { get; set; }

            public float UnknownField3 { get; set; }

            public float UnknownField4 { get; set; }

            public float UnknownField5 { get; set; }

            public float UnknownField6 { get; set; }

            public float UnknownField7 { get; set; }

            public float UnknownField8 { get; set; }

            public uint UnknownField9 { get; set; }

            public int UnknownField10 { get; set; }

            public ColorInfo() { }

            public ColorInfo(BinaryObjectReaderEx in_reader)
            {
                Read(in_reader);
            }

            public void Read(BinaryObjectReaderEx in_reader)
            {
                UnknownField1 = in_reader.Read<float>();
                UnknownField2 = in_reader.Read<float>();
                UnknownField3 = in_reader.Read<float>();
                UnknownField4 = in_reader.Read<float>();
                UnknownField5 = in_reader.Read<float>();
                UnknownField6 = in_reader.Read<float>();
                UnknownField7 = in_reader.Read<float>();
                UnknownField8 = in_reader.Read<float>();
                UnknownField9 = in_reader.Read<uint>();
                UnknownField10 = in_reader.Read<int>();
            }

            public void Write(BinaryObjectWriterEx in_writer)
            {
                in_writer.Write(UnknownField1);
                in_writer.Write(UnknownField2);
                in_writer.Write(UnknownField3);
                in_writer.Write(UnknownField4);
                in_writer.Write(UnknownField5);
                in_writer.Write(UnknownField6);
                in_writer.Write(UnknownField7);
                in_writer.Write(UnknownField8);
                in_writer.Write(UnknownField9);
                in_writer.Write(UnknownField10);
            }

            public uint GetParamCount()
            {
                return 10;
            }
        }
    }
}
