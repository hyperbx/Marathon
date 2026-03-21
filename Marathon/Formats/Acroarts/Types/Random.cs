using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types
{
    public class Random
    {
        public static uint GetRandomI(uint in_max)
        {
            return (uint)(long)((double)in_max * (float)(CRandom.Rand() % 30000) / 29999.0) % in_max;
        }

        public static float GetRandomF(float in_max)
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
