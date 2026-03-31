using System.Numerics;

namespace Marathon.IO.Types
{
    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;

        public Rectangle() { }

        public Rectangle(Vector2 in_min, Vector2 in_max)
        {
            Min = in_min;
            Max = in_max;
        }

        public Rectangle(float in_minX, float in_minY, float in_maxX, float in_maxY)
        {
            Min = new(in_minX, in_minY);
            Max = new(in_maxX, in_maxY);
        }

        public override string ToString()
        {
            return $"<{Min.X}, {Min.Y}, {Max.X}, {Max.Y}>";
        }
    }
}
