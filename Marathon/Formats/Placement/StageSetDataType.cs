using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Format names:        Stage Set
// Format references:   Sonicteam::Prop::StageSetManagerRunner, LoadStageSet
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Radfordhound, Darío

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
