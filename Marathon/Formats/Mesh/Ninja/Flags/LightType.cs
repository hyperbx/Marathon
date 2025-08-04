using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LightType : uint
    {
        NND_LIGHTTYPE_STANDARD_GL = 65536,
        NND_LIGHTTYPE_MASK = 65599,
        NND_LIGHTTYPE_PARALLEL = 1,
        NND_LIGHTTYPE_POINT = 2,
        NND_LIGHTTYPE_TARGET_SPOT = 4,
        NND_LIGHTTYPE_ROTATION_SPOT = 8,
        NND_LIGHTTYPE_TARGET_DIRECTIONAL = 16,
        NND_LIGHTTYPE_ROTATION_DIRECTIONAL = 32,
        NND_LIGHTTYPE_COMMON_MASK = 63
    }
}
