using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;

// Format names:        Particle Texture Bank
// Format references:   Sonicteam::GE1PE::TextureBank
// Format designers:    Sonic Team, SEGA Global Entertainment R&D Dept. #1
// Format researchers:  Knuxfan24, Hyper, GordinRamsay

namespace Marathon.Formats.Particle
{
    /// <summary>
    /// Support for *.ptb files; used for defining textures for particle effects.
    /// </summary>
    public class ParticleTextureBank : FileBase
    {
        private const string _extension = ".ptb"; // "Particle Texture Bank"
        private const string _signature = "BTEP"; // "Particle Effect Texture Bank" (reverse)

        /// <summary>
        /// The name of this texture bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The textures in this bank.
        /// </summary>
        public List<ParticleTexture> Textures { get; set; } = [];

        public override string Extension => _extension;

        public ParticleTexture this[int in_index]
        {
            get => Textures[in_index];
            set => Textures[in_index] = value;
        }

        public ParticleTexture this[string in_name]
        {
            get => Textures.Find(x => x.Name == in_name);
        }

        public ParticleTextureBank() { }

        public ParticleTextureBank(string in_path) : base(in_path) { }

        public ParticleTextureBank(Stream in_stream) : base(in_stream) { }

        public ParticleTextureBank(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            // Always null.
            reader.JumpAhead(8);

            var entryCount = reader.ReadUInt32();

            Name = reader.ReadStringFixedLength(0x20);

            var entryTableOffset = reader.ReadUInt32();

            for (int i = 0; i < entryCount; i++)
            {
                var texture = new ParticleTexture()
                {
                    Name = reader.ReadStringFixedLength(0x20),
                    Location = reader.ReadStringFixedLength(0x80),
                    UnknownField1 = reader.Read<uint>(),
                    UnknownField2 = reader.Read<uint>()
                };

                Textures.Add(texture);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.WriteNullBytes(8);
            writer.Write(Textures.Count);
            writer.WriteStringFixedLength(Name, 0x20);

            if (Textures.Count <= 0)
            {
                writer.Write(0);
            }
            else
            {
                writer.Reserve<uint>("EntryTableOffset");
                writer.WriteReserved("EntryTableOffset", (uint)writer.Position - BINAHeader.Size);
            }

            for (int i = 0; i < Textures.Count; i++)
            {
                writer.WriteStringFixedLength(Textures[i].Name, 0x20);
                writer.WriteStringFixedLength(Textures[i].Location, 0x80);
                writer.Write(Textures[i].UnknownField1);
                writer.Write(Textures[i].UnknownField2);
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ParticleTexture
    {
        /// <summary>
        /// The name of this particle texture.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location of the texture.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField1 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField2 { get; set; }

        public ParticleTexture() { }

        public ParticleTexture(string in_name, string in_location, uint in_unkField1, uint in_unkField2)
        {
            Name = in_name;
            Location = in_location;
            UnknownField1 = in_unkField1;
            UnknownField2 = in_unkField2;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
