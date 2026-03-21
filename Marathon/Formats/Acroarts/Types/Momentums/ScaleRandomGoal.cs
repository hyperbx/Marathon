using Marathon.IO;
using System.Collections.Generic;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomGoal : IMomentumParamSet
    {
        public ScaleInfoSet Steps { get; set; }

        public uint UnknownField1 { get; set; }

        public GTCounter GTCounter { get; set; }

        public uint UnknownField3 { get; set; }

        public ScaleRandomGoal() { }

        public ScaleRandomGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Steps = new ScaleInfoSet(in_reader);
            });

            UnknownField1 = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            UnknownField3 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var infoSetOffset = in_writer.Reserve<uint>();
            in_writer.Write(UnknownField1);
            in_writer.Write(GTCounter);
            in_writer.Write(UnknownField3);
            in_writer.WriteReserved(infoSetOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            Steps.Write(in_writer);
        }

        public uint GetParamCount()
        {
            return 4;
        }

        public class ScaleInfoSet : List<ScaleInfo>, IMomentumParamSet
        {
            private List<long> _arrayPtrOffsets = [];

            public ScaleInfoSet() { }

            public ScaleInfoSet(BinaryObjectReaderEx in_reader)
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
                            Add(new ScaleInfo(in_reader));
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

        public class ScaleInfo : IMomentumParamSet
        {
            public Vector3 UnknownField1 { get; set; }

            public Vector3 UnknownField2 { get; set; }

            public GoalInterpolation GoalInterpolation { get; set; }

            public uint UnknownField4 { get; set; }

            public uint UnknownField5 { get; set; }

            public float UnknownField6 { get; set; }

            public float UnknownField7 { get; set; }

            public bool Accel { get; set; }

            public uint UnknownField9 { get; set; }

            public int UnknownField10 { get; set; }

            public ScaleInfo() { }

            public ScaleInfo(BinaryObjectReaderEx in_reader)
            {
                Read(in_reader);
            }

            public void Read(BinaryObjectReaderEx in_reader)
            {
                UnknownField1 = in_reader.Read<Vector3>();
                UnknownField2 = in_reader.Read<Vector3>();
                GoalInterpolation = in_reader.Read<GoalInterpolation>();
                UnknownField4 = in_reader.Read<uint>();
                UnknownField5 = in_reader.Read<uint>();
                UnknownField6 = in_reader.Read<float>();
                UnknownField7 = in_reader.Read<float>();
                Accel = in_reader.Read<uint>() != 0;
                UnknownField9 = in_reader.Read<uint>();
                UnknownField10 = in_reader.Read<int>();
            }

            public void Write(BinaryObjectWriterEx in_writer)
            {
                in_writer.Write(UnknownField1);
                in_writer.Write(UnknownField2);
                in_writer.Write(GoalInterpolation);
                in_writer.Write(UnknownField4);
                in_writer.Write(UnknownField5);
                in_writer.Write(UnknownField6);
                in_writer.Write(UnknownField7);
                in_writer.Write(Accel ? 1 : 0);
                in_writer.Write(UnknownField9);
                in_writer.Write(UnknownField10);
            }

            public uint GetParamCount()
            {
                return 14;
            }
        }
    }
}
