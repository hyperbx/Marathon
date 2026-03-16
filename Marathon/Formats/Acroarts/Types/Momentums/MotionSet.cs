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

        public MotionSet(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
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

            in_reader.ReadAtOffset(in_parentChunk.Offset + paramsOffset, () =>
            {
                var offsets = new AnonymousMomentumParamSet(in_reader, in_parentChunk);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_parentChunk.Offset + offset.UInt32, () =>
                    {
                        Params.Add(new AnonymousMomentumParamSet(in_reader, in_parentChunk));
                    });
                }
            });
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
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

            in_writer.WriteReserved(paramsOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);

            in_writer.Write(Params.Count);
            in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint));

            var paramsOffsets = new List<long>();

            for (int i = 0; i < Params.Count; i++)
                paramsOffsets.Add(in_writer.Reserve<uint>());

            for (int i = 0; i < Params.Count; i++)
            {
                in_writer.WriteReserved(paramsOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                Params[i].Write(in_writer, in_parentChunk);
            }
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
