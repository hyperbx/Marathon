using Marathon.IO;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format research attribution: Hyper

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for *.rab files; used for reflection zones.
    /// </summary>
    public class ReflectionArea : FileBase
    {
        private const string _extension = ".rab"; // "Reflection Area Binary"

        public ReflectionArea() { }

        public ReflectionArea(string in_path) : base(in_path) { }

        public List<ReflectionAreaParam> Parameters { get; set; } = [];

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

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
                    UnknownField1 = reader.Read<float>(),
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
            var writer = new BINAWriter(in_stream);

            writer.Write(Parameters.Count);
            writer.CreateNamedField("ReflectionTableOffset");

            writer.Write(Parameters.Count);
            writer.CreateNamedField("EntryTableOffset");

            writer.WriteNamedField("ReflectionTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.Write(Parameters[i].Pitch);
                writer.Write(Parameters[i].UnknownField1);
                writer.Write(Parameters[i].Roll);
                writer.Write(Parameters[i].Y);
            }

            writer.WriteNamedField("EntryTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.Write(Parameters[i].Vertices.Count);
                writer.CreateNamedField($"VertexTableOffset{i}");
                writer.Write(i);
            }

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.WriteNamedField($"VertexTableOffset{i}", (uint)writer.Position - BINAHeader.Size);

                foreach (var vector in Parameters[i].Vertices)
                    writer.Write(vector);
            }

            writer.FinishWrite();
        }
    }

    public class ReflectionAreaParam
    {
        public float Pitch { get; set; }

        public float UnknownField1 { get; set; }

        public float Roll { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// The vertices that contain all of the terrain vertices to be reflected.
        /// </summary>
        [JsonIgnore]
        public List<Vector3> Vertices { get; set; } = [];

        public ReflectionAreaParam() { }

        public ReflectionAreaParam(float in_pitch, float in_unkField1, float in_roll, float in_y)
        {
            Pitch = in_pitch;
            UnknownField1 = in_unkField1;
            Roll = in_roll;
            Y = in_y;
        }
    }
}
