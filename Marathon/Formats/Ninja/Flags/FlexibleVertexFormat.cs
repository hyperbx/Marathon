using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FlexibleVertexFormat : uint
    {
        // Masks
        NND_D3DFVF_POSITION_MASK = 0x400EU,
        NND_D3DFVF_TEXCOUNT_MASK = 0xF00U,

        // Types
        NND_D3DFVF_TEXTUREFORMAT2 = 0,
        NND_D3DFVF_TEXTUREFORMAT3 = 1,
        NND_D3DFVF_TEXTUREFORMAT4 = 2,
        NND_D3DFVF_TEXTUREFORMAT1 = 3,
        NND_D3DFVF_XYZ = 2,
        NND_D3DFVF_XYZRHW = 4,
        NND_D3DFVF_XYZB1 = 6,
        NND_D3DFVF_XYZB2 = 8,
        NND_D3DFVF_XYZB3 = 10,
        NND_D3DFVF_XYZB4 = 12,
        NND_D3DFVF_XYZB5 = 14,
        NND_D3DFVF_XYZW = 0x4002U,
        NND_D3DFVF_NORMAL = 0x010U,
        NND_D3DFVF_PSIZE = 0x020U,
        NND_D3DFVF_DIFFUSE = 0x040U,
        NND_D3DFVF_SPECULAR = 0x080U,
        NND_D3DFVF_TEX0 = 0x000U,
        NND_D3DFVF_TEX1 = 0x100U,
        NND_D3DFVF_TEX2 = 0x200U,
        NND_D3DFVF_TEX3 = 0x300U,
        NND_D3DFVF_TEX4 = 0x400U,
        NND_D3DFVF_TEX5 = 0x500U,
        NND_D3DFVF_TEX6 = 0x600U,
        NND_D3DFVF_TEX7 = 0x700U,
        NND_D3DFVF_TEX8 = 0x800U,
        NND_D3DFVF_LASTBETA_UBYTE4 = 0x1000U,
        NND_D3DFVF_LASTBETA_D3DCOLOR = 0x8000U
    }
}
