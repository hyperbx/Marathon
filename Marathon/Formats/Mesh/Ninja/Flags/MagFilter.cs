using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MagFilter : ushort
    {
        NND_MAG_NEAREST = 0,
        NND_MAG_LINEAR = 1,
        NND_MAG_ANISOTROPIC = 2
    }
}
