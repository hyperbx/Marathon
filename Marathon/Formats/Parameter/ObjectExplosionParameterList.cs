using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        Object Explosion Parameter List
// Format references:   Sonicteam::ObjectExplosionParameterList
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for Explosion.bin files; used for configuring explosion parameters.
    /// </summary>
    public class ObjectExplosionParameterList : FileBase, IList<ObjectExplosionParameter>
    {
        private const string _extension = ".bin"; // "BINary"

        public ObjectExplosionParameterList() { }

        public ObjectExplosionParameterList(string in_path) : base(in_path) { }

        public List<ObjectExplosionParameter> Parameters { get; set; } = [];

        public int Count => Parameters.Count;

        public bool IsReadOnly => false;

        public ObjectExplosionParameter this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public ObjectExplosionParameter this[string in_name]
        {
            get => Parameters.Find((x) => x.Name == in_name);
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
                var param = new ObjectExplosionParameter();

                var nameOffset = reader.ReadUInt32();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => param.Name = reader.ReadStringNullTerminated());

                param.UnknownField1 = reader.Read<uint>();
                param.Radius = reader.Read<float>();
                param.UnknownField2 = reader.Read<float>();
                param.UnknownField3 = reader.Read<float>();
                param.UnknownField4 = reader.Read<float>();
                param.UnknownField5 = reader.Read<float>();
                param.Force = reader.Read<float>();
                param.Damage = reader.Read<uint>();
                param.Behaviour = reader.Read<uint>();

                var particleContainerOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerOffset,
                    () => param.ParticleContainer = reader.ReadStringNullTerminated());

                var particleNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameOffset,
                    () => param.ParticleName = reader.ReadStringNullTerminated());

                var soundBankOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundBankOffset,
                    () => param.SoundBank = reader.ReadStringNullTerminated());

                var soundNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundNameOffset,
                    () => param.SoundName = reader.ReadStringNullTerminated());

                var lightNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + lightNameOffset,
                    () => param.LightName = reader.ReadStringNullTerminated());

                // Always null.
                reader.JumpAhead(12);

                Parameters.Add(param);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.WriteStringOffset(Parameters[i].Name);
                writer.Write(Parameters[i].UnknownField1);
                writer.Write(Parameters[i].Radius);
                writer.Write(Parameters[i].UnknownField2);
                writer.Write(Parameters[i].UnknownField3);
                writer.Write(Parameters[i].UnknownField4);
                writer.Write(Parameters[i].UnknownField5);
                writer.Write(Parameters[i].Force);
                writer.Write(Parameters[i].Damage);
                writer.Write(Parameters[i].Behaviour);
                writer.WriteStringOffset(Parameters[i].ParticleContainer);
                writer.WriteStringOffset(Parameters[i].ParticleName);
                writer.WriteStringOffset(Parameters[i].SoundBank);
                writer.WriteStringOffset(Parameters[i].SoundName);
                writer.WriteStringOffset(Parameters[i].LightName);
                writer.WriteNullBytes(12);
            }

            writer.Write(0);
            writer.FinishWrite();
        }

        public int IndexOf(ObjectExplosionParameter in_item)
        {
            return Parameters.IndexOf(in_item);
        }

        public void Insert(int in_index, ObjectExplosionParameter in_item)
        {
            Parameters.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Parameters.RemoveAt(in_index);
        }

        public void Add(ObjectExplosionParameter in_item)
        {
            Parameters.Add(in_item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(ObjectExplosionParameter in_item)
        {
            return Parameters.Contains(in_item);
        }

        public void CopyTo(ObjectExplosionParameter[] in_array, int in_arrayIndex)
        {
            Parameters.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(ObjectExplosionParameter in_item)
        {
            return Parameters.Remove(in_item);
        }

        public IEnumerator<ObjectExplosionParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ObjectExplosionParameter
    {
        /// <summary>
        /// The name of this explosion.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TODO: unknown - setting this to 1 allowed a BombBox explosion to kill enemies, 0 and 2 did not.
        /// </summary>
        public uint UnknownField1 { get; set; }

        /// <summary>
        /// The size of this explosion.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// TODO: unknown - usually always the same as <see cref="Radius"/>.
        /// </summary>
        public float UnknownField2 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField3 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField4 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField5 { get; set; }

        /// <summary>
        /// TODO: unknown - increasing this value seems to affect how the explosion affects other physics objects?
        /// </summary>
        public float Force { get; set; }

        /// <summary>
        /// The amount of damage this explosion causes.
        /// </summary>
        public uint Damage { get; set; }

        /// <summary>
        /// TODO: unknown - has many different values, 46 made every explosion stun enemies like a FlashBox and be unable to damage the player.
        /// </summary>
        public uint Behaviour { get; set; }

        /// <summary>
        /// The location of the particle container used for this explosion.
        /// </summary>
        public string ParticleContainer { get; set; }

        /// <summary>
        /// The name of the particle used for this explosion.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// The location of the sound bank used for this explosion.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The name of the sound to use for this explosion.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// The location of the light animation (*.xni) used for this explosion.
        /// </summary>
        public string LightName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
