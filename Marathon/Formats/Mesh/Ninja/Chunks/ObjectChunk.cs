using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class ObjectChunk : IChunk
    {
        public const string ID = "NXOB";

        public string ChunkID { get; set; } = ID;

        public int Version { get; set; }

        public Vector3 Centre { get; set; }

        public float Radius { get; set; }

        public List<Material> Materials { get; set; } = [];

        public List<VertexList> VertexLists { get; set; } = [];

        public List<PrimitiveList> PrimitiveLists { get; set; } = [];

        public uint MaxNodeDepth { get; set; }

        public List<Node> Nodes { get; set; } = [];

        public uint MatrixIndexCount { get; set; }

        public List<SubObject> SubObjects { get; set; } = [];

        public uint TextureCount { get; set; }

        public uint? Type { get; set; }

        public Vector3? BoundingBox { get; set; }

        public ObjectChunk() { }

        public ObjectChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(ID))
                throw new InvalidSignatureException(ID, header.ID);

            var verticesLength = in_reader.Read<uint>();
            var verticesOffset = in_reader.Read<uint>();

            in_reader.Align(16);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            Centre = in_reader.Read<Vector3>();
            Radius = in_reader.Read<float>();
            var materialCount = in_reader.Read<uint>();
            var materialOffset = in_reader.Read<uint>();
            var vertexListCount = in_reader.Read<uint>();
            var vertexListOffset = in_reader.Read<uint>();
            var primitiveListCount = in_reader.Read<uint>();
            var primitiveListOffset = in_reader.Read<uint>();
            var nodeCount = in_reader.Read<uint>();
            MaxNodeDepth = in_reader.Read<uint>();
            var nodeOffset = in_reader.Read<uint>();
            MatrixIndexCount = in_reader.Read<uint>();
            var subObjectCount = in_reader.Read<uint>();
            var subObjectOffset = in_reader.Read<uint>();
            TextureCount = in_reader.Read<uint>();

            if (header.Version == 3)
            {
                Type = in_reader.Read<uint>();
                var version = in_reader.Read<int>();
                BoundingBox = in_reader.Read<Vector3>();
            }

            in_reader.JumpTo(InfoChunk.Size + materialOffset);

            for (int i = 0; i < materialCount; i++)
            {
                in_reader.JumpTo(InfoChunk.Size + materialOffset + (Material.InfoSize * i));
                Materials.Add(new Material(in_reader));
            }

            in_reader.JumpTo(InfoChunk.Size + vertexListOffset);

            for (int i = 0; i < vertexListCount; i++)
            {
                in_reader.JumpTo(InfoChunk.Size + vertexListOffset + (VertexList.InfoSize * i));
                VertexLists.Add(new VertexList(in_reader));
            }

            in_reader.JumpTo(InfoChunk.Size + primitiveListOffset);

            for (int i = 0; i < primitiveListCount; i++)
            {
                in_reader.JumpTo(InfoChunk.Size + primitiveListOffset + (PrimitiveList.InfoSize * i));
                PrimitiveLists.Add(new PrimitiveList(in_reader));
            }

            in_reader.JumpTo(InfoChunk.Size + nodeOffset);

            for (int i = 0; i < nodeCount; i++)
                Nodes.Add(new Node(in_reader));

            in_reader.JumpTo(InfoChunk.Size + subObjectOffset);

            for (int i = 0; i < subObjectCount; i++)
            {
                in_reader.JumpTo(InfoChunk.Size + subObjectOffset + (SubObject.InfoSize * i));
                SubObjects.Add(new SubObject(in_reader));
            }    
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, ID, Version);
            var verticesLength = in_writer.Reserve<uint>();
            var verticesOffset = in_writer.Reserve<uint>();

            in_writer.Align(16);

            var nodePos = (uint)in_writer.Position;

            foreach (var node in Nodes)
                node.Write(in_writer);

            var materialColours = new List<MaterialColour>();
            var materialColourOffsets = new List<uint>();

            foreach (var material in Materials)
            {
                // NOTE: Ninja optimises repeat material colours
                // by pointing other materials to the same one if
                // they're identical.
                if (materialColours.Contains(material.Colour))
                {
                    materialColourOffsets.Add(materialColourOffsets[materialColours.IndexOf(material.Colour)]);
                }
                else
                {
                    materialColourOffsets.Add((uint)in_writer.Position);
                    material.Colour.Write(in_writer);
                }

                materialColours.Add(material.Colour);
            }

            var materialLogics = new List<MaterialLogic>();
            var materialLogicOffsets = new List<uint>();

            foreach (var material in Materials)
            {
                // NOTE: Ninja optimises repeat material logic
                // by pointing other materials to the same one if
                // they're identical.
                if (materialLogics.Contains(material.Logic))
                {
                    materialLogicOffsets.Add(materialLogicOffsets[materialLogics.IndexOf(material.Logic)]);
                }
                else
                {
                    materialLogicOffsets.Add((uint)in_writer.Position);
                    material.Logic.Write(in_writer);
                }

                materialLogics.Add(material.Logic);
            }

            var materialTextureMaps = new List<MaterialTextureMap>();
            var materialTextureMapOffsets = new List<uint>();

            foreach (var material in Materials)
            {
                // NOTE: Ninja optimises repeat material texture maps
                // by pointing other materials to the same one if
                // they're identical.
                if (materialTextureMaps.Contains(material.TextureMap))
                {
                    materialTextureMapOffsets.Add(materialTextureMapOffsets[materialTextureMaps.IndexOf(material.TextureMap)]);
                }
                else
                {
                    materialTextureMapOffsets.Add((uint)in_writer.Position);
                    material.TextureMap.Write(in_writer);
                }

                materialTextureMaps.Add(material.TextureMap);
            }

            for (int i = 0; i < Materials.Count; i++)
            {
                Materials[i].WriteInfo
                (
                    in_writer,
                    materialColourOffsets[i],
                    materialLogicOffsets[i],
                    materialTextureMapOffsets[i]
                );
            }

            var materialsPos = (uint)in_writer.Position;

            for (int i = 0; i < Materials.Count; i++)
                Materials[i].WritePointer(in_writer);

            foreach (var vertexList in VertexLists)
                vertexList.Write(in_writer);

            var vertexListsPos = (uint)in_writer.Position;

            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WritePointer(in_writer);

            foreach (var primitiveList in PrimitiveLists)
            {
                primitiveList.WriteStripIndices(in_writer);
                primitiveList.WriteIndexIndices(in_writer);
            }

            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].WriteInfo(in_writer);

            var primitiveListsPos = (uint)in_writer.Position;

            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].WritePointer(in_writer);

            foreach (var subObject in SubObjects)
            {
                subObject.WriteMeshSets(in_writer);
                subObject.WriteTextureIndices(in_writer);
            }

            var subObjectsPos = (uint)in_writer.Position;

            for (int i = 0; i < SubObjects.Count; i++)
                SubObjects[i].WriteInfo(in_writer);

            var dataOffset = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Centre);
            in_writer.Write(Radius);

            in_writer.Write(Materials.Count);
            var materialInfoOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(materialInfoOffset, materialsPos - InfoChunk.Size, false);

            in_writer.Write(VertexLists.Count);
            var vertexListInfoOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(vertexListInfoOffset, vertexListsPos - InfoChunk.Size, false);

            in_writer.Write(PrimitiveLists.Count);
            var primitiveListInfoOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(primitiveListInfoOffset, primitiveListsPos - InfoChunk.Size, false);

            in_writer.Write(Nodes.Count);
            in_writer.Write(MaxNodeDepth);
            var nodeOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(nodeOffset, nodePos - InfoChunk.Size, false);

            in_writer.Write(MatrixIndexCount);

            in_writer.Write(SubObjects.Count);
            var subObjectInfoOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(subObjectInfoOffset, subObjectsPos - InfoChunk.Size, false);

            in_writer.Write(TextureCount);

            if (Type.HasValue && BoundingBox.HasValue)
            {
                Version = 3;

                in_writer.Write(Type.Value);
                in_writer.Write(Version);
                in_writer.Write(BoundingBox.Value);
            }

            in_writer.WriteReserved(verticesOffset, (uint)(in_writer.Position - InfoChunk.Size), false);

            var vertexTableStart = (uint)in_writer.Position;

            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WriteVertices(in_writer);

            var vertexTableLength = (uint)(in_writer.Position - vertexTableStart);

            in_writer.Align(16);

            var chunkEnd = (uint)in_writer.Position;
            var chunkSize = chunkEnd - header.GetChunkStart();

            in_writer.WriteReserved(verticesLength, vertexTableLength);

            header.FinishWrite(in_writer, chunkSize, dataOffset, Version);
        }
    }
}
