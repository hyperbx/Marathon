using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class MaterialColour
    {
        public ARGBF32 Diffuse { get; set; }

        public ARGBF32 Ambient { get; set; }

        public ARGBF32 Specular { get; set; }

        public ARGBF32 Emissive { get; set; }

        public float Power { get; set; }

        public MaterialColour() { }

        public MaterialColour(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Diffuse = in_reader.Read<ARGBF32>();
            Ambient = in_reader.Read<ARGBF32>();
            Specular = in_reader.Read<ARGBF32>();
            Emissive = in_reader.Read<ARGBF32>();
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

            return out_colour.Diffuse.Equals(Diffuse) &&
                   out_colour.Ambient.Equals(Ambient) &&
                   out_colour.Specular.Equals(Specular) &&
                   out_colour.Emissive.Equals(Emissive) &&
                   out_colour.Power == Power;
        }
    }
}
