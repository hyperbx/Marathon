using System;
using System.IO;
using System.Collections.Generic;
using Marathon.IO.Exceptions;
using Assimp;

namespace Marathon.IO.Formats.Meshes
{
    /// <summary>
    /// <para>File base for the MDL format.</para>
    /// <para>Used for various stage models in the PS3 version of SONIC THE HEDGEHOG.</para>
    /// </summary>
    public class SOXModel : FileBase
    {
        // TODO: clean up this garbage code and understand all the unknowns.

        public SOXModel() { }

        public SOXModel(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".fbx":
                case ".obj":
                    //ImportAssimp(file); // Need to get rid of the need for a donor model to make this work.
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public class SoXVertex
        {
            public Vector3 Position;

            public uint UnknownUInt32_1; // Something to do with base shadow? Number seems almost random.
            public uint UnknownUInt32_2; // Setting this to 0 seemingly made every vertex cast a shadow? Number seems almost random.

            public RGBA VertexColour = new RGBA(); // 4 bytes making up a set of RGBA values.

            public Half XTextureCoordinate;
            public Half YTextureCoordinate;

            public uint UnknownUInt32_3; // Something to do with distant shadows? Number seems almost random.
        }

        public class SoXTexture
        {
            public string Name;

            public uint UnknownUInt32_1; // Always 2 in official files.
            public uint UnknownUInt32_2; // Always 0 in official files.
            public uint UnknownUInt32_3; // Always 0 in official files.
            public uint UnknownUInt32_4; // Always 1 in official files.

            public uint TextureID;   // 0 - 81? Think this might be Texture ID based on patterns.
            public uint TextureType; // Either 0, 1 or 2 in official files. 0 might be Punch Transparency, 1 might be Light Map Stuff? 2 seems to be standard?
        }

        public class SoXMaterial
        {
            public string Name;

            public uint UnknownUInt32_1;  // Always 0 in official files (padding or part of the name?)

            public string EffectFile;

            public uint UnknownUInt32_2;  // Used values: 0, 1, 2, 3, 5, 9, 12, 14, 15, 18

            public string EffectTechnique;

            public uint UnknownUInt32_3;  // Always 0 in official files (padding or part of the technique?)

            public float UnknownFloat_1;  // Holds a value between 0 and 1.
            public float UnknownFloat_2;  // Holds a value between 0 and 1.
            public float UnknownFloat_3;  // Holds a value between 0 and 1.
            public float UnknownFloat_4;  // Always 1 in official files.
            public float UnknownFloat_5;  // Holds a value between 0 and 1.
            public float UnknownFloat_6;  // Holds a value between 0 and 1.
            public float UnknownFloat_7;  // Holds a value between 0 and 1.
            public float UnknownFloat_8;  // Holds a value between 0 and 1.
            public float UnknownFloat_9;  // Holds a value between 0 and 1.
            public float UnknownFloat_10; // Holds a value between 0 and 1.
            public float UnknownFloat_11; // Holds a value between 0 and 1.
            public float UnknownFloat_12; // Always 1 in official files.
            public float UnknownFloat_13; // Either 0, 0.2941177 or 1 in official files.
            public float UnknownFloat_14; // Holds a value between 0 and 1.
            public float UnknownFloat_15; // Always 0 in official files.
            public float UnknownFloat_16; // Always 1 in official files.
            public float UnknownFloat_17; // Various different values up to 4096: 0, 4, 4.287094, 4.924578, 8, 8, 11.31371, 12.12573, 16, 22.62742, 32, 63.99999, 68.5935, 90.50966, 128, 512, 2896.309, 4096

            public string TextureFile1;

            public uint UnknownUInt32_4;  // 0 - 19? But doesn't have 17?
            public string TextureFile2;

            public uint UnknownUInt32_5;  // 0 - 16? But doesn't have 9 or 11?

            public string TextureFile3;

            public uint UnknownUInt32_6;  // Always 0 in official files (padding or part of the name?)

            public string TextureFile4;

            public uint UnknownUInt32_7;  // Always 0 in official files (padding or part of the name?)
            public uint UnknownUInt32_8;  // Either 0, 1 or 4 in official files.
            public uint UnknownUInt32_9;  // Either 0 or 1 in official files (some form of bool instead?) Might be for semi transparency? Just based on the materials that use this at 1?

            public uint BackfaceCull;     // Whether the backs of faces using this model should be culled out.

            public uint UnknownUInt32_11; // Either 0 or 16777216 in official files (maybe actually a bool in a single byte?)
            public uint UnknownUInt32_12; // Either 0 or 128 in official files.
            public uint UnknownUInt32_13; // Either 4 or 7 in official files.
            public uint UnknownUInt32_14; // Either 0 or 3 in official files.

            public float UnknownFloat_18; // 0, 1.742946E-39, 1.834782E-39, 1 or 2 in official files.
        }

        public class SoXMesh
        {
            public string Material;

            public uint UnknownUInt32_1;   // Always 3 in official files.
            public uint UnknownUInt32_2;   // Unknown, number seems almost random.
            public uint UnknownUInt32_3;   // Unknown, number seems almost random.

            public uint FaceTablePosition; // This number divided by three seems to tell the game where to start reading within the Face Table.
            public uint FaceCount;

            public uint UnknownUInt32_4;   // Either 0, 1 or 2 in official files, something to do with Real Time Shadows, not sure why I made this note? Setting this to 2 did semi transparency.
        }

        public const string Signature = "SOX_MDL",
                            Extension = ".mdl";

        public AABB BoundingBox = new AABB();
        public List<uint> UnknownBytes = new List<uint>(); // 784 bytes, not the same between all files, unsure what it does, reading them as uints right now.
        public List<SoXVertex> Vertices = new List<SoXVertex>();
        public List<Face> Faces = new List<Face>();
        public List<SoXTexture> Textures = new List<SoXTexture>();
        public List<SoXMaterial> Materials = new List<SoXMaterial>();
        public List<SoXMesh> Meshes = new List<SoXMesh>();

        public override void Load(Stream fileStream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(fileStream, true);

            // Signature Reading
            string signature = reader.ReadSignature(7);
            if (signature != Signature) throw new InvalidSignatureException(Signature, signature);
            reader.JumpAhead(1); // Jump ahead by 1 to line up with multiples of 4.

            // Header and Offsets
            reader.JumpAhead(4); // Always 0x60 in official files.
            reader.JumpAhead(4); // Always 0x20 in official files.

            uint VerticesOffset  = reader.ReadUInt32(); // Always 0x360 in official files.
            uint VerticiesCount  = reader.ReadUInt32(); // Needs dividng by 32 for my system.
            uint FacesOffset     = reader.ReadUInt32();
            uint FaceCount       = reader.ReadUInt32(); // Needs dividng by 3 for my system.
            uint TexturesOffset  = reader.ReadUInt32();
            uint TexturesCount   = reader.ReadUInt32();
            uint MaterialsOffset = reader.ReadUInt32();
            uint MaterialsCount  = reader.ReadUInt32();
            uint MeshesOffset    = reader.ReadUInt32();
            uint MeshCount       = reader.ReadUInt32();

            // Bounding Box Vector3s.
            BoundingBox.Minimum = reader.ReadVector3();
            BoundingBox.Maximum = reader.ReadVector3();

            // Not sure what these are. There's a lot of data here that is different between different files, nulling them causes the game to die.
            while (reader.BaseStream.Position < 0x360) { UnknownBytes.Add(reader.ReadUInt32()); }

            // Verticies
            reader.JumpTo(VerticesOffset);
            for (int i = 0; i < VerticiesCount / 32; i++)
            {
                SoXVertex vertex             = new SoXVertex();
                vertex.Position           = reader.ReadVector3();
                vertex.UnknownUInt32_1    = reader.ReadUInt32();
                vertex.UnknownUInt32_2    = reader.ReadUInt32();
                vertex.VertexColour.Red   = reader.ReadByte();
                vertex.VertexColour.Green = reader.ReadByte();
                vertex.VertexColour.Blue  = reader.ReadByte();
                vertex.VertexColour.Alpha = reader.ReadByte();
                vertex.XTextureCoordinate = reader.ReadHalf();
                vertex.YTextureCoordinate = reader.ReadHalf();
                vertex.UnknownUInt32_3    = reader.ReadUInt32();

                Vertices.Add(vertex);
            }

            // Faces
            reader.JumpTo(FacesOffset);
            for (int i = 0; i < FaceCount / 3; i++)
            {
                Face face = new Face
                {
                    VertexA = reader.ReadUInt16(),
                    VertexB = reader.ReadUInt16(),
                    VertexC = reader.ReadUInt16()
                };

                Faces.Add(face);
            }

            // Textures
            reader.JumpTo(TexturesOffset);
            for (int i = 0; i < TexturesCount; i++)
            {
                SoXTexture texture = new SoXTexture
                {
                    Name            = new string(reader.ReadChars(76)),
                    UnknownUInt32_1 = reader.ReadUInt32(),
                    UnknownUInt32_2 = reader.ReadUInt32(),
                    UnknownUInt32_3 = reader.ReadUInt32(),
                    UnknownUInt32_4 = reader.ReadUInt32(),
                    TextureID       = reader.ReadUInt32(),
                    TextureType     = reader.ReadUInt32()
                };

                Textures.Add(texture);
            }

            // Materials
            reader.JumpTo(MaterialsOffset);
            for (int i = 0; i < MaterialsCount; i++)
            {
                SoXMaterial material = new SoXMaterial
                {
                    Name             = new string(reader.ReadChars(60)),

                    UnknownUInt32_1  = reader.ReadUInt32(),

                    EffectFile       = new string(reader.ReadChars(60)),

                    UnknownUInt32_2  = reader.ReadUInt32(),

                    EffectTechnique  = new string(reader.ReadChars(60)),

                    UnknownUInt32_3  = reader.ReadUInt32(),
                    UnknownFloat_1   = reader.ReadSingle(),
                    UnknownFloat_2   = reader.ReadSingle(),
                    UnknownFloat_3   = reader.ReadSingle(),
                    UnknownFloat_4   = reader.ReadSingle(),
                    UnknownFloat_5   = reader.ReadSingle(),
                    UnknownFloat_6   = reader.ReadSingle(),
                    UnknownFloat_7   = reader.ReadSingle(),
                    UnknownFloat_8   = reader.ReadSingle(),
                    UnknownFloat_9   = reader.ReadSingle(),
                    UnknownFloat_10  = reader.ReadSingle(),
                    UnknownFloat_11  = reader.ReadSingle(),
                    UnknownFloat_12  = reader.ReadSingle(),
                    UnknownFloat_13  = reader.ReadSingle(),
                    UnknownFloat_14  = reader.ReadSingle(),
                    UnknownFloat_15  = reader.ReadSingle(),
                    UnknownFloat_16  = reader.ReadSingle(),
                    UnknownFloat_17  = reader.ReadSingle(),

                    TextureFile1     = new string(reader.ReadChars(60)),

                    UnknownUInt32_4  = reader.ReadUInt32(),

                    TextureFile2     = new string(reader.ReadChars(60)),

                    UnknownUInt32_5  = reader.ReadUInt32(),

                    TextureFile3     = new string(reader.ReadChars(60)),

                    UnknownUInt32_6  = reader.ReadUInt32(),

                    TextureFile4     = new string(reader.ReadChars(60)),

                    UnknownUInt32_7  = reader.ReadUInt32(),
                    UnknownUInt32_8  = reader.ReadUInt32(),
                    UnknownUInt32_9  = reader.ReadUInt32(),

                    BackfaceCull     = reader.ReadUInt32(),

                    UnknownUInt32_11 = reader.ReadUInt32(),
                    UnknownUInt32_12 = reader.ReadUInt32(),
                    UnknownUInt32_13 = reader.ReadUInt32(),
                    UnknownUInt32_14 = reader.ReadUInt32(),

                    UnknownFloat_18  = reader.ReadSingle()
                };

                Materials.Add(material);
            }

            // Meshes
            reader.JumpTo(MeshesOffset);
            for (int i = 0; i < MeshCount; i++)
            {
                SoXMesh mesh = new SoXMesh
                {
                    Material          = new string(reader.ReadChars(68)),

                    UnknownUInt32_1   = reader.ReadUInt32(),
                    UnknownUInt32_2   = reader.ReadUInt32(),
                    UnknownUInt32_3   = reader.ReadUInt32(),

                    FaceTablePosition = reader.ReadUInt32(),
                    FaceCount         = reader.ReadUInt32(),

                    UnknownUInt32_4   = reader.ReadUInt32()
                };

                Meshes.Add(mesh);
            }
        }

        public unsafe override void Save(Stream fileStream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(fileStream, true);

            // Header and Offsets
            writer.WriteSignature(Signature);
            writer.WriteNulls(1);
            writer.Write(0x60);
            writer.Write(0x20);
            writer.AddOffset("VerticiesOffset");
            writer.Write(Vertices.Count * 32);
            writer.AddOffset("FacesOffset");
            writer.Write(Faces.Count * 3);
            writer.AddOffset("TexturesOffset");
            writer.Write(Textures.Count);
            writer.AddOffset("MaterialsOffset");
            writer.Write(Materials.Count);
            writer.AddOffset("MeshesOffset");
            writer.Write(Meshes.Count);
            writer.Write(BoundingBox.Minimum);
            writer.Write(BoundingBox.Maximum);

            // Unknown Bytes
            for (int i = 0; i < UnknownBytes.Count; i++) { writer.Write(UnknownBytes[i]); }

            // Verticies
            writer.FillInOffset("VerticiesOffset");
            for (int i = 0; i < Vertices.Count; i++)
            {
                writer.Write(Vertices[i].Position);

                writer.Write(Vertices[i].UnknownUInt32_1);
                writer.Write(Vertices[i].UnknownUInt32_2);

                writer.Write(Vertices[i].VertexColour.Red);
                writer.Write(Vertices[i].VertexColour.Green);
                writer.Write(Vertices[i].VertexColour.Blue);
                writer.Write(Vertices[i].VertexColour.Alpha);

                // Really hacky code to get around the lack of Half support, should be able to clean this up when Hyper commits Half support.
                // TODO: use Half type.
                var halfHackValue = Vertices[i].XTextureCoordinate;
                var halfHack = *((ushort*)&halfHackValue);
                writer.Write(halfHack);
                halfHackValue = Vertices[i].YTextureCoordinate;
                halfHack = *((ushort*)&halfHackValue);
                writer.Write(halfHack);

                writer.Write(Vertices[i].UnknownUInt32_3);
            }

            // Faces
            writer.FillInOffset("FacesOffset");
            for (int i = 0; i < Faces.Count; i++)
            {
                writer.Write(Faces[i].VertexA);
                writer.Write(Faces[i].VertexB);
                writer.Write(Faces[i].VertexC);
            }

            writer.FixPadding();

            // Textures
            writer.FillInOffset("TexturesOffset");
            for (int i = 0; i < Textures.Count; i++)
            {
                writer.Write(Textures[i].Name.PadRight(76, '\0'));
                writer.Write(Textures[i].UnknownUInt32_1);
                writer.Write(Textures[i].UnknownUInt32_2);
                writer.Write(Textures[i].UnknownUInt32_3);
                writer.Write(Textures[i].UnknownUInt32_4);
                writer.Write(Textures[i].TextureID);
                writer.Write(Textures[i].TextureType);
            }

            // Materials
            writer.FillInOffset("MaterialsOffset");
            for (int i = 0; i < Materials.Count; i++)
            {
                writer.Write(Materials[i].Name.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_1);

                writer.Write(Materials[i].EffectFile.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_2);

                writer.Write(Materials[i].EffectTechnique.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_3);

                writer.Write(Materials[i].UnknownFloat_1);
                writer.Write(Materials[i].UnknownFloat_2);
                writer.Write(Materials[i].UnknownFloat_3);
                writer.Write(Materials[i].UnknownFloat_4);
                writer.Write(Materials[i].UnknownFloat_5);
                writer.Write(Materials[i].UnknownFloat_6);
                writer.Write(Materials[i].UnknownFloat_7);
                writer.Write(Materials[i].UnknownFloat_8);
                writer.Write(Materials[i].UnknownFloat_9);
                writer.Write(Materials[i].UnknownFloat_10);
                writer.Write(Materials[i].UnknownFloat_11);
                writer.Write(Materials[i].UnknownFloat_12);
                writer.Write(Materials[i].UnknownFloat_13);
                writer.Write(Materials[i].UnknownFloat_14);
                writer.Write(Materials[i].UnknownFloat_15);
                writer.Write(Materials[i].UnknownFloat_16);
                writer.Write(Materials[i].UnknownFloat_17);

                writer.Write(Materials[i].TextureFile1.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_4);

                writer.Write(Materials[i].TextureFile2.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_5);

                writer.Write(Materials[i].TextureFile3.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_6);

                writer.Write(Materials[i].TextureFile4.PadRight(60, '\0'));

                writer.Write(Materials[i].UnknownUInt32_7);
                writer.Write(Materials[i].UnknownUInt32_8);
                writer.Write(Materials[i].UnknownUInt32_9);

                writer.Write(Materials[i].BackfaceCull);

                writer.Write(Materials[i].UnknownUInt32_11);
                writer.Write(Materials[i].UnknownUInt32_12);
                writer.Write(Materials[i].UnknownUInt32_13);
                writer.Write(Materials[i].UnknownUInt32_14);

                writer.Write(Materials[i].UnknownFloat_18);
            }

            // Meshes
            writer.FillInOffset("MeshesOffset");
            for (int i = 0; i < Meshes.Count; i++)
            {
                writer.Write(Meshes[i].Material.PadRight(68, '\0'));

                writer.Write(Meshes[i].UnknownUInt32_1);
                writer.Write(Meshes[i].UnknownUInt32_2);
                writer.Write(Meshes[i].UnknownUInt32_3);

                writer.Write(Meshes[i].FaceTablePosition);
                writer.Write(Meshes[i].FaceCount);

                writer.Write(Meshes[i].UnknownUInt32_4);
            }
        }

        // TODO: Make this actually, oh I don't know... WORK?
        public void ExportOBJ(string filepath)
        {
            // OBJ.
            using (StreamWriter obj = new StreamWriter(filepath))
            {
                obj.WriteLine($"mtllib {Path.GetFileNameWithoutExtension(filepath)}.mtl\n");

                // Verticies
                foreach (SoXVertex vertex in Vertices)
                {
                    obj.WriteLine($"v {vertex.Position.X} {vertex.Position.Y} {vertex.Position.Z}");
                }

                obj.WriteLine();

                // Texture Coordinates
                foreach (SoXVertex vertex in Vertices)
                {
                    obj.WriteLine($"vt {vertex.XTextureCoordinate} {vertex.YTextureCoordinate}");
                }

                obj.WriteLine();

                // Vertex Colours
                foreach (SoXVertex vertex in Vertices)
                {
                    obj.WriteLine($"vc {vertex.VertexColour.Red} {vertex.VertexColour.Green} {vertex.VertexColour.Blue} {vertex.VertexColour.Alpha}");
                }

                obj.WriteLine();

                // Objects
                for (int i = 0; i < Meshes.Count; i++)
                {
                    obj.WriteLine($"g {Path.GetFileNameWithoutExtension(filepath)}_Mesh{i}");
                    obj.WriteLine($"usemtl {Meshes[i].Material.Replace("\0", "")}");

                    for (int f = 0; f < Meshes[i].FaceCount; f++)
                    {
                        // Jesus Christ
                        obj.WriteLine($"f {Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexA + 1}/{Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexA + 1} {Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexB + 1}/{Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexB + 1} {Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexC + 1}/{Faces[f + (int)(Meshes[i].FaceTablePosition / 3)].VertexC + 1}");
                    }
                }
            }

            // MTL.
            using (StreamWriter mtl = new StreamWriter($"{Path.GetDirectoryName(filepath)}\\{Path.GetFileNameWithoutExtension(filepath)}.mtl"))
            {
                foreach (SoXMaterial material in Materials)
                {
                    mtl.WriteLine($"newmtl {material.Name.Replace("\0", "")}");
                    mtl.WriteLine("Ka 0.2 0.2 0.2");
                    mtl.WriteLine("Kd 0.8 0.8 0.8");
                    mtl.WriteLine("Ks 0 0 0");
                    mtl.WriteLine("Ns 10");
                    mtl.WriteLine("d 1.0");
                    mtl.WriteLine("illum 4");
                    mtl.WriteLine($"map_Kd {material.TextureFile1.Replace("\0", "")}\n");
                }
            }
        }

        public void ImportAssimp(string srcFile, string donorFile)
        {
            // TODO: Test other file formats, currently only know about FBX support. Cleanup Code.
            // Setup Assimp scene and load the srcFile.
            AssimpContext assimpImporter = new AssimpContext();
            Scene assimpModel = assimpImporter.ImportFile(srcFile, PostProcessSteps.PreTransformVertices);

            // Setup and load the donor model from donorFile.
            SOXModel donorMdl = new SOXModel();
            donorMdl.Load(donorFile);

            // Bounding Box (TODO: Unhardcode these values in someway?)
            BoundingBox.Minimum = new Vector3(-999999f, -999999f, -999999f);
            BoundingBox.Maximum = new Vector3(999999f, 999999f, 999999f);

            // Unknown Bytes (TODO: Research these so a donor model isn't needed.)
            UnknownBytes = donorMdl.UnknownBytes;

            for (int m = 0; m < assimpModel.MeshCount; m++)
            {
                // Vertices
                // TODO: soft code unknowns so a donor model isn't needed.
                int previousVertexCount = Vertices.Count;

                for (int i = 0; i < assimpModel.Meshes[m].Vertices.Count; i++)
                {
                    SoXVertex vertex = new SoXVertex();

                    vertex.Position = new Vector3(assimpModel.Meshes[m].Vertices[i].X, assimpModel.Meshes[m].Vertices[i].Y, assimpModel.Meshes[m].Vertices[i].Z);

                    if (assimpModel.Meshes[m].VertexColorChannelCount != 0)
                    {
                        vertex.VertexColour.Red   = Convert.ToByte(assimpModel.Meshes[m].VertexColorChannels[0][i].R * 255);
                        vertex.VertexColour.Green = Convert.ToByte(assimpModel.Meshes[m].VertexColorChannels[0][i].G * 255);
                        vertex.VertexColour.Blue  = Convert.ToByte(assimpModel.Meshes[m].VertexColorChannels[0][i].B * 255);
                        vertex.VertexColour.Alpha = Convert.ToByte(assimpModel.Meshes[m].VertexColorChannels[0][i].A * 255);
                    }
                    else
                    {
                        vertex.VertexColour.Red   = Convert.ToByte(255);
                        vertex.VertexColour.Green = Convert.ToByte(255);
                        vertex.VertexColour.Blue  = Convert.ToByte(255);
                        vertex.VertexColour.Alpha = Convert.ToByte(255);
                    }

                    vertex.XTextureCoordinate = (Half)assimpModel.Meshes[m].TextureCoordinateChannels[0][i].X;
                    vertex.YTextureCoordinate = (Half)(-assimpModel.Meshes[m].TextureCoordinateChannels[0][i].Y);

                    vertex.UnknownUInt32_1    = donorMdl.Vertices[0].UnknownUInt32_1;
                    vertex.UnknownUInt32_2    = donorMdl.Vertices[0].UnknownUInt32_2;
                    vertex.UnknownUInt32_3    = donorMdl.Vertices[0].UnknownUInt32_3;

                    Vertices.Add(vertex);
                }

                // Meshes
                // TODO: Research and soft code unknowns.
                SoXMesh mesh = new SoXMesh();

                if (assimpModel.Materials[m].Name.Contains("@"))
                {
                    mesh.Material = assimpModel.Materials[m].Name.Remove(assimpModel.Materials[m].Name.IndexOf('@'));
                }
                else
                {
                    mesh.Material = assimpModel.Materials[m].Name;
                }

                mesh.UnknownUInt32_1 = 3u;
                mesh.UnknownUInt32_2 = 0x00000000;
                mesh.UnknownUInt32_3 = 0x00000B75;

                mesh.FaceTablePosition = Convert.ToUInt32(Faces.Count * 3);
                mesh.FaceCount = Convert.ToUInt32(assimpModel.Meshes[m].FaceCount);

                mesh.UnknownUInt32_4 = 0;

                Meshes.Add(mesh);

                // Faces.
                Face face;

                foreach (Assimp.Face assimpFace in assimpModel.Meshes[m].Faces)
                {
                    face = new Face
                    {
                        VertexA = (ushort)(assimpFace.Indices[0] + previousVertexCount),
                        VertexB = (ushort)(assimpFace.Indices[1] + previousVertexCount),
                        VertexC = (ushort)(assimpFace.Indices[2] + previousVertexCount)
                    };

                    Faces.Add(face);
                }
            }

            for (int m = 0; m < assimpModel.MaterialCount; m++)
            {
                // Textures
                // TODO: Research and softcode unknowns.
                SoXTexture texture = new SoXTexture
                {
                    Name = assimpModel.Materials[m].TextureDiffuse.FilePath.Substring(assimpModel.Materials[m].TextureDiffuse.FilePath.LastIndexOf('\\') + 1),

                    UnknownUInt32_1 = 2,
                    UnknownUInt32_2 = 0,
                    UnknownUInt32_3 = 0,
                    UnknownUInt32_4 = 1,

                    TextureID = Convert.ToUInt32(m),
                    TextureType = 2
                };

                Textures.Add(texture);

                // Materials
                // TODO: Research and soft code unknown values so a donor model isn't needed.
                SoXMaterial material = new SoXMaterial();

                material.EffectFile = "Billboard03.fx";
                material.EffectTechnique = "Billboard03";

                if (assimpModel.Materials[m].Name.Contains("@"))
                {
                    material.Name = assimpModel.Materials[m].Name.Remove(assimpModel.Materials[m].Name.IndexOf('@'));
                }
                else
                {
                    material.Name = assimpModel.Materials[m].Name;
                }

                material.TextureFile1 = assimpModel.Materials[m].TextureDiffuse.FilePath.Substring(assimpModel.Materials[m].TextureDiffuse.FilePath.LastIndexOf('\\') + 1);
                material.TextureFile2 = "";
                material.TextureFile3 = "";
                material.TextureFile4 = "";

                //material.UnknownFloat_4 = material.UnknownFloat_12 = material.UnknownFloat_16 = 1f;
                material.UnknownFloat_1 = donorMdl.Materials[0].UnknownFloat_1;
                material.UnknownFloat_2 = donorMdl.Materials[0].UnknownFloat_2;
                material.UnknownFloat_3 = donorMdl.Materials[0].UnknownFloat_3;
                material.UnknownFloat_4 = donorMdl.Materials[0].UnknownFloat_4;
                material.UnknownFloat_5 = donorMdl.Materials[0].UnknownFloat_5;
                material.UnknownFloat_6 = donorMdl.Materials[0].UnknownFloat_6;
                material.UnknownFloat_7 = donorMdl.Materials[0].UnknownFloat_7;
                material.UnknownFloat_8 = donorMdl.Materials[0].UnknownFloat_8;
                material.UnknownFloat_9 = donorMdl.Materials[0].UnknownFloat_9;
                material.UnknownFloat_10 = donorMdl.Materials[0].UnknownFloat_10;
                material.UnknownFloat_11 = donorMdl.Materials[0].UnknownFloat_11;
                material.UnknownFloat_12 = donorMdl.Materials[0].UnknownFloat_12;
                material.UnknownFloat_13 = donorMdl.Materials[0].UnknownFloat_13;
                material.UnknownFloat_14 = donorMdl.Materials[0].UnknownFloat_14;
                material.UnknownFloat_15 = donorMdl.Materials[0].UnknownFloat_15;
                material.UnknownFloat_16 = donorMdl.Materials[0].UnknownFloat_16;
                material.UnknownFloat_17 = donorMdl.Materials[0].UnknownFloat_17;
                material.UnknownFloat_18 = donorMdl.Materials[0].UnknownFloat_18;

                material.UnknownUInt32_1 = donorMdl.Materials[0].UnknownUInt32_1;
                material.UnknownUInt32_2 = donorMdl.Materials[0].UnknownUInt32_2;
                material.UnknownUInt32_3 = donorMdl.Materials[0].UnknownUInt32_3;
                material.UnknownUInt32_4 = donorMdl.Materials[0].UnknownUInt32_4;
                material.UnknownUInt32_5 = donorMdl.Materials[0].UnknownUInt32_5;
                material.UnknownUInt32_6 = donorMdl.Materials[0].UnknownUInt32_6;
                material.UnknownUInt32_7 = donorMdl.Materials[0].UnknownUInt32_7;
                material.UnknownUInt32_8 = donorMdl.Materials[0].UnknownUInt32_8;
                material.UnknownUInt32_9 = donorMdl.Materials[0].UnknownUInt32_9;

                material.BackfaceCull = donorMdl.Materials[0].BackfaceCull;

                material.UnknownUInt32_11 = donorMdl.Materials[0].UnknownUInt32_11;
                material.UnknownUInt32_12 = donorMdl.Materials[0].UnknownUInt32_12;
                material.UnknownUInt32_13 = donorMdl.Materials[0].UnknownUInt32_13;
                material.UnknownUInt32_14 = donorMdl.Materials[0].UnknownUInt32_14;

                Materials.Add(material);
            }
        }
    }
}