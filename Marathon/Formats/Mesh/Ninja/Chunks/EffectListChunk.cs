using Marathon.Exceptions;
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
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            var infoOffset = in_reader.Read<uint>();

            in_reader.Seek(InfoChunk.Size + infoOffset, SeekOrigin.Begin);

            Type = in_reader.Read<uint>();

            var effectFileCount = in_reader.Read<uint>();
            var effectFileOffset = in_reader.Read<uint>();

            var techniqueNameCount = in_reader.Read<uint>();
            var techniqueNameOffset = in_reader.Read<uint>();

            var techniqueIndicesCount = in_reader.Read<uint>();
            var techniqueIndicesOffset = in_reader.Read<uint>();

            in_reader.Seek(InfoChunk.Size + effectFileOffset, SeekOrigin.Begin);

            for (int i = 0; i < effectFileCount; i++)
                Effects.Add(new Effect(in_reader));

            in_reader.Seek(InfoChunk.Size + techniqueNameOffset, SeekOrigin.Begin);

            for (int i = 0; i < techniqueNameCount; i++)
                Techniques.Add(new Technique(in_reader));

            in_reader.Seek(InfoChunk.Size + techniqueIndicesOffset, SeekOrigin.Begin);

            for (int i = 0; i < techniqueIndicesCount; i++)
                TechniqueIndices.Add(in_reader.Read<short>());
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);

            var length = in_writer.Reserve<uint>();
            var infoOffset = in_writer.Reserve<uint>();

            in_writer.Align(16);

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

            in_writer.WriteReserved(infoOffset, (int)(in_writer.Position - InfoChunk.Size));

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

            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }

    public class Effect
    {
        public const int Size = 8;

        public uint Type { get; set; }

        public string Name { get; set; }

        public Effect() { }

        public Effect(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());
        }

        public long Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);

            return in_writer.Reserve<uint>();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Technique
    {
        public const int Size = 8;

        public uint Type { get; set; }

        public uint EffectIndex { get; set; }

        public string Name { get; set; }

        public Technique() { }

        public Technique(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();
            EffectIndex = in_reader.Read<uint>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());
        }

        public long Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(EffectIndex);

            return in_writer.Reserve<uint>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
