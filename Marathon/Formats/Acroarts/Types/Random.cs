using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types
{
    public class Random
    {
        public static float Get(float in_max)
        {
            var result = (float)(CRandom.Rand() % 100 + 1) / 100.0f * in_max;

            if (in_max < 0.0f)
            {
                if (in_max >= result)
                    return result / 2.0f;
            }
            else if (result >= in_max)
            {
                return result / 2.0f;
            }

            return result;
        }
    }
}
