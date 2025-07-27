using Marathon.Formats.Particle;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Particle
{
    internal class ParticleContainerTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<ParticleContainer>("*.plc");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
