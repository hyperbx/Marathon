namespace Marathon.Formats.Ninja
{
    public class NinjaMath
    {
        public static int DegreesToNinjaAngle(float in_degrees)
        {
            return (int)(in_degrees * 182.04443f);
        }

        public static int RadiansToNinjaAngle(float in_radians)
        {
            return (int)(in_radians * 10430.378f);
        }

        public static float NinjaAngleToDegrees(int in_angle)
        {
            return (float)(in_angle * 0.005493164f);
        }

        public static float NinjaAngleToRadians(int in_angle)
        {
            return (float)(in_angle * 9.58738E-05f);
        }
    }
}
