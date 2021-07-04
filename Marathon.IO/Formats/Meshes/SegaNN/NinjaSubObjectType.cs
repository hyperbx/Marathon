using System;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    // TODO: Figure out what maps to what. The Sonic 4 decomp doesn't seem to have these values, so we're on our own.
    [Flags]
    public enum NinjaSubObjectType
    {
        NND_SUBOBJTYPE_OPAQUE = 1,
        NND_SUBOBJTYPE_PLIABLE = 512
    }
}
