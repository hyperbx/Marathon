using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceType : int
    {
        Model = 0,
        CellSprite = 4,
        Camera = 5,
        Screen = 6,
        Light = 7,
        Primitive = 8,
        Extra = 9
    }
}
