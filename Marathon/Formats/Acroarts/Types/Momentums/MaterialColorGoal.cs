using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Types;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorGoal : IMomentumParamSet
    {
        public ColourInfoSet[] Sets { get; set; } = new ColourInfoSet[4];

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorGoal() { }

        public MaterialColorGoal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            for (int i = 0; i < Sets.Length; i++)
            {
                var offset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_parentChunk.Offset + offset, () =>
                {
                    Sets[i] = new ColourInfoSet(in_reader, in_parentChunk);
                });
            }

            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            var offsets = new long[Sets.Length];

            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(ColorBlendMode);

            for (int i = 0; i < Sets.Length; i++)
            {
                in_writer.WriteReserved(offsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                Sets[i].Write(in_writer, in_parentChunk);
            }
        }

        public uint GetParamCount()
        {
            return 7;
        }

        public class ColourInfoSet : List<ColourInfo>, IMomentumParamSet
        {
            private List<long> _arrayPtrOffsets = [];

            public ColourInfoSet() { }

            public ColourInfoSet(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
            {
                Read(in_reader, in_parentChunk);
            }

            public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
            {
                var offsets = new AnonymousMomentumParamSet(in_reader, in_parentChunk);

                foreach (var offset in offsets)
                {
                    in_reader.ReadAtOffset(in_parentChunk.Offset + offset.UInt32, () =>
                    {
                        var paramCount = in_reader.Read<uint>();
                        var paramOffset = in_reader.Read<uint>();

                        in_reader.ReadAtOffset(in_parentChunk.Offset + paramOffset, () =>
                        {
                            Add(new ColourInfo(in_reader, in_parentChunk));
                        });
                    });
                }
            }

            public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
            {
                in_writer.Write(Count);
                in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint));

                for (int i = 0; i < Count; i++)
                    _arrayPtrOffsets.Add(in_writer.Reserve<uint>());

                for (int i = 0; i < Count; i++)
                {
                    in_writer.WriteReserved(_arrayPtrOffsets[i], (uint)(in_writer.Position - in_parentChunk.Offset), false);
                    in_writer.Write(this[i].GetParamCount());
                    in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint));
                    this[i].Write(in_writer, in_parentChunk);
                }
            }

            public uint GetParamCount()
            {
                return (uint)Count;
            }
        }

        public class ColourInfo : IMomentumParamSet
        {
            public Colour<float, RGBA> Colour { get; set; }

            public uint UnknownField1 { get; set; }

            public uint UnknownField2 { get; set; }

            public int UnknownField3 { get; set; }

            public ColourInfo() { }

            public ColourInfo(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
            {
                Read(in_reader, in_parentChunk);
            }

            public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
            {
                Colour = in_reader.ReadObject<Colour<float, RGBA>>();
                UnknownField1 = in_reader.Read<uint>();
                UnknownField2 = in_reader.Read<uint>();
                UnknownField3 = in_reader.Read<int>();
            }

            public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
            {
                in_writer.WriteObject(Colour);
                in_writer.Write(UnknownField1);
                in_writer.Write(UnknownField2);
                in_writer.Write(UnknownField3);
            }

            public uint GetParamCount()
            {
                return 7;
            }
        }
    }
}
