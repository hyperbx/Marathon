using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubMotionInterpolationType : uint
    {
        // Masks
        NND_SMOTIPTYPE_IP_MASK = 0xE77U,
        NND_SMOTIPTYPE_REPEAT_MASK = 0x1F0000U,

        // Repeat types
        NND_SMOTIPTYPE_NOREPEAT = 0x10000U,
        NND_SMOTIPTYPE_CONSTREPEAT = 0x20000U,
        NND_SMOTIPTYPE_REPEAT = 0x40000U,
        NND_SMOTIPTYPE_MIRROR = 0x80000U,
        NND_SMOTIPTYPE_OFFSET = 0x100000U,

        // Interpolation types
        NND_SMOTIPTYPE_SPLINE = 1,
        NND_SMOTIPTYPE_LINEAR = 2,
        NND_SMOTIPTYPE_CONSTANT = 4,
        NND_SMOTIPTYPE_BEZIER = 16,
        NND_SMOTIPTYPE_SI_SPLINE = 32,
        NND_SMOTIPTYPE_TRIGGER = 64,
        NND_SMOTIPTYPE_QUAT_LERP = 512,
        NND_SMOTIPTYPE_QUAT_SLERP = 1024,
        NND_SMOTIPTYPE_QUAT_SQUAD = 2048
    }
}
