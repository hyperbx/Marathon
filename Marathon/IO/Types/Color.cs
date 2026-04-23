using Amicitia.IO.Binary;
using System.Collections.Generic;

namespace Marathon.IO.Types
{
    public struct Color<TData, TFormat> : IBinarySerializable where TData : unmanaged where TFormat : IColorFormat
    {
        public TData R { get; set; }

        public TData G { get; set; }

        public TData B { get; set; }

        public TData A { get; set; }

        public Color() { }

        public Color(TData in_r, TData in_g, TData in_b, TData in_a)
        {
            R = in_r;
            G = in_g;
            B = in_b;
            A = in_a;
        }

        public void Read(BinaryObjectReader in_reader)
        {
            if (typeof(TData) == typeof(byte))
            {
                var colour = in_reader.Read<uint>();

                var c0 = (TData)(object)(byte)(colour & 0xFF);
                var c1 = (TData)(object)(byte)((colour >> 8) & 0xFF);
                var c2 = (TData)(object)(byte)((colour >> 16) & 0xFF);
                var c3 = (TData)(object)(byte)((colour >> 24) & 0xFF);

                Assign(c0, c1, c2, c3);

                return;
            }

            Assign(in_reader.Read<TData>(), in_reader.Read<TData>(), in_reader.Read<TData>(), in_reader.Read<TData>());
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            if (typeof(TData) == typeof(byte))
            {
                var colour = 0U;

                if (typeof(TFormat) == typeof(RGBA))
                {
                    colour = (uint)(object)R |
                             ((uint)(object)G << 8) |
                             ((uint)(object)B << 16) |
                             ((uint)(object)A << 24);
                }
                else if (typeof(TFormat) == typeof(ARGB))
                {
                    colour = (uint)(object)A |
                             ((uint)(object)R << 8) |
                             ((uint)(object)G << 16) |
                             ((uint)(object)B << 24);
                }
                else if (typeof(TFormat) == typeof(ABGR))
                {
                    colour = (uint)(object)A |
                             ((uint)(object)B << 8) |
                             ((uint)(object)G << 16) |
                             ((uint)(object)R << 24);
                }
                else if (typeof(TFormat) == typeof(BGRA))
                {
                    colour = (uint)(object)B |
                             ((uint)(object)G << 8) |
                             ((uint)(object)R << 16) |
                             ((uint)(object)A << 24);
                }

                in_writer.Write(colour);

                return;
            }

            Write(in_writer, R, G, B, A);
        }

        public void Write(BinaryObjectWriter in_writer, TData in_r, TData in_g, TData in_b, TData in_a)
        {
            if (typeof(TFormat) == typeof(RGBA))
            {
                in_writer.Write(in_r);
                in_writer.Write(in_g);
                in_writer.Write(in_b);
                in_writer.Write(in_a);
            }
            else if (typeof(TFormat) == typeof(ARGB))
            {
                in_writer.Write(in_a);
                in_writer.Write(in_r);
                in_writer.Write(in_g);
                in_writer.Write(in_b);
            }
            else if (typeof(TFormat) == typeof(ABGR))
            {
                in_writer.Write(in_a);
                in_writer.Write(in_b);
                in_writer.Write(in_g);
                in_writer.Write(in_r);
            }
            else if (typeof(TFormat) == typeof(BGRA))
            {
                in_writer.Write(in_b);
                in_writer.Write(in_g);
                in_writer.Write(in_r);
                in_writer.Write(in_a);
            }
        }

        public void Assign(TData in_c0, TData in_c1, TData in_c2, TData in_c3)
        {
            if (typeof(TFormat) == typeof(RGBA))
            {
                R = in_c0;
                G = in_c1;
                B = in_c2;
                A = in_c3;
            }
            else if (typeof(TFormat) == typeof(ARGB))
            {
                A = in_c3;
                R = in_c0;
                G = in_c1;
                B = in_c2;
            }
            else if (typeof(TFormat) == typeof(ABGR))
            {
                A = in_c3;
                B = in_c2;
                G = in_c1;
                R = in_c0;
            }
            else if (typeof(TFormat) == typeof(BGRA))
            {
                B = in_c2;
                G = in_c1;
                R = in_c0;
                A = in_c3;
            }
        }

        public readonly Color<TData, TFormat> Flip()
        {
            return new Color<TData, TFormat>(A, B, G, R);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not Color<TData, TFormat> out_color)
                return false;

            return EqualityComparer<TData>.Default.Equals(R, out_color.R) &&
                   EqualityComparer<TData>.Default.Equals(G, out_color.G) &&
                   EqualityComparer<TData>.Default.Equals(B, out_color.B) &&
                   EqualityComparer<TData>.Default.Equals(A, out_color.A);
        }

        public override string ToString()
        {
            if (typeof(TFormat) == typeof(ARGB))
            {
                return $"<{A}, {R}, {G}, {B}>";
            }
            else if (typeof(TFormat) == typeof(ABGR))
            {
                return $"<{A}, {B}, {G}, {R}>";
            }
            else if (typeof(TFormat) == typeof(BGRA))
            {
                return $"<{B}, {G}, {R}, {A}>";
            }

            return $"<{R}, {G}, {B}, {A}>";
        }
    }

    public struct RGBA : IColorFormat { }

    public struct ARGB : IColorFormat { }

    public struct ABGR : IColorFormat { }

    public struct BGRA : IColorFormat { }

    public interface IColorFormat { }
}
