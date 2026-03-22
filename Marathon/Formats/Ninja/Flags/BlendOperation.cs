using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlendOperation : uint
    {
        NNE_BLENDOP_ADD = 0x8006,
        NNE_BLENDOP_MIN = 0x8007,
        NNE_BLENDOP_MAX = 0x8008,
        NNE_BLENDOP_SUB = 0x800A,
        NNE_BLENDOP_REVSUB = 0x800B,
        NNE_BLENDOP_REVSUBSIGNED = 0xF005,
        NNE_BLENDOP_ADDSIGNED = 0xF006,
    }
}
