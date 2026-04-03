using Assimp;
using System.Numerics;

namespace Marathon.Extensions
{
    public static class AssimpExtensions
    {
        public static Vector3 ToVector3(this Vector3D in_vector)
        {
            return new Vector3(in_vector.X, in_vector.Y, in_vector.Z);
        }
    }
}
