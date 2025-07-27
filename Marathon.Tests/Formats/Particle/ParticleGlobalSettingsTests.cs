using Marathon.Formats.Particle;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Particle
{
    internal class ParticleGlobalSettingsTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<ParticleGlobalSettings>("*.pgs");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
