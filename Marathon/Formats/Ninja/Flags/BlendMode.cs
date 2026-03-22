using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlendMode : uint
    {
        NNE_BLENDMODE_NONE = 0,
        NNE_BLENDMODE_ADDITIVE = 0x01,
        NNE_BLENDMODE_SRCCOL = 0x300,
        NNE_BLENDMODE_INVSRCCOL = 0x301,
        NNE_BLENDMODE_SRCALPHA = 0x302,
        NNE_BLENDMODE_INVSRCALPHA = 0x303,
        NNE_BLENDMODE_DSTALPHA = 0x304,
        NNE_BLENDMODE_INVDSTALPHA = 0x305,
        NNE_BLENDMODE_DSTCOL = 0x306,
        NNE_BLENDMODE_INVDSTCOL = 0x307,
        NNE_BLENDMODE_SRCALPHASAT = 0x308,
        NNE_BLENDMODE_CONSTCOL = 0x8001,
        NNE_BLENDMODE_INVCONSTCOL = 0x8002,
        NNE_BLENDMODE_CONSTALPHA = 0x8003,
        NNE_BLENDMODE_INVCONSTALPHA = 0x8004,
    }
}
