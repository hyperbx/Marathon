using Marathon.Formats.Audio;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Audio
{
    internal class SoundBankTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<SoundBank>("*.sbk");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
