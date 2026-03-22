using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Ninja.Types
{
    public class MaterialColor
    {
        public Color<float, BGRA> Diffuse { get; set; }

        public Color<float, BGRA> Ambient { get; set; }

        public Color<float, BGRA> Specular { get; set; }

        public Color<float, BGRA> Emissive { get; set; }

        public float Power { get; set; }

        public MaterialColor() { }

        public MaterialColor(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Diffuse = in_reader.ReadObject<Color<float, BGRA>>();
            Ambient = in_reader.ReadObject<Color<float, BGRA>>();
            Specular = in_reader.ReadObject<Color<float, BGRA>>();
            Emissive = in_reader.ReadObject<Color<float, BGRA>>();
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
            if (in_obj is not MaterialColor out_color)
                return false;

            return out_color.Diffuse.Equals(Diffuse) &&
                   out_color.Ambient.Equals(Ambient) &&
                   out_color.Specular.Equals(Specular) &&
                   out_color.Emissive.Equals(Emissive) &&
                   out_color.Power == Power;
        }
    }
}
