using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Marathon.Formats.Mesh.Ninja.Flags
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeNameSortType : uint
    {
        NNE_NODENAME_SORTTYPE_INDEX = 0,
        NNE_NODENAME_SORTTYPE_NAME = 1
    }
}
