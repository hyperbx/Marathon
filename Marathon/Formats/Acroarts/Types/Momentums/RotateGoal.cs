using Marathon.IO;
using System.Collections.Generic;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateGoal : IMomentumParamSet
    {
        public RotateInfoSet Set { get; set; }

        public GTCounter GTCounter { get; set; }

        public RotateGoal() { }

        public RotateGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Set = new RotateInfoSet(in_reader);
            });

            GTCounter = in_reader.Read<GTCounter>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var infoSetOffset = in_writer.Reserve<uint>();
            in_writer.Write(GTCounter);
            in_writer.WriteReserved(infoSetOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            Set.Write(in_writer);
        }

        public uint GetParamCount()
        {
            return 2;
        }

        public class RotateInfoSet : List<RotateInfo>, IMomentumParamSet
        {
            private List<long> _arrayPtrOffsets = [];

            public RotateInfoSet() { }

            public RotateInfoSet(BinaryObjectReaderEx in_reader)
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
                            Add(new RotateInfo(in_reader));
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

        public class RotateInfo : IMomentumParamSet
        {
            public Vector3 Rotation { get; set; }

            public GoalInterpolation GoalInterpolation { get; set; }

            public float TotalTime { get; set; }

            public float Coefficient { get; set; }

            public bool Accel { get; set; }

            public uint UnknownField1 { get; set; }

            public int UnknownField2 { get; set; }

            public RotateInfo() { }

            public RotateInfo(BinaryObjectReaderEx in_reader)
            {
                Read(in_reader);
            }

            public void Read(BinaryObjectReaderEx in_reader)
            {
                Rotation = in_reader.Read<Vector3>();
                GoalInterpolation = in_reader.Read<GoalInterpolation>();
                TotalTime = in_reader.Read<float>();
                Coefficient = in_reader.Read<float>();
                Accel = in_reader.Read<uint>() != 0;
                UnknownField1 = in_reader.Read<uint>();
                UnknownField2 = in_reader.Read<int>();
            }

            public void Write(BinaryObjectWriterEx in_writer)
            {
                in_writer.Write(Rotation);
                in_writer.Write(GoalInterpolation);
                in_writer.Write(TotalTime);
                in_writer.Write(Coefficient);
                in_writer.Write(Accel ? 1 : 0);
                in_writer.Write(UnknownField1);
                in_writer.Write(UnknownField2);
            }

            public uint GetParamCount()
            {
                return 9;
            }
        }
    }
}
