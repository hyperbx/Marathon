using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class EffectListChunk : IChunk
    {
        public const string ID = "NXEF";

        public uint Type { get; set; }

        public List<Effect> Effects { get; set; } = [];

        public List<Technique> Techniques { get; set; } = [];

        public List<short> TechniqueIndices { get; set; } = [];

        public EffectListChunk() { }

        public EffectListChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.Seek(InfoChunk.Size + header.DataOffset, SeekOrigin.Begin);

            Type = in_reader.Read<uint>();

            var effectFileCount = in_reader.Read<uint>();
            var effectFileOffset = in_reader.Read<uint>();

            var techniqueNameCount = in_reader.Read<uint>();
            var techniqueNameOffset = in_reader.Read<uint>();

            var techniqueIndicesCount = in_reader.Read<uint>();
            var techniqueIndicesOffset = in_reader.Read<uint>();

            in_reader.Seek(InfoChunk.Size + effectFileOffset, SeekOrigin.Begin);

            for (int i = 0; i < effectFileCount; i++)
                Effects.Add(new(in_reader));

            in_reader.Seek(InfoChunk.Size + techniqueNameOffset, SeekOrigin.Begin);

            for (int i = 0; i < techniqueNameCount; i++)
                Techniques.Add(new(in_reader));

            in_reader.Seek(InfoChunk.Size + techniqueIndicesOffset, SeekOrigin.Begin);

            for (int i = 0; i < techniqueIndicesCount; i++)
                TechniqueIndices.Add(in_reader.Read<short>());
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var effectsOffset = (int)(in_writer.Position - InfoChunk.Size);
            var effectsOffsets = new List<long>();

            for (int i = 0; i < Effects.Count; i++)
                effectsOffsets.Add(Effects[i].Write(in_writer));

            var techniqueNamesOffset = (int)(in_writer.Position - InfoChunk.Size);
            var techniqueNamesOffsets = new List<long>();

            for (int i = 0; i < Techniques.Count; i++)
                techniqueNamesOffsets.Add(Techniques[i].Write(in_writer));

            var techniqueIndicesOffset = (int)(in_writer.Position - InfoChunk.Size);

            for (int i = 0; i < TechniqueIndices.Count; i++)
                in_writer.Write(TechniqueIndices[i]);

            in_writer.Align(4);

            var dataOffset = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Type);

            in_writer.Write(Effects.Count);
            var effectsOffsetField = in_writer.Reserve<uint>();
            in_writer.WriteReserved(effectsOffsetField, effectsOffset, false);

            in_writer.Write(Techniques.Count);
            var techniqueNamesOffsetField = in_writer.Reserve<uint>();
            in_writer.WriteReserved(techniqueNamesOffsetField, techniqueNamesOffset, false);

            in_writer.Write(TechniqueIndices.Count);
            var techniqueIndicesOffsetField = in_writer.Reserve<uint>();
            in_writer.WriteReserved(techniqueIndicesOffsetField, techniqueIndicesOffset, false);

            for (int i = 0; i < Effects.Count; i++)
            {
                in_writer.WriteReserved(effectsOffsets[i], (int)(in_writer.Position - InfoChunk.Size), false);
                in_writer.WriteStringNullTerminated(Effects[i].Name);
            }

            for (int i = 0; i < Techniques.Count; i++)
            {
                in_writer.WriteReserved(techniqueNamesOffsets[i], (int)(in_writer.Position - InfoChunk.Size), false);
                in_writer.WriteStringNullTerminated(Techniques[i].Name);
            }

            in_writer.Align(16);

            header.FinishWrite(in_writer, dataOffset);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
