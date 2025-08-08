using Marathon.IO;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Reflection Area
// Format references:   Sonicteam::ReflectionArea
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for *.rab files; used for reflection zones.
    /// </summary>
    public class ReflectionArea : FileBase, IList<ReflectionAreaParam>
    {
        private const string _extension = ".rab"; // "Reflection Area Binary"

        public List<ReflectionAreaParam> Parameters { get; set; } = [];

        public int Count => Parameters.Count;

        public bool IsReadOnly => false;

        public ReflectionAreaParam this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public ReflectionArea() { }

        public ReflectionArea(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var reflectionCount = reader.Read<uint>();
            var reflectionTableOffset = reader.Read<uint>();
            var entryTableCount = reader.Read<uint>();
            var entryTableOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + reflectionTableOffset);

            for (int i = 0; i < reflectionCount; i++)
            {
                var param = new ReflectionAreaParam()
                {
                    Pitch = reader.Read<float>(),
                    UnknownField = reader.Read<float>(),
                    Roll = reader.Read<float>(),
                    Y = reader.Read<float>()
                };

                Parameters.Add(param);
            }

            reader.JumpTo(BINAHeader.Size + entryTableOffset);

            for (int i = 0; i < entryTableCount; i++)
            {
                var vertexCount = reader.Read<uint>();
                var vertexTableOffset = reader.Read<uint>();
                var reflectionIndex = reader.Read<uint>();

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + vertexTableOffset);

                for (int j = 0; j < vertexCount; j++)
                    Parameters[(int)reflectionIndex].Vertices.Add(reader.Read<Vector3>());

                reader.JumpTo(pos);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Write(Parameters.Count);
            writer.Reserve<uint>("ReflectionTableOffset");

            writer.Write(Parameters.Count);
            writer.Reserve<uint>("EntryTableOffset");

            writer.WriteReserved("ReflectionTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.Write(Parameters[i].Pitch);
                writer.Write(Parameters[i].UnknownField);
                writer.Write(Parameters[i].Roll);
                writer.Write(Parameters[i].Y);
            }

            writer.WriteReserved("EntryTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.Write(Parameters[i].Vertices.Count);
                writer.Reserve<uint>($"VertexTableOffset{i}");
                writer.Write(i);
            }

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.WriteReserved($"VertexTableOffset{i}", (uint)writer.Position - BINAHeader.Size);

                foreach (var vector in Parameters[i].Vertices)
                    writer.Write(vector);
            }

            writer.FinishWrite();
        }

        public int IndexOf(ReflectionAreaParam in_item)
        {
            return Parameters.IndexOf(in_item);
        }

        public void Insert(int in_index, ReflectionAreaParam in_item)
        {
            Parameters.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Parameters.RemoveAt(in_index);
        }

        public void Add(ReflectionAreaParam in_item)
        {
            Parameters.Add(in_item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(ReflectionAreaParam in_item)
        {
            return Parameters.Contains(in_item);
        }

        public void CopyTo(ReflectionAreaParam[] in_array, int in_arrayIndex)
        {
            Parameters.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(ReflectionAreaParam in_item)
        {
            return Parameters.Remove(in_item);
        }

        public IEnumerator<ReflectionAreaParam> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReflectionAreaParam
    {
        public float Pitch { get; set; }

        public float UnknownField { get; set; }

        public float Roll { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// The vertices that contain all of the terrain vertices to be reflected.
        /// </summary>
        [JsonIgnore]
        public List<Vector3> Vertices { get; set; } = [];

        public ReflectionAreaParam() { }

        public ReflectionAreaParam(float in_pitch, float in_unkField, float in_roll, float in_y)
        {
            Pitch = in_pitch;
            UnknownField = in_unkField;
            Roll = in_roll;
            Y = in_y;
        }
    }
}
