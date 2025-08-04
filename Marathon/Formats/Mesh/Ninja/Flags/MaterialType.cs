using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MaterialType : uint
    {
        NND_MATTYPE_TEXTURE = 1,
        NND_MATTYPE_TEXTURE2 = 2,
        NND_MATTYPE_TEXTURE3 = 3,
        NND_MATTYPE_TEXTURE4 = 4,
        NND_MATTYPE_TEXMATTYPE2 = 16,
    }
}
