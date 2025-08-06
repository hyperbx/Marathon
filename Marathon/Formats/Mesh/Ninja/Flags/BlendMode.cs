using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlendMode : uint
    {
        NNE_BLENDMODE_NONE = 0,
        NNE_BLENDMODE_ADDITIVE = 0x01,
        NNE_BLENDMODE_SRCALPHA = 0x302,
        NNE_BLENDMODE_INVSRCALPHA = 0x303
    }
}
