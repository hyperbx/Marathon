using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public struct LightStandardGL
    {
        public uint UserData { get; set; }

        public RGBAF32 Ambient { get; set; }

        public RGBAF32 Diffuse { get; set; }

        public RGBAF32 Specular { get; set; }

        public Vector4 Position { get; set; }

        public Vector3 SpotDirection { get; set; }

        public float SpotExponent { get; set; }

        public float SpotCutOff { get; set; }

        public float ConstantAttenuation { get; set; }

        public float LinearAttenuation { get; set; }

        public float QuadraticAttenuation { get; set; }
    }

    public struct LightParallel
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Direction { get; set; }
    }

    public struct LightPoint
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }
    }

    public struct LightTargetSpot
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public int InnerAngle { get; set; }

        public int OuterAngle { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }
    }

    public struct LightRotationSpot
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public int RotationType { get; set; }

        public int Yaw { get; set; }

        public int Pitch { get; set; }

        public int Roll { get; set; }

        public int InnerAngle { get; set; }

        public int OuterAngle { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }
    }

    public struct LightTargetDirectional
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public float InnerRange { get; set; }

        public float OuterRange { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }

        public float Reserved { get; set; }
    }

    public struct LightRotationDirectional
    {
        public uint UserData { get; set; }

        public RGBAF32 Colour { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public int RotationType { get; set; }

        public int Yaw { get; set; }

        public int Pitch { get; set; }

        public int Roll { get; set; }

        public float InnerRange { get; set; }

        public float OuterRange { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }
    }
}
