using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TextureFileType : uint
    {
        // Masks
        NND_TEXFTYPE_TEXTYPE_MASK = 255,

        // Types
        NND_TEXFTYPE_GVRTEX = 0,
        NND_TEXFTYPE_SVRTEX = 1,
        NND_TEXFTYPE_XVRTEX = 2,

        // Flags
        NND_TEXFTYPE_NO_FILENAME = 256,
        NND_TEXFTYPE_NO_FILTER = 512,
        NND_TEXFTYPE_LISTGLBIDX = 1024,
        NND_TEXFTYPE_LISTBANK = 2048
    }
}
