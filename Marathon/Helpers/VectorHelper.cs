using Newtonsoft.Json.Linq;
using System.Numerics;

namespace Marathon.Helpers
{
    public class VectorHelper
    {
        /// <summary>
        /// Parses a <see cref="Vector3"/> from a <see cref="JObject"/>.
        /// <para>If <paramref name="in_data"/> is already a <see cref="Vector3"/>, it'll be returned as is.</para>
        /// </summary>
        public static Vector3 ParseVector3(object in_data)
        {
            if (in_data.GetType().Equals(typeof(JObject)))
            {
                JObject jsonVector3 = (JObject)in_data;

                return new Vector3(jsonVector3["X"].Value<float>(), jsonVector3["Y"].Value<float>(), jsonVector3["Z"].Value<float>());
            }

            return (Vector3)in_data;
        }
    }
}
