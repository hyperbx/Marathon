using Newtonsoft.Json.Linq;

namespace Marathon.Helpers
{
    public class VectorHelper
    {
        /// <summary>
        /// Parses Vector3 data from a JObject - returns the input object if it's just a Vector3.
        /// </summary>
        public static Vector3 ParseVector3(object data)
        {
            if (data.GetType().Equals(typeof(JObject)))
            {
                JObject jsonVector3 = (JObject)data;

                return new Vector3(jsonVector3["X"].Value<float>(), jsonVector3["Y"].Value<float>(), jsonVector3["Z"].Value<float>());
            }

            return (Vector3)data;
        }
    }
}
