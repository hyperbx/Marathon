using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MinFilter : ushort
    {
        NND_MIN_NEAREST = 0,
        NND_MIN_LINEAR = 1,
        NND_MIN_NEAREST_MIPMAP_NEAREST = 2,
        NND_MIN_NEAREST_MIPMAP_LINEAR = 3,
        NND_MIN_LINEAR_MIPMAP_NEAREST = 4,
        NND_MIN_LINEAR_MIPMAP_LINEAR = 5,
        NND_MIN_ANISOTROPIC = 6,
        NND_MIN_ANISOTROPIC2 = 6,
        NND_MIN_ANISOTROPIC_MIPMAP_NEAREST = 7,
        NND_MIN_ANISOTROPIC2_MIPMAP_NEAREST = 7,
        NND_MIN_ANISOTROPIC_MIPMAP_LINEAR = 8,
        NND_MIN_ANISOTROPIC2_MIPMAP_LINEAR = 8,
        NND_MIN_ANISOTROPIC4 = 9,
        NND_MIN_ANISOTROPIC4_MIPMAP_NEAREST = 10,
        NND_MIN_ANISOTROPIC4_MIPMAP_LINEAR = 11,
        NND_MIN_ANISOTROPIC8 = 12,
        NND_MIN_ANISOTROPIC8_MIPMAP_NEAREST = 13,
        NND_MIN_ANISOTROPIC8_MIPMAP_LINEAR = 14
    }
}
