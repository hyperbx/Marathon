using System.Numerics;

namespace Marathon.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ParseVector3(string in_x, string in_y, string in_z)
        {
            return new Vector3(float.Parse(in_x), float.Parse(in_y), float.Parse(in_z));
        }

        public static bool TryParseVector3(string in_x, string in_y, string in_z, out Vector3 out_result)
        {
            try
            {
                out_result = ParseVector3(in_x, in_y, in_z);
                return true;
            }
            catch
            {
                out_result = new();
            }

            return false;
        }
    }
}
