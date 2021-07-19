using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Marathon.IO;

namespace Marathon.Formats.Mesh
{
    public class ReflectionArea
    {
        public float Z_Rotation { get; set; }

        public float Length { get; set; }

        public float Y_Rotation { get; set; }

        public float Height { get; set; }

        public List<Vector3> Vertices { get; set; } = new();

        public ReflectionArea() { }

        public ReflectionArea(BinaryReaderEx reader)
        {
            Z_Rotation = reader.ReadSingle();
            Length     = reader.ReadSingle();
            Y_Rotation = reader.ReadSingle();
            Height     = reader.ReadSingle();
        }
    }

    /// <summary>
    /// <para>File base for the RAB format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for reflection bounding boxes.</para>
    /// </summary>
    public class ReflectionZone : FileBase
    {
        public ReflectionZone() { }

        public ReflectionZone(string file)
        {
            switch (Path.GetExtension(file))
            {
                default:
                    Load(file);
                    break;
            }
        }

        public const string Extension = ".rab";

        public List<ReflectionArea> Reflections = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            uint reflectionTableCount  = reader.ReadUInt32(),
                 reflectionTableOffset = reader.ReadUInt32(),
                 entryTableCount       = reader.ReadUInt32(),
                 entryTableOffset      = reader.ReadUInt32();

            reader.JumpTo(reflectionTableOffset, true);

            for (int i = 0; i < reflectionTableCount; i++)
                Reflections.Add(new ReflectionArea(reader));

            reader.JumpTo(entryTableOffset, true);

            for (int i = 0; i < entryTableCount; i++)
            {
                uint vertexCount       = reader.ReadUInt32(),
                     vertexTableOffset = reader.ReadUInt32(),
                     reflectionIndex   = reader.ReadUInt32();

                long position = reader.BaseStream.Position;

                reader.JumpTo(vertexTableOffset, true);

                for (int v = 0; v < vertexCount; v++)
                    Reflections[(int)reflectionIndex].Vertices.Add(reader.ReadVector3());

                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.Write(Reflections.Count);
            writer.AddOffset("reflectionTableOffset");

            writer.Write(Reflections.Count);
            writer.AddOffset("entryTableOffset");

            writer.FillOffset("reflectionTableOffset", true);
            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.Write(Reflections[i].Z_Rotation);
                writer.Write(Reflections[i].Length);
                writer.Write(Reflections[i].Y_Rotation);
                writer.Write(Reflections[i].Height);
            }

            writer.FillOffset("entryTableOffset", true);
            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.Write(Reflections[i].Vertices.Count);
                writer.AddOffset($"vertexTableOffset_{i}");
                writer.Write(i);
            }

            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.FillOffset($"vertexTableOffset_{i}", true);

                foreach (Vector3 vector in Reflections[i].Vertices)
                    writer.Write(vector);
            }

            writer.FinishWrite();
        }
    }
}
