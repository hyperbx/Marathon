using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VertexType : uint
    {
        // Masks
        NND_VTXTYPE_PLATFORM_MASK = 0xFFFF,
        NND_VTXTYPE_COMMON_MASK = 0xFF0000,
        NND_VTXTYPE_USER_MASK = 0xFF000000,

        // DirectX types
        NND_VTXTYPE_DX_VERTEXDESC = 1,

        // Common types
        NND_VTXTYPE_COMMON_VTXDESC = 0x10000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET = 0x20000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET_NULL = 0x40000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_OBJECT = 0x80000
    }
}
