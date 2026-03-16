using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColorBlendMode : int
    {
        Modulation,
        Addition,
        Subtraction,
        Replace
    }
}
