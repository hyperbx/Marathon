using System;

namespace Marathon.Helpers
{
    public class MathHelper
    {
        public static float ToDegrees(float in_radians)
        {
            return in_radians * (180.0f / (float)Math.PI);
        }

        public static float ToRadians(float in_degrees)
        {
            return in_degrees * ((float)Math.PI / 180.0f);
        }
    }
}
