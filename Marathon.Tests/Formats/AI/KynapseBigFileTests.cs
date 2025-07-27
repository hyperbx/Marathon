using Marathon.Formats.AI;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.AI
{
    internal class KynapseBigFileTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<KynapseBigFile>("*.kbf");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
