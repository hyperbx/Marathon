using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public struct KeyframeF32
    {
        public float Frame { get; set; }

        public float Value { get; set; }
    }

    public struct KeyframeS16
    {
        public short Frame { get; set; }

        public short Value { get; set; }
    }

    public struct KeyframeVector
    {
        public float Frame { get; set; }

        public Vector3 Value { get; set; }
    }

    public struct KeyframeRotateS16
    {
        public short Frame { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public short Z { get; set; }
    }
}
