using Marathon.Formats.Acroarts.Chunks;
using Marathon.Formats.Acroarts.Types.Momentums;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types
{
    public class Momentum : INode
    {
        public uint Flags { get; set; }

        public MomentumType Type { get; set; }

        public int SwitchID { get; set; } = -1;

        public float StartTime { get; set; }

        public float EndTime { get; set; }

        public IMomentumParamSet Params { get; set; }

        public Momentum() { }

        public Momentum(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            var position = in_reader.Position;

            Flags = in_reader.Read<uint>();
            Type = in_reader.Read<MomentumType>();
            SwitchID = in_reader.Read<int>();
            StartTime = in_reader.Read<float>();
            EndTime = in_reader.Read<float>();

            var paramPosition = in_reader.Position;
            var paramCount = in_reader.Read<uint>();
            var paramOffset = in_reader.Read<uint>();

            in_reader.JumpTo(in_parentChunk.Offset + paramOffset);

            Params = MomentumFactory.ReadMomentumByType(in_reader, in_parentChunk, Type);

            if (Params == null)
            {
                in_reader.JumpTo(paramPosition);

                Params = new AnonymousMomentumParamSet(in_reader, in_parentChunk);

                Logger.Warning($"[Momentum] Unimplemented type at 0x{position:X08} of length {paramCount * 4}: {Type}");
            }
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            in_writer.Write(Flags);
            in_writer.Write(Type);
            in_writer.Write(SwitchID);
            in_writer.Write(StartTime);
            in_writer.Write(EndTime);

            var paramCount = Params.GetParamCount();

            if (paramCount <= 0)
            {
                in_writer.WriteZero<long>();
            }
            else
            {
                in_writer.Write(Params.GetParamCount());
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint)); // Params offset.

                Params.Write(in_writer, in_parentChunk);
            }
        }
    }
}
