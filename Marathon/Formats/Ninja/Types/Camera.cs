using System.Numerics;

namespace Marathon.Formats.Ninja.Types
{
    public interface ICamera
    {
        public uint UserData { get; set; }

        public int FOV { get; set; }

        public float AspectRatio { get; set; }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 Position { get; set; }
    }

    public struct CameraTargetRoll : ICamera
    {
        public uint UserData { get; set; }

        public int FOV { get; set; }

        public float AspectRatio { get; set; }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public int Roll { get; set; }
    }

    public struct CameraTargetUpVector : ICamera
    {
        public uint UserData { get; set; }

        public int FOV { get; set; }

        public float AspectRatio { get; set; }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public Vector3 UpVector { get; set; }
    }

    public struct CameraTargetUpTarget : ICamera
    {
        public uint UserData { get; set; }

        public int FOV { get; set; }

        public float AspectRatio { get; set; }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public Vector3 UpTarget { get; set; }
    }

    public struct CameraRotation : ICamera
    {
        public uint UserData { get; set; }

        public int FOV { get; set; }

        public float AspectRatio { get; set; }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 Position { get; set; }

        public int RotationType { get; set; }

        public int Yaw { get; set; }

        public int Pitch { get; set; }

        public int Roll { get; set; }
    }
}
