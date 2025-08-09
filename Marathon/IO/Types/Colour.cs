using Amicitia.IO.Binary;
using System.Collections.Generic;

namespace Marathon.IO.Types
{
    public struct Colour<TData, TFormat> : IBinarySerializable where TData : unmanaged where TFormat : IColourFormat
    {
        public TData R { get; set; }

        public TData G { get; set; }

        public TData B { get; set; }

        public TData A { get; set; }

        public Colour() { }

        public Colour(TData in_r, TData in_g, TData in_b, TData in_a)
        {
            R = in_r;
            G = in_g;
            B = in_b;
            A = in_a;
        }

        public void Read(BinaryObjectReader in_reader)
        {
            if (typeof(TFormat) == typeof(RGBA))
            {
                R = in_reader.Read<TData>();
                G = in_reader.Read<TData>();
                B = in_reader.Read<TData>();
                A = in_reader.Read<TData>();
            }
            else if (typeof(TFormat) == typeof(ARGB))
            {
                A = in_reader.Read<TData>();
                R = in_reader.Read<TData>();
                G = in_reader.Read<TData>();
                B = in_reader.Read<TData>();
            }
            else if (typeof(TFormat) == typeof(BGRA))
            {
                B = in_reader.Read<TData>();
                G = in_reader.Read<TData>();
                R = in_reader.Read<TData>();
                A = in_reader.Read<TData>();
            }
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            if (typeof(TFormat) == typeof(RGBA))
            {
                in_writer.Write(R);
                in_writer.Write(G);
                in_writer.Write(B);
                in_writer.Write(A);
            }
            else if (typeof(TFormat) == typeof(ARGB))
            {
                in_writer.Write(A);
                in_writer.Write(R);
                in_writer.Write(G);
                in_writer.Write(B);
            }
            else if (typeof(TFormat) == typeof(BGRA))
            {
                in_writer.Write(B);
                in_writer.Write(G);
                in_writer.Write(R);
                in_writer.Write(A);
            }
        }

        public Colour<TData, TFormat> Flip()
        {
            return new Colour<TData, TFormat>(A, B, G, R);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not Colour<TData, TFormat> out_colour)
                return false;

            return EqualityComparer<TData>.Default.Equals(R, out_colour.R) &&
                   EqualityComparer<TData>.Default.Equals(G, out_colour.G) &&
                   EqualityComparer<TData>.Default.Equals(B, out_colour.B) &&
                   EqualityComparer<TData>.Default.Equals(A, out_colour.A);
        }
    }

    public struct RGBA : IColourFormat { }

    public struct ARGB : IColourFormat { }

    public struct BGRA : IColourFormat { }

    public interface IColourFormat { }
}
