using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlendOperation : uint
    {
        NNE_BLENDOP_ADD = 0x8006
    }
}
