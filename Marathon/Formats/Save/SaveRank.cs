using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Format names:        Save Data
// Format references:   Sonicteam::SaveDataTask
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Save
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SaveRank : int
    {
        S,
        A,
        B,
        C,
        D
    }
}
