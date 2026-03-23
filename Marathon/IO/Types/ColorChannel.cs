using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marathon.IO.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColorChannel : int
    {
        R,
        G,
        B,
        A
    }
}
