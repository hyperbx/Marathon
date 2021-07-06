using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Particle
{
    public class ParticleEffect
    {
        /// <summary>
        /// Name of this particle effect.
        /// </summary>
        public string Name { get; set; }

        public List<ParticleEffectAttributes> Effects = new();

        public override string ToString() => Name;
    }

    public class ParticleEffectAttributes
    {
        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_1 { get; set; }

        /// <summary>
        /// How long this particle lasts.
        /// </summary>
        public float Lifetime { get; set; }

        /// <summary>
        /// The amount of particles cast from this effect.
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// Potentially unused? Always 0.
        /// </summary>
        public float UnknownFloat_1 { get; set; }

        /// <summary>
        /// How long this effect lasts.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// How quickly this effect travels along the X Axis.
        /// </summary>
        public float XSpeed { get; set; }
        
        /// <summary>
        /// How quickly this effect travels along the Y Axis?
        /// </summary>
        public float YSpeed { get; set; }

        /// <summary>
        /// How quickly this effect travels along the Z Axis.
        /// </summary>
        public float ZSpeed { get; set; }

        /// <summary>
        /// Affects something to do with movement on the Y Axis? (Particle Spread like the one below?)
        /// </summary>
        public float UnknownFloat_2 { get; set; }

        /// <summary>
        /// Something to do with the effect's spread on the Z Axis?
        /// </summary>
        public float UnknownFloat_3 { get; set; }

        /// <summary>
        /// How large this effect is? Might be affected by the flags?
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Something to do with randomness (radius?)
        /// </summary>
        public float UnknownFloat_4 { get; set; }

        public float UnknownFloat_5 { get; set; }

        public float UnknownFloat_6 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_2 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_3 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_4 { get; set; }

        /// <summary>
        /// The shader system to use in sonicnext.pgs
        /// </summary>
        public string Shader { get; set; }

        /// <summary>
        /// The ptb file to pull from for the first texture.
        /// </summary>
        public string TextureBank1 { get; set; }

        /// <summary>
        /// The texture entry to look for for the first texture.
        /// </summary>
        public string Texture1 { get; set; }

        /// <summary>
        /// The ptb file to pull from for the second texture.
        /// </summary>
        public string TextureBank2 { get; set; }

        /// <summary>
        /// The texture entry to look for for the second texture.
        /// </summary>
        public string Texture2 { get; set; }

        /// <summary>
        /// The ptb file to pull from for the third texture.
        /// </summary>
        public string TextureBank3 { get; set; }

        /// <summary>
        /// The texture entry to look for for the third texture.
        /// </summary>
        public string Texture3 { get; set; }

        /// <summary>
        /// Literally no clue what this is, length is based on a value before its offset.
        /// </summary>
        public byte[] CompletelyUnknownData { get; set; }
    }

    public class ParticleEffectBank : FileBase
    {
        public ParticleEffectBank() { }

        public ParticleEffectBank(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Data = JsonDeserialise<FormatData>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Signature = "BEEP",
                            Extension = ".peb";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ParticleEffect> Effects = new();

            public override string ToString() => Name;
        }

        public FormatData Data = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            reader.JumpAhead(0x8); // Always Null.
            uint EntryCount = reader.ReadUInt32();
            Data.Name = new string(reader.ReadChars(0x20)).Trim('\0');
            uint OffsetTable = reader.ReadUInt32();

            reader.JumpTo(OffsetTable, true); // Should already be here but just to be safe.

            // Particle Effect Entries.
            for (int i = 0; i < EntryCount; i++)
            {
                ParticleEffect particle = new();

                particle.Name = new string(reader.ReadChars(0x40)).Trim('\0');
                uint effectCount = reader.ReadUInt32();
                uint effectsOffset = reader.ReadUInt32();

                long position = reader.BaseStream.Position;

                reader.JumpTo(effectsOffset, true);
                for (int e = 0; e < effectCount; e++)
                {
                    ParticleEffectAttributes peAttributes = new();

                    peAttributes.UnknownUInt32_1 = reader.ReadUInt32();
                    peAttributes.Lifetime        = reader.ReadSingle();
                    peAttributes.Density         = reader.ReadSingle();
                    peAttributes.UnknownFloat_1  = reader.ReadSingle();
                    peAttributes.Duration        = reader.ReadSingle();
                    peAttributes.XSpeed          = reader.ReadSingle();
                    peAttributes.YSpeed          = reader.ReadSingle();
                    peAttributes.ZSpeed          = reader.ReadSingle();
                    peAttributes.UnknownFloat_2  = reader.ReadSingle();
                    peAttributes.UnknownFloat_3  = reader.ReadSingle();
                    peAttributes.Scale           = reader.ReadSingle();
                    peAttributes.UnknownFloat_4  = reader.ReadSingle();
                    peAttributes.UnknownFloat_5  = reader.ReadSingle();
                    peAttributes.UnknownFloat_6  = reader.ReadSingle();
                    peAttributes.UnknownUInt32_2 = reader.ReadUInt32();
                    peAttributes.UnknownUInt32_3 = reader.ReadUInt32();
                    peAttributes.UnknownUInt32_4 = reader.ReadUInt32();

                    peAttributes.Shader       = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.TextureBank1 = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.Texture1     = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.TextureBank2 = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.Texture2     = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.TextureBank3 = new string(reader.ReadChars(0x20)).Trim('\0');
                    peAttributes.Texture3     = new string(reader.ReadChars(0x20)).Trim('\0');

                    int UnknownLength  = reader.ReadInt32(); // At least I THINK this is the length of the data from the UnknownOffset? Always a multiple of 8 + 4?
                    uint UnknownOffset = reader.ReadUInt32();

                    long currentPos = reader.BaseStream.Position;

                    reader.JumpTo(UnknownOffset, true);
                    peAttributes.CompletelyUnknownData = reader.ReadBytes(UnknownLength);
                    reader.JumpTo(currentPos);

                    particle.Effects.Add(peAttributes);
                }

                reader.JumpTo(position);
                Data.Effects.Add(particle);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.WriteNulls(0x8);
            writer.Write(Data.Effects.Count);
            writer.WriteNullPaddedString(Data.Name, 0x20);
            writer.AddOffset("OffsetTable");

            writer.FillOffset("OffsetTable", true);

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                writer.WriteNullPaddedString(Data.Effects[i].Name, 0x40);
                writer.Write(Data.Effects[i].Effects.Count);
                writer.AddOffset($"EffectsOffset_{i}");
            }

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                writer.FillOffset($"EffectsOffset_{i}", true);
                for (int e = 0; e < Data.Effects[i].Effects.Count; e++)
                {
                    var currentEffect = Data.Effects[i].Effects[e];

                    writer.Write(currentEffect.UnknownUInt32_1);
                    writer.Write(currentEffect.Lifetime);
                    writer.Write(currentEffect.Density);
                    writer.Write(currentEffect.UnknownFloat_1);
                    writer.Write(currentEffect.Duration);
                    writer.Write(currentEffect.XSpeed);
                    writer.Write(currentEffect.YSpeed);
                    writer.Write(currentEffect.ZSpeed);
                    writer.Write(currentEffect.UnknownFloat_2);
                    writer.Write(currentEffect.UnknownFloat_3);
                    writer.Write(currentEffect.Scale);
                    writer.Write(currentEffect.UnknownFloat_4);
                    writer.Write(currentEffect.UnknownFloat_5);
                    writer.Write(currentEffect.UnknownFloat_6);
                    writer.Write(currentEffect.UnknownUInt32_2);
                    writer.Write(currentEffect.UnknownUInt32_3);
                    writer.Write(currentEffect.UnknownUInt32_4);

                    writer.WriteNullPaddedString(currentEffect.Shader, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBank1, 0x20);
                    writer.WriteNullPaddedString(currentEffect.Texture1, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBank2, 0x20);
                    writer.WriteNullPaddedString(currentEffect.Texture2, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBank3, 0x20);
                    writer.WriteNullPaddedString(currentEffect.Texture3, 0x20);

                    writer.Write(currentEffect.CompletelyUnknownData.Length);

                    writer.AddOffset($"Effects_{i}_UnknownOffset_{e}");
                }
            }

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                for (int e = 0; e < Data.Effects[i].Effects.Count; e++)
                {
                    writer.FillOffset($"Effects_{i}_UnknownOffset_{e}", true);
                    writer.Write(Data.Effects[i].Effects[e].CompletelyUnknownData);
                }
            }

            writer.FinishWrite();
        }
    }
}
