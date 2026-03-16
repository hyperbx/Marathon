using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TranslateVectorType : int
    {
        Local,
        WorldVector,
        World,
        CameraVector,
        Camera,
        LightVector,
        Light
    }
}
