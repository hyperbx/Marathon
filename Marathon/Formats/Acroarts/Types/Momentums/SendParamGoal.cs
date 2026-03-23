using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SendParamGoal : IMomentumParamSet
    {
        public List<AnonymousMomentumParamSet> StepsA { get; set; } = [];

        public List<AnonymousMomentumParamSet> StepsB { get; set; } = [];

        public GTCounter GTCounter { get; set; }

        public SendParamGoal() { }

        public SendParamGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var stepsOffsetA = in_reader.Read<uint>();
            var stepsOffsetB = in_reader.Read<uint>();

            GTCounter = in_reader.Read<GTCounter>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(stepsOffsetA), () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        StepsA.Add(new AnonymousMomentumParamSet(in_reader));
                    });
                }
            });

            in_reader.ReadAtOffset(in_reader.CalculateOffset(stepsOffsetB), () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_reader.CalculateOffset(offset.UInt32), () =>
                    {
                        StepsB.Add(new AnonymousMomentumParamSet(in_reader));
                    });
                }
            });
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var stepsOffsetA = in_writer.Reserve<uint>();
            var stepsOffsetB = in_writer.Reserve<uint>();

            in_writer.Write(GTCounter);

            var stepsOffsetsA = new List<long>();
            var stepsOffsetsB = new List<long>();

            in_writer.WriteReserved(stepsOffsetA, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            in_writer.Write(StepsA.Count);
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

            for (int i = 0; i < StepsA.Count; i++)
                stepsOffsetsA.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < StepsA.Count; i++)
            {
                in_writer.WriteReserved(stepsOffsetsA[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                StepsA[i].Write(in_writer);
            }

            in_writer.WriteReserved(stepsOffsetB, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            in_writer.Write(StepsB.Count);
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));

            for (int i = 0; i < StepsB.Count; i++)
                stepsOffsetsB.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < StepsB.Count; i++)
            {
                in_writer.WriteReserved(stepsOffsetsB[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                StepsB[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
