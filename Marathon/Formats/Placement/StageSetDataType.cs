using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Format research attribution: Knuxfan24, Darío

namespace Marathon.Formats.Placement
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StageSetDataType
    {
        Boolean,
        Int32,
        Single,
        String,
        Vector3,
        UInt32 = 6
    }
}
