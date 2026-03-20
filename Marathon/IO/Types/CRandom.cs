namespace Marathon.IO.Types
{
    public class CRandom
    {
        private static ulong _next;

        public static int Rand()
        {
            _next = _next * 1103515245U + 12345U;
            return (int)((_next / 65536U) % 32768U);
        }

        public static void SRand(uint in_seed)
        {
            _next = in_seed;
        }
    }
}
