using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public struct CameraTargetRoll
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

    public struct CameraTargetUpVector
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

    public struct CameraTargetUpTarget
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

    public struct CameraRotation
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
