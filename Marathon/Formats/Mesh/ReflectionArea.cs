using Marathon.IO;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using Newtonsoft.Json;
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
    [FileType("Reflection Area", "Mesh", _extension)]
    public class ReflectionArea : FileBase
    {
        private const string _extension = ".rab"; // "Reflection Area Binary"

        public List<ReflectionAreaParam> Parameters { get; set; } = [];

        public override string Extension => _extension;

        public ReflectionAreaParam this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public ReflectionArea() { }

        public ReflectionArea(string in_path) : base(in_path) { }

        public ReflectionArea(Stream in_stream) : base(in_stream) { }

        public ReflectionArea(IFile in_file) : base(in_file) { }

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
                Parameters.Add(reader.ReadObjectEx<ReflectionAreaParam>());

            reader.JumpTo(BINAHeader.Size + entryTableOffset);

            for (int i = 0; i < entryTableCount; i++)
            {
                var vertexCount = reader.Read<uint>();
                var vertexTableOffset = reader.Read<uint>();
                var reflectionIndex = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + vertexTableOffset, () =>
                {
                    for (int j = 0; j < vertexCount; j++)
                        Parameters[(int)reflectionIndex].Vertices.Add(reader.Read<Vector3>());
                });
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Write(Parameters.Count);
            var reflectionTableOffset = writer.Reserve<uint>();

            writer.Write(Parameters.Count);
            var entryTableOffset = writer.Reserve<uint>();

            writer.WriteReserved(reflectionTableOffset, (uint)writer.Position - BINAHeader.Size);

            foreach (var parameter in Parameters)
                writer.WriteObjectEx(parameter);

            writer.WriteReserved(entryTableOffset, (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.Write(Parameters[i].Vertices.Count);
                writer.Reserve<uint>($"VertexTableOffset{i}");
                writer.Write(i);
            }

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.WriteReserved($"VertexTableOffset{i}", (uint)writer.Position - BINAHeader.Size);

                foreach (var vertex in Parameters[i].Vertices)
                    writer.Write(vertex);
            }

            writer.FinishWrite();
        }
    }

    public class ReflectionAreaParam : IBinarySerializableEx
    {
        public float Yaw { get; set; }

        public float Roll { get; set; }

        public float Pitch { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// The vertices that contain all of the terrain vertices to be reflected.
        /// </summary>
        [JsonIgnore]
        public List<Vector3> Vertices { get; set; } = [];

        public ReflectionAreaParam() { }

        public ReflectionAreaParam(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public ReflectionAreaParam(float in_yaw, float in_roll, float in_pitch, float in_y)
        {
            Yaw = in_yaw;
            Roll = in_roll;
            Pitch = in_pitch;
            Y = in_y;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Yaw = in_reader.Read<float>();
            Roll = in_reader.Read<float>();
            Pitch = in_reader.Read<float>();
            Y = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Yaw);
            in_writer.Write(Roll);
            in_writer.Write(Pitch);
            in_writer.Write(Y);
        }
    }
}
