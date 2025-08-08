using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MotionType : uint
    {
        // Masks
        NND_MOTIONTYPE_CATEGORY_MASK = 31,
        NND_MOTIONTYPE_REPEAT_MASK = 0x1F0040U,

        // Flags
        NND_MOTIONTYPE_VERSION2 = 0x10000000U,

        // Repeat types
        NND_MOTIONTYPE_TRIGGER = 64,
        NND_MOTIONTYPE_NOREPEAT = 0x10000U,
        NND_MOTIONTYPE_CONSTREPEAT = 0x20000U,
        NND_MOTIONTYPE_REPEAT = 0x40000U,
        NND_MOTIONTYPE_MIRROR = 0x80000U,
        NND_MOTIONTYPE_OFFSET = 0x100000U,

        // Motion types
        NND_MOTIONTYPE_NODE = 1,
        NND_MOTIONTYPE_CAMERA = 2,
        NND_MOTIONTYPE_LIGHT = 4,
        NND_MOTIONTYPE_MORPH = 8,
        NND_MOTIONTYPE_MATERIAL = 16
    }
}
