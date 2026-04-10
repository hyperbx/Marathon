using Amicitia.IO.Binary;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.IO.Types;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Numerics;

namespace Marathon.Formats.Ninja.Types
{
    public class LightStandardGL : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Ambient { get; set; }

        public Color<float, RGBA> Diffuse { get; set; }

        public Color<float, RGBA> Specular { get; set; }

        public Vector4 Position { get; set; }

        public Vector3 SpotDirection { get; set; }

        public float SpotExponent { get; set; }

        public float SpotCutOff { get; set; }

        public float ConstantAttenuation { get; set; }

        public float LinearAttenuation { get; set; }

        public float QuadraticAttenuation { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Ambient = in_reader.ReadObject<Color<float, RGBA>>();
            Diffuse = in_reader.ReadObject<Color<float, RGBA>>();
            Specular = in_reader.ReadObject<Color<float, RGBA>>();
            Position = in_reader.Read<Vector4>();
            SpotDirection = in_reader.Read<Vector3>();
            SpotExponent = in_reader.Read<float>();
            SpotCutOff = in_reader.Read<float>();
            ConstantAttenuation = in_reader.Read<float>();
            LinearAttenuation = in_reader.Read<float>();
            QuadraticAttenuation = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.WriteObject(Ambient);
            in_writer.WriteObject(Diffuse);
            in_writer.WriteObject(Specular);
            in_writer.Write(Position);
            in_writer.Write(SpotDirection);
            in_writer.Write(SpotExponent);
            in_writer.Write(SpotCutOff);
            in_writer.Write(ConstantAttenuation);
            in_writer.Write(LinearAttenuation);
            in_writer.Write(QuadraticAttenuation);
        }
    }

    public class LightParallel : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

        public float Intensity { get; set; }

        public Vector3 Direction { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Direction = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Direction);
        }
    }

    public class LightPoint : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            FallOffStart = in_reader.Read<float>();
            FallOffEnd = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Position);
            in_writer.Write(FallOffStart);
            in_writer.Write(FallOffEnd);
        }
    }

    public class LightTargetSpot : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public int InnerAngle { get; set; }

        public int OuterAngle { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            Target = in_reader.Read<Vector3>();
            InnerAngle = in_reader.Read<int>();
            OuterAngle = in_reader.Read<int>();
            FallOffStart = in_reader.Read<float>();
            FallOffEnd = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Position);
            in_writer.Write(Target);
            in_writer.Write(InnerAngle);
            in_writer.Write(OuterAngle);
            in_writer.Write(FallOffStart);
            in_writer.Write(FallOffEnd);
        }
    }

    public class LightRotationSpot : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

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

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            RotationType = in_reader.Read<int>();
            Yaw = in_reader.Read<int>();
            Pitch = in_reader.Read<int>();
            Roll = in_reader.Read<int>();
            InnerAngle = in_reader.Read<int>();
            OuterAngle = in_reader.Read<int>();
            FallOffStart = in_reader.Read<float>();
            FallOffEnd = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Position);
            in_writer.Write(RotationType);
            in_writer.Write(Yaw);
            in_writer.Write(Pitch);
            in_writer.Write(Roll);
            in_writer.Write(InnerAngle);
            in_writer.Write(OuterAngle);
            in_writer.Write(FallOffStart);
            in_writer.Write(FallOffEnd);
        }
    }

    public class LightTargetDirectional : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

        public float Intensity { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public float InnerRange { get; set; }

        public float OuterRange { get; set; }

        public float FallOffStart { get; set; }

        public float FallOffEnd { get; set; }

        public float Reserved { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            Target = in_reader.Read<Vector3>();
            InnerRange = in_reader.Read<int>();
            OuterRange = in_reader.Read<int>();
            FallOffStart = in_reader.Read<float>();
            FallOffEnd = in_reader.Read<float>();
            Reserved = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Position);
            in_writer.Write(Target);
            in_writer.Write(InnerRange);
            in_writer.Write(OuterRange);
            in_writer.Write(FallOffStart);
            in_writer.Write(FallOffEnd);
            in_writer.Write(Reserved);
        }
    }

    public class LightRotationDirectional : IBinarySerializable
    {
        public uint UserData { get; set; }

        public Color<float, RGBA> Color { get; set; }

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

        public void Read(BinaryObjectReader in_reader)
        {
            UserData = in_reader.Read<uint>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Intensity = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            RotationType = in_reader.Read<int>();
            Yaw = in_reader.Read<int>();
            Pitch = in_reader.Read<int>();
            Roll = in_reader.Read<int>();
            InnerRange = in_reader.Read<int>();
            OuterRange = in_reader.Read<int>();
            FallOffStart = in_reader.Read<float>();
            FallOffEnd = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(UserData);
            in_writer.Write(Color);
            in_writer.Write(Intensity);
            in_writer.Write(Position);
            in_writer.Write(RotationType);
            in_writer.Write(Yaw);
            in_writer.Write(Pitch);
            in_writer.Write(Roll);
            in_writer.Write(InnerRange);
            in_writer.Write(OuterRange);
            in_writer.Write(FallOffStart);
            in_writer.Write(FallOffEnd);
        }
    }
}
