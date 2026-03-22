using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MotionSet : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public float UnknownField5 { get; set; }

        public List<AnonymousMomentumParamSet> Params { get; set; } = [];

        public uint UnknownField6 { get; set; }

        public uint UnknownField7 { get; set; }

        public uint UnknownField8 { get; set; }

        public MotionSet() { }

        public MotionSet(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<float>();

            var paramsOffset = in_reader.Read<uint>();

            UnknownField6 = in_reader.Read<uint>();
            UnknownField7 = in_reader.Read<uint>();
            UnknownField8 = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(paramsOffset), () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        Params.Add(new AnonymousMomentumParamSet(in_reader));
                    });
                }
            });
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);

            var paramsOffset = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField6);
            in_writer.Write(UnknownField7);
            in_writer.Write(UnknownField8);

            in_writer.WriteReserved(paramsOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);

            in_writer.Write(Params.Count);
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

            var paramsOffsets = new List<long>();

            for (int i = 0; i < Params.Count; i++)
                paramsOffsets.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < Params.Count; i++)
            {
                in_writer.WriteReserved(paramsOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                Params[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
