using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        Enemy Parameter List
// Format references:   Sonicteam::Enemy::ParameterList
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper, GordinRamsay

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for ScriptParameter.bin files; used for configuring enemy parameters.
    /// </summary>
    public class EnemyParameterList : FileBase, IList<EnemyParameter>
    {
        private const string _extension = ".bin"; // "BINary"

        public EnemyParameterList() { }

        public EnemyParameterList(string in_path) : base(in_path) { }

        public List<EnemyParameter> Parameters { get; set; } = [];

        public int Count => Parameters.Count;

        public bool IsReadOnly => false;

        public EnemyParameter this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public EnemyParameter this[string in_name]
        {
            get => Parameters.Find(x => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var begin = reader.Position;
            var stringPoolOffset = reader.Read<uint>() + BINAHeader.Size;
            var paramCount = stringPoolOffset / EnemyParameter.Size;
            
            reader.JumpTo(begin);

            for (int i = 0; i < paramCount; i++)
            {
                var param = new EnemyParameter();

                var nameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => param.Name = reader.ReadStringNullTerminated());

                param.Behaviour = reader.Read<EnemyBehaviour>();
                param.Health = reader.Read<uint>();
                param.Score = reader.Read<uint>();
                param.UnknownField1 = reader.Read<float>();
                param.FindRangeIn = reader.Read<float>();
                param.FindRangeOut = reader.Read<float>();
                param.UnknownField2 = reader.Read<float>();
                param.UnknownField3 = reader.Read<float>();
                param.MovementSpeed = reader.Read<float>();
                param.UnknownField4 = reader.Read<float>();
                param.UnknownField5 = reader.Read<float>();
                param.RotationSpeed = reader.Read<float>();
                param.UnknownField6 = reader.Read<float>();
                param.UnknownField7 = reader.Read<float>();
                param.UnknownField8 = reader.Read<float>();
                param.UnknownField9 = reader.Read<float>();
                param.UnknownField10 = reader.Read<float>();
                param.UnknownField11 = reader.Read<float>();
                param.ReactionTime = reader.Read<float>();
                param.UnknownField12 = reader.Read<float>();

                Parameters.Add(param);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            foreach (var param in Parameters)
            {
                writer.WriteStringOffset(param.Name);
                writer.Write(param.Behaviour);
                writer.Write(param.Health);
                writer.Write(param.Score);
                writer.Write(param.UnknownField1);
                writer.Write(param.FindRangeIn);
                writer.Write(param.FindRangeOut);
                writer.Write(param.UnknownField2);
                writer.Write(param.UnknownField3);
                writer.Write(param.MovementSpeed);
                writer.Write(param.UnknownField4);
                writer.Write(param.UnknownField5);
                writer.Write(param.RotationSpeed);
                writer.Write(param.UnknownField6);
                writer.Write(param.UnknownField7);
                writer.Write(param.UnknownField8);
                writer.Write(param.UnknownField9);
                writer.Write(param.UnknownField10);
                writer.Write(param.UnknownField11);
                writer.Write(param.ReactionTime);
                writer.Write(param.UnknownField12);
            }

            writer.Write(0);
            writer.FinishWrite();
        }

        public int IndexOf(EnemyParameter in_item)
        {
            return Parameters.IndexOf(in_item);
        }

        public void Insert(int in_index, EnemyParameter in_item)
        {
            Parameters.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Parameters.RemoveAt(in_index);
        }

        public void Add(EnemyParameter in_item)
        {
            Parameters.Add(in_item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(EnemyParameter in_item)
        {
            return Parameters.Contains(in_item);
        }

        public void CopyTo(EnemyParameter[] in_array, int in_arrayIndex)
        {
            Parameters.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(EnemyParameter in_item)
        {
            return Parameters.Remove(in_item);
        }

        public IEnumerator<EnemyParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class EnemyParameter
    {
        public const uint Size = 0x54;

        /// <summary>
        /// The name of this enemy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The behaviour of this enemy.
        /// <para>TODO: there are some unknown types in here.</para>
        /// </summary>
        public EnemyBehaviour Behaviour { get; set; }

        /// <summary>
        /// The health of this enemy.
        /// </summary>
        public uint Health { get; set; }

        /// <summary>
        /// The score rewarded for killing this enemy.
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField1 { get; set; }

        /// <summary>
        /// The range the player must be in for this enemy to notice them.
        /// </summary>
        public float FindRangeIn { get; set; }

        /// <summary>
        /// The range the player must be in for this enemy to stop seeing them.
        /// </summary>
        public float FindRangeOut { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField2 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField3 { get; set; }

        /// <summary>
        /// The speed the enemies move at whilst searching for the player.
        /// </summary>
        public float MovementSpeed { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField4 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField5 { get; set; }

        /// <summary>
        /// The speed the enemies rotate at whilst searching for the player.
        /// </summary>
        public float RotationSpeed { get; set; }

        /// <summary>
        /// TODO: unknown, affects rotation speed.
        /// </summary>
        public float UnknownField6 { get; set; }

        /// <summary>
        /// TODO: unknown, affects rotation speed.
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
        /// The time taken for the enemies to react to seeing the player.
        /// </summary>
        public float ReactionTime { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField12 { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnemyBehaviour : uint
    {
        Normal,
        Fly,
        Wall = 3,
        AllAround
    }
}
