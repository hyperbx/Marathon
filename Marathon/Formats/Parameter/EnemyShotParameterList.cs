using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
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
    public class EnemyShotParameterList : FileBase, IList<EnemyShotParameter>
    {
        private const string _extension = ".bin"; // "BINary"

        public EnemyShotParameterList() { }

        public EnemyShotParameterList(string in_path) : base(in_path) { }

        public List<EnemyShotParameter> Parameters { get; set; } = [];

        public int Count => Parameters.Count;

        public bool IsReadOnly => false;

        public EnemyShotParameter this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public EnemyShotParameter this[string in_name]
        {
            get => Parameters.Find(x => x.Name == in_name);
        }

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
                    () => param.ParticleContainerA = reader.ReadStringNullTerminated());

                var particleNameAOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameAOffset,
                    () => param.ParticleNameA = reader.ReadStringNullTerminated());

                var soundBankOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundBankOffset,
                    () => param.SoundBank = reader.ReadStringNullTerminated());

                var soundNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundNameOffset,
                    () => param.SoundName = reader.ReadStringNullTerminated());

                var particleContainerBOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerBOffset,
                    () => param.ParticleContainerB = reader.ReadStringNullTerminated());

                var particleNameBOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameBOffset,
                    () => param.ParticleNameB = reader.ReadStringNullTerminated());

                var particleContainerCOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerCOffset,
                    () => param.ParticleContainerC = reader.ReadStringNullTerminated());

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
                writer.WriteStringOffset(Parameters[i].ParticleContainerA);
                writer.WriteStringOffset(Parameters[i].ParticleNameA);
                writer.WriteStringOffset(Parameters[i].SoundBank);
                writer.WriteStringOffset(Parameters[i].SoundName);
                writer.WriteStringOffset(Parameters[i].ParticleContainerB);
                writer.WriteStringOffset(Parameters[i].ParticleNameB);
                writer.WriteStringOffset(Parameters[i].ParticleContainerC);
                writer.WriteStringOffset(Parameters[i].ParticleNameC);
                writer.WriteStringOffset(Parameters[i].UnknownField15);
            }

            writer.Write(0);
            writer.FinishWrite();
        }

        public int IndexOf(EnemyShotParameter in_item)
        {
            return Parameters.IndexOf(in_item);
        }

        public void Insert(int in_index, EnemyShotParameter in_item)
        {
            Parameters.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Parameters.RemoveAt(in_index);
        }

        public void Add(EnemyShotParameter in_item)
        {
            Parameters.Add(in_item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(EnemyShotParameter in_item)
        {
            return Parameters.Contains(in_item);
        }

        public void CopyTo(EnemyShotParameter[] in_array, int in_arrayIndex)
        {
            Parameters.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(EnemyShotParameter in_item)
        {
            return Parameters.Remove(in_item);
        }

        public IEnumerator<EnemyShotParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// The location of a particle container for one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerA { get; set; }

        /// <summary>
        /// The name of the particle used by this projectile from the first particle container.
        /// </summary>
        public string ParticleNameA { get; set; }

        /// <summary>
        /// The location of the sound bank for this projectile.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The name of the sound used when this projectile is destroyed.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// The location of a particle container for one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerB { get; set; }

        /// <summary>
        /// The name of the particle used by this projectile from the second particle container.
        /// </summary>
        public string ParticleNameB { get; set; }

        /// <summary>
        /// The location of a particle container for one of this projectile's particle effects.
        /// </summary>
        public string ParticleContainerC { get; set; }

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