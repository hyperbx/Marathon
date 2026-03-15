using Marathon.Formats.Acroarts.Chunks;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public class MomentumList : INode
    {
        public uint Flags { get; set; }

        public int ID { get; set; }

        public uint CoordTarget { get; set; }

        public uint CoordType { get; set; } = 1;

        public uint CoordNode { get; set; }

        public List<Momentum> Momentums { get; set; } = [];

        public MomentumList() { }

        public MomentumList(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
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

            in_reader.Seek(in_parentChunk.Offset + momentumTableOffset, SeekOrigin.Begin);

            for (uint i = 0; i < momentumCount; i++)
            {
                var momentumOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_parentChunk.Offset + momentumOffset, () =>
                {
                    Momentums.Add(new Momentum(in_reader, in_parentChunk));
                });
            }
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
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
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint)); // Momentum table offset.

                var momentumOffsets = new List<long>();

                for (int i = 0; i < Momentums.Count; i++)
                    momentumOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Momentums.Count; i++)
                {
                    in_writer.WriteReserved(momentumOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                    Momentums[i].Write(in_writer, in_parentChunk);
                }
            }
        }
    }
}
