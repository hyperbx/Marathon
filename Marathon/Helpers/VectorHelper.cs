using System;
using System.Collections.Generic;
using System.Numerics;

namespace Marathon.Helpers
{
    public class VectorHelper
    {
        public static Vector3 Centre(List<Vector3> in_points)
        {
            if (in_points.Count <= 0)
                throw new ArgumentException("The point list is empty.");

            if (in_points.Count == 1)
                return in_points[0];

            var sum = Vector3.Zero;

            foreach (var v in in_points)
                sum += v;

            return sum / in_points.Count;
        }

        public static Quaternion Average(List<Quaternion> in_quaternions)
        {
            if (in_quaternions.Count <= 0)
                throw new ArgumentException("The quaternion list is empty.");

            if (in_quaternions.Count == 1)
                return in_quaternions[0];

            var sum = new Quaternion(0, 0, 0, 0);

            foreach (var q in in_quaternions)
            {
                if (Quaternion.Dot(q, in_quaternions[0]) < 0.0f)
                {
                    sum += -q;
                }
                else
                {
                    sum += q;
                }
            }

            return Quaternion.Normalize(sum);
        }
    }
}
