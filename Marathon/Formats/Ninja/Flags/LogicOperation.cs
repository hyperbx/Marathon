using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LogicOperation : uint
    {
        NNE_LOGICOP_NONE = 0,
        NNE_LOGICOP_CLEAR = 0x1500,
        NNE_LOGICOP_AND = 0x1501,
        NNE_LOGICOP_ANDREV = 0x1502,
        NNE_LOGICOP_COPY = 0x1503,
        NNE_LOGICOP_ANDINV = 0x1504,
        NNE_LOGICOP_NOOP = 0x1505,
        NNE_LOGICOP_XOR = 0x1506,
        NNE_LOGICOP_OR = 0x1507,
        NNE_LOGICOP_NOR = 0x1508,
        NNE_LOGICOP_EQUIV = 0x1509,
        NNE_LOGICOP_INVERT = 0x150A,
        NNE_LOGICOP_ORREV = 0x150B,
        NNE_LOGICOP_COPYINV = 0x150C,
        NNE_LOGICOP_ORINV = 0x150D,
        NNE_LOGICOP_NAND = 0x150E,
        NNE_LOGICOP_SET = 0x150F,
    }
}
