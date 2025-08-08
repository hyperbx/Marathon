using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Ninja.Types
{
    public class MaterialColour
    {
        public Colour<float, BGRA> Diffuse { get; set; }

        public Colour<float, BGRA> Ambient { get; set; }

        public Colour<float, BGRA> Specular { get; set; }

        public Colour<float, BGRA> Emissive { get; set; }

        public float Power { get; set; }

        public MaterialColour() { }

        public MaterialColour(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Diffuse = in_reader.ReadObject<Colour<float, BGRA>>();
            Ambient = in_reader.ReadObject<Colour<float, BGRA>>();
            Specular = in_reader.ReadObject<Colour<float, BGRA>>();
            Emissive = in_reader.ReadObject<Colour<float, BGRA>>();
            Power = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Diffuse);
            in_writer.WriteObject(Ambient);
            in_writer.WriteObject(Specular);
            in_writer.WriteObject(Emissive);
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
