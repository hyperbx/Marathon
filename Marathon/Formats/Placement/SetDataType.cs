﻿namespace Marathon.Formats.Placement
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SetDataType
    {
        Boolean,
        Int32,
        Single,
        String,
        Vector3,
        UInt32 = 6
    }
}
