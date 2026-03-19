using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SendParamGoal : IMomentumParamSet
    {
        public List<AnonymousMomentumParamSet> ParamsA { get; set; } = [];

        public List<AnonymousMomentumParamSet> ParamsB { get; set; } = [];

        public GTCounter GTCounter { get; set; }

        public SendParamGoal() { }

        public SendParamGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var unkOffset1 = in_reader.Read<uint>();
            var unkOffset2 = in_reader.Read<uint>();

            GTCounter = in_reader.Read<GTCounter>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(unkOffset1), () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        ParamsA.Add(new AnonymousMomentumParamSet(in_reader));
                    });
                }
            });

            in_reader.ReadAtOffset(in_reader.CalculateOffset(unkOffset2), () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        ParamsB.Add(new AnonymousMomentumParamSet(in_reader));
                    });
                }
            });
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var unkOffset1 = in_writer.Reserve<uint>();
            var unkOffset2 = in_writer.Reserve<uint>();

            in_writer.Write(GTCounter);

            var paramsAOffsets = new List<long>();
            var paramsBOffsets = new List<long>();

            in_writer.WriteReserved(unkOffset1, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            in_writer.Write(ParamsA.Count);
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

            for (int i = 0; i < ParamsA.Count; i++)
                paramsAOffsets.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < ParamsA.Count; i++)
            {
                in_writer.WriteReserved(paramsAOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                ParamsA[i].Write(in_writer);
            }

            in_writer.WriteReserved(unkOffset2, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            in_writer.Write(ParamsB.Count);
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

            for (int i = 0; i < ParamsB.Count; i++)
                paramsBOffsets.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < ParamsB.Count; i++)
            {
                in_writer.WriteReserved(paramsBOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                ParamsB[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
