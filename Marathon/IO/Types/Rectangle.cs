using System.Numerics;

namespace Marathon.IO.Types
{
    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;

        public override string ToString()
        {
            return $"<{Min.X}, {Min.Y}, {Max.X}, {Max.Y}>";
        }
    }
}
