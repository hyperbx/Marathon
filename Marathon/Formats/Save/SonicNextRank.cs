using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Format research attribution: Hyper

namespace Marathon.Formats.Save
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SonicNextRank : int
    {
        S,
        A,
        B,
        C,
        D
    }
}
