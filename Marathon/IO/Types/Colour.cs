namespace Marathon.IO.Types
{
    public struct RGBA8
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public byte A { get; set; }

        public RGBA8() { }

        public RGBA8(byte in_r, byte in_g, byte in_b, byte in_a)
        {
            R = in_r;
            G = in_g;
            B = in_b;
            A = in_a;
        }

        public readonly RGBA8 Flip()
        {
            return new(A, B, G, R);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not RGBA8 out_rgba8)
                return false;

            return R == out_rgba8.R &&
                   G == out_rgba8.G &&
                   B == out_rgba8.B &&
                   A == out_rgba8.A;
        }
    }

    public struct RGBAF32
    {
        public float R { get; set; }

        public float G { get; set; }

        public float B { get; set; }

        public float A { get; set; }

        public RGBAF32() { }

        public RGBAF32(float in_r, float in_g, float in_b, float in_a)
        {
            R = in_r;
            G = in_g;
            B = in_b;
            A = in_a;
        }

        public readonly RGBAF32 Flip()
        {
            return new(A, B, G, R);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not RGBAF32 out_rgbaF32)
                return false;

            return R == out_rgbaF32.R &&
                   G == out_rgbaF32.G &&
                   B == out_rgbaF32.B &&
                   A == out_rgbaF32.A;
        }
    }

    public struct ARGB8
    {
        public byte A { get; set; }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public ARGB8() { }

        public ARGB8(byte in_a, byte in_r, byte in_g, byte in_b)
        {
            A = in_a;
            R = in_r;
            G = in_g;
            B = in_b;
        }

        public readonly ARGB8 Flip()
        {
            return new(B, G, R, A);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not ARGB8 out_argb8)
                return false;

            return R == out_argb8.R &&
                   G == out_argb8.G &&
                   B == out_argb8.B &&
                   A == out_argb8.A;
        }
    }

    public struct ARGBF32
    {
        public float A { get; set; }

        public float R { get; set; }

        public float G { get; set; }

        public float B { get; set; }

        public ARGBF32() { }

        public ARGBF32(float in_a, float in_r, float in_g, float in_b)
        {
            A = in_a;
            R = in_r;
            G = in_g;
            B = in_b;
        }

        public readonly ARGBF32 Flip()
        {
            return new(B, G, R, A);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not ARGBF32 out_argbF32)
                return false;

            return R == out_argbF32.R &&
                   G == out_argbF32.G &&
                   B == out_argbF32.B &&
                   A == out_argbF32.A;
        }
    }

    public struct RGB8
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public RGB8() { }

        public RGB8(byte in_r, byte in_g, byte in_b)
        {
            R = in_r;
            G = in_g;
            B = in_b;
        }

        public readonly RGB8 Flip()
        {
            return new(B, G, R);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not RGB8 out_rgb8)
                return false;

            return R == out_rgb8.R &&
                   G == out_rgb8.G &&
                   B == out_rgb8.B;
        }
    }
}
