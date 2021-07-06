using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Marathon.IO;

namespace Marathon.Formats.Mesh
{
    public class CollisionFace
    {
        public ushort VertexA { get; set; }

        public ushort VertexB { get; set; }

        public ushort VertexC { get; set; }

        public uint Flags { get; set; }

        public override string ToString() => $"<{VertexA}, {VertexB}, {VertexC}> @ 0x{Flags:X}";
    }

    public class Collision : FileBase
    {
        public Collision() { }

        public Collision(string file)
        {
            switch (Path.GetExtension(file))
            {
                default:
                    Load(file);
                    break;
            }
        }

        public const string Extension = ".bin";

        public class FormatData
        {
            public List<Vector3> Vertices = new();
            public List<CollisionFace> Faces = new();
        }

        public FormatData Data = new();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new(fileStream);

            uint unknownUInt32_1   = reader.ReadUInt32(); // TODO: Unknown, is apparently an offset?
            uint moppCodeOffset    = reader.ReadUInt32(); // Offset to this file's Havok MOPP Code, which '06 doesn't seem to use?
            uint vertexTableOffset = reader.ReadUInt32(); // Offset to this file's Vertex Table.
            uint faceTableOffset   = reader.ReadUInt32(); // Offset to this file's Face Table.

            reader.JumpTo(vertexTableOffset, true); // Should already be here but just to be safe.
            uint vertexCount = reader.ReadUInt32(); // Get the amount of Vertices in this file.

            // Read the Vertices.
            for (int i = 0; i < vertexCount; i++)
                Data.Vertices.Add(reader.ReadVector3());

            reader.JumpTo(faceTableOffset, true); // Should already be here but just to be safe.
            uint faceCount = reader.ReadUInt32(); // Get the amount of Faces in this file.

            // Read the Faces.
            for (int i = 0; i < faceCount; i++)
            {
                CollisionFace face = new()
                {
                    VertexA = reader.ReadUInt16(),
                    VertexB = reader.ReadUInt16(),
                    VertexC = reader.ReadUInt16()
                };

                reader.FixPadding();

                face.Flags = reader.ReadUInt32(); // TODO: Experiment with flags to see what they all do.

                Data.Faces.Add(face);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            writer.AddOffset("unknownUInt32_1");

            writer.WriteNulls(4); // Ignore MOPP code offset.

            writer.FillOffset("unknownUInt32_1", true);

            writer.AddOffset("vertexCountOffset");
            writer.AddOffset("faceCountOffset");

            writer.FillOffset("vertexCountOffset", true);
            writer.Write(Data.Vertices.Count);

            for (int i = 0; i < Data.Vertices.Count; i++)
                writer.Write(Data.Vertices[i]);

            writer.FillOffset("faceCountOffset", true);
            writer.Write(Data.Faces.Count);

            for (int i = 0; i < Data.Faces.Count; i++)
            {
                writer.Write(Data.Faces[i].VertexA);
                writer.Write(Data.Faces[i].VertexB);
                writer.Write(Data.Faces[i].VertexC);
                writer.FixPadding();
                writer.Write(Data.Faces[i].Flags);
            }

            writer.FinishWrite();
        }
    }
}
