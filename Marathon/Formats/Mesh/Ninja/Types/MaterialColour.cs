using Amicitia.IO.Binary;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class MaterialColour
    {
        public Vector4 Diffuse { get; set; }

        public Vector4 Ambient { get; set; }

        public Vector4 Specular { get; set; }

        public Vector4 Emissive { get; set; }

        public float Power { get; set; }

        public MaterialColour() { }

        public MaterialColour(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Diffuse = in_reader.Read<Vector4>();
            Ambient = in_reader.Read<Vector4>();
            Specular = in_reader.Read<Vector4>();
            Emissive = in_reader.Read<Vector4>();
            Power = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Diffuse);
            in_writer.Write(Ambient);
            in_writer.Write(Specular);
            in_writer.Write(Emissive);
            in_writer.Write(Power);
            in_writer.Align(16);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not MaterialColour out_colour)
                return false;

            return out_colour.Diffuse == Diffuse &&
                   out_colour.Ambient == Ambient &&
                   out_colour.Specular == Specular &&
                   out_colour.Emissive == Emissive &&
                   out_colour.Power == Power;
        }
    }
}
