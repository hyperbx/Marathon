using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PrimitiveType : uint
    {
        // DirectX types
        NND_PRIMTYPE_DX_STRIPLIST = 1
    }
}
