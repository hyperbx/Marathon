using Marathon.Formats.Particle;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Particle
{
    internal class ParticleEffectBankTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<ParticleEffectBank>("*.peb");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
