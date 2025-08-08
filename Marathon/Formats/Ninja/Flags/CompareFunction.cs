using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompareFunction : uint
    {
        NNE_CMPFUNC_NEVER = 0x200,
        NNE_CMPFUNC_LESS = 0x201,
        NNE_CMPFUNC_EQUAL = 0x202,
        NNE_CMPFUNC_LESSEQUAL = 0x203,
        NNE_CMPFUNC_GREATER = 0x204,
        NNE_CMPFUNC_NOTEQUAL = 0x205,
        NNE_CMPFUNC_GREATEREQUAL = 0x206,
        NNE_CMPFUNC_ALWAYS = 0x207
    }
}
