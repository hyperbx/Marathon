using System.Collections.Generic;

namespace Marathon.IO
{
    public class AssimpFormat
    {
        public const string Collada = "collada";
        public const string X = "x";
        public const string Step = "stp";
        public const string WavefrontObj = "obj";
        public const string WavefrontObjNoMaterials = "objnomtl";
        public const string Stereolithography = "stl";
        public const string StereolithographyBinary = "stlb";
        public const string StanfordPolygonLibrary = "ply";
        public const string StanfordPolygonLibraryBinary = "plyb";
        public const string Autodesk3DStudio = "3ds";
        public const string GLTransmissionFormatV2 = "gltf2";
        public const string GLTransmissionFormatV2Binary = "glb2";
        public const string GLTransmissionFormatV1 = "gltf";
        public const string GLTransmissionFormatV1Binary = "glb";
        public const string AssimpBinary = "assbin";
        public const string AssimpXml = "assxml";
        public const string Extensible3D = "x3d";
        public const string AutodeskFbxBinary = "fbx";
        public const string AutodeskFbxPlaintext = "fbxa";
        public const string Model3DBinary = "m3d";
        public const string Model3DPlaintext = "m3da";
        public const string The3mfFileFormat = "3mf";
        public const string PbrtV4 = "pbrt";
        public const string AssimpJson = "assjson";

        public static readonly Dictionary<string, string> Descriptions = new()
        {
            { Collada, "COLLADA - Digital Asset Exchange Schema" },
            { X, "X Files" },
            { Step, "Step Files" },
            { WavefrontObj, "Wavefront OBJ format" },
            { WavefrontObjNoMaterials, "Wavefront OBJ format without material file" },
            { Stereolithography, "Stereolithography" },
            { StereolithographyBinary, "Stereolithography (binary)" },
            { StanfordPolygonLibrary, "Stanford Polygon Library" },
            { StanfordPolygonLibraryBinary, "Stanford Polygon Library (binary)" },
            { Autodesk3DStudio, "Autodesk 3DS (legacy)" },
            { GLTransmissionFormatV2, "GL Transmission Format v. 2" },
            { GLTransmissionFormatV2Binary, "GL Transmission Format v. 2 (binary)" },
            { GLTransmissionFormatV1, "GL Transmission Format" },
            { GLTransmissionFormatV1Binary, "GL Transmission Format (binary)" },
            { AssimpBinary, "Assimp Binary File" },
            { AssimpXml, "Assimp XML Document" },
            { Extensible3D, "Extensible 3D" },
            { AutodeskFbxBinary, "Autodesk FBX (binary)" },
            { AutodeskFbxPlaintext, "Autodesk FBX (ascii)" },
            { Model3DBinary, "Model 3D (binary)" },
            { Model3DPlaintext, "Model 3D (ascii)" },
            { The3mfFileFormat, "The 3MF-File-Format" },
            { PbrtV4, "pbrt-v4 scene description file" },
            { AssimpJson, "Assimp JSON Document" }
        };
    }
}
