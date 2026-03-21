using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types
{
    public class MomentumList : IBinarySerializableEx
    {
        public uint Flags { get; set; }

        public int ID { get; set; }

        public uint CoordTarget { get; set; }

        public uint CoordType { get; set; } = 1;

        public uint CoordNode { get; set; }

        public List<Momentum> Momentums { get; set; } = [];

        public MomentumList() { }

        public MomentumList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Flags = in_reader.Read<uint>();
            ID = in_reader.Read<int>();
            CoordTarget = in_reader.Read<uint>();
            CoordType = in_reader.Read<uint>();
            CoordNode = in_reader.Read<uint>();

            // TODO: test this.
            var unkField = in_reader.Read<uint>();

            if (unkField > 0)
                Logger.Warning($"[MomentumList] Unknown field is non-zero: {unkField}");

            var momentumCount = in_reader.Read<uint>();
            var momentumTableOffset = in_reader.Read<uint>();

            in_reader.JumpTo(in_reader.CalculateOffset(momentumTableOffset));

            for (uint i = 0; i < momentumCount; i++)
            {
                var momentumOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(momentumOffset), () =>
                {
                    Momentums.Add(new Momentum(in_reader));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Flags);
            in_writer.Write(ID);
            in_writer.Write(CoordTarget);
            in_writer.Write(CoordType);
            in_writer.Write(CoordNode);
            in_writer.WriteZero<int>(); // TODO: unkField

            if (Momentums.Count <= 0)
            {
                in_writer.WriteZero<long>();
            }
            else
            {
                in_writer.Write(Momentums.Count);
                in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative)); // Momentum table offset.

                var momentumOffsets = new List<long>();

                for (int i = 0; i < Momentums.Count; i++)
                    momentumOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Momentums.Count; i++)
                {
                    in_writer.WriteReserved(momentumOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    Momentums[i].Write(in_writer);
                }
            }
        }
    }
}
