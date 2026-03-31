using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;

// Format names:        Enemy Shot Parameter List
// Format references:   Sonicteam::Enemy::ShotParameterList
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for ShotParameter.bin files; used for configuring enemy projectiles.
    /// </summary>
    [FileType("Enemy Shot Parameter List", "Parameter", @"ShotParameter\.bin$", true)]
    public class EnemyShotParameterList : FileBase
    {
        private const string _extension = ".bin"; // "BINary"

        public List<EnemyShotParameter> Parameters { get; set; } = [];

        public override string Extension => _extension;

        public EnemyShotParameter this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public EnemyShotParameter this[string in_name]
        {
            get => Parameters.Find(x => x.Name == in_name);
        }

        public EnemyShotParameterList() { }

        public EnemyShotParameterList(string in_path) : base(in_path) { }

        public EnemyShotParameterList(Stream in_stream) : base(in_stream) { }

        public EnemyShotParameterList(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var pos = reader.Position;
            var stringPoolOffset = reader.ReadUInt32();

            reader.JumpTo(pos);

            while (reader.Position < stringPoolOffset)
            {
                var param = new EnemyShotParameter();

                var nameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => param.Name = reader.ReadStringNullTerminated());

                var modelOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + modelOffset,
                    () => param.Model = reader.ReadStringNullTerminated());

                var unkField1Offset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + unkField1Offset,
                    () => param.UnknownField1 = reader.ReadStringNullTerminated());

                var unkField2Offset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + unkField2Offset,
                    () => param.UnknownField2 = reader.ReadStringNullTerminated());

                param.UnknownField3 = reader.Read<uint>();
                param.UnknownField4 = reader.Read<float>();
                param.UnknownField5 = reader.Read<float>();
                param.UnknownField6 = reader.Read<uint>();
                param.UnknownField7 = reader.Read<float>();
                param.UnknownField8 = reader.Read<float>();
                param.UnknownField9 = reader.Read<float>();
                param.UnknownField10 = reader.Read<float>();
                param.UnknownField11 = reader.Read<float>();
                param.UnknownField12 = reader.Read<uint>();
                param.UnknownField13 = reader.Read<float>();
                param.UnknownField14 = reader.Read<float>();

                var explosionNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + explosionNameOffset,
                    () => param.ExplosionName = reader.ReadStringNullTerminated());

                var particleContainerAOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerAOffset,
                    () => param.ParticleContainerNameA = reader.ReadStringNullTerminated());

                var particleNameAOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameAOffset,
                    () => param.ParticleNameA = reader.ReadStringNullTerminated());

                var soundBankOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundBankOffset,
                    () => param.SoundBankName = reader.ReadStringNullTerminated());

                var soundNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundNameOffset,
                    () => param.SoundName = reader.ReadStringNullTerminated());

                var particleContainerBOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerBOffset,
                    () => param.ParticleContainerNameB = reader.ReadStringNullTerminated());

                var particleNameBOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameBOffset,
                    () => param.ParticleNameB = reader.ReadStringNullTerminated());

                var particleContainerCOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerCOffset,
                    () => param.ParticleContainerNameC = reader.ReadStringNullTerminated());

                var particleNameCOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameCOffset,
                    () => param.ParticleNameC = reader.ReadStringNullTerminated());

                var unkField15Offset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + unkField15Offset,
                    () => param.UnknownField15 = reader.ReadStringNullTerminated());

                Parameters.Add(param);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.WriteStringOffset(Parameters[i].Name);
                writer.WriteStringOffset(Parameters[i].Model);
                writer.WriteStringOffset(Parameters[i].UnknownField1);
                writer.WriteStringOffset(Parameters[i].UnknownField2);
                writer.Write(Parameters[i].UnknownField3);
                writer.Write(Parameters[i].UnknownField4);
                writer.Write(Parameters[i].UnknownField5);
                writer.Write(Parameters[i].UnknownField6);
                writer.Write(Parameters[i].UnknownField7);
                writer.Write(Parameters[i].UnknownField8);
                writer.Write(Parameters[i].UnknownField9);
                writer.Write(Parameters[i].UnknownField10);
                writer.Write(Parameters[i].UnknownField11);
                writer.Write(Parameters[i].UnknownField12);
                writer.Write(Parameters[i].UnknownField13);
                writer.Write(Parameters[i].UnknownField14);
                writer.WriteStringOffset(Parameters[i].ExplosionName);
                writer.WriteStringOffset(Parameters[i].ParticleContainerNameA);
                writer.WriteStringOffset(Parameters[i].ParticleNameA);
                writer.WriteStringOffset(Parameters[i].SoundBankName);
                writer.WriteStringOffset(Parameters[i].SoundName);
                writer.WriteStringOffset(Parameters[i].ParticleContainerNameB);
                writer.WriteStringOffset(Parameters[i].ParticleNameB);
                writer.WriteStringOffset(Parameters[i].ParticleContainerNameC);
                writer.WriteStringOffset(Parameters[i].ParticleNameC);
                writer.WriteStringOffset(Parameters[i].UnknownField15);
            }

            writer.Write(0);
            writer.FinishWrite();
        }
    }

    public class EnemyShotParameter
    {
        /// <summary>
        /// The name of this projectile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location of the model file for this projectile.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// TODO: unknown, only used by the Egg Buster missile?
        /// </summary>
        public string UnknownField1 { get; set; }

        /// <summary>
        /// TODO: unknown, only used by the Egg Buster missile?
        /// </summary>
        public string UnknownField2 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField3 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField4 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField5 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField6 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField7 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField8 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField9 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField10 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField11 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField12 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField13 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField14 { get; set; }

        /// <summary>
        /// The name of the explosion used when this projectile is destroyed.
        /// </summary>
        public string ExplosionName { get; set; }

        /// <summary>
        /// The name of a particle container used to source one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerNameA { get; set; }

        /// <summary>
        /// The name of the particle used by this projectile from the first particle container.
        /// </summary>
        public string ParticleNameA { get; set; }

        /// <summary>
        /// The location of the sound bank for this projectile.
        /// </summary>
        public string SoundBankName { get; set; }

        /// <summary>
        /// The name of the sound used when this projectile is destroyed.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// The name of a particle container used to source one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerNameB { get; set; }

        /// <summary>
        /// The name of the particle used by this projectile from the second particle container.
        /// </summary>
        public string ParticleNameB { get; set; }

        /// <summary>
        /// The name of a particle container used to source one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerNameC { get; set; }

        /// <summary>
        /// The name of the particle used by this projectile from the third particle container.
        /// </summary>
        public string ParticleNameC { get; set; }

        /// <summary>
        /// TODO: unknown, usually either empty or related to misfired stuff.
        /// </summary>
        public string UnknownField15 { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}