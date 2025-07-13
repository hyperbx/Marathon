using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Format research attribution: Knuxfan24, Darío

namespace Marathon.Formats.Placement
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StageSetDataType : uint
    {
        Boolean,
        Int32,
        Single,
        String,
        Vector3,
        Object = 6
    }
}
