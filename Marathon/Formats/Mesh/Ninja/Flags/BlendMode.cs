using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlendMode : uint
    {
        NNE_BLENDMODE_SRCALPHA = 0x302,
        NNE_BLENDMODE_INVSRCALPHA = 0x303
    }
}
