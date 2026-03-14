using Marathon.Formats.Acroarts.Types;
using Newtonsoft.Json;

namespace Marathon.Formats.Acroarts.Chunks
{
    public interface IChunk : INode
    {
        [JsonIgnore]
        long Offset { get; set; }
    }
}
