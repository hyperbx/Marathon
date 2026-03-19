using Marathon.Formats.Acroarts;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Acroarts
{
    internal class AckResourceTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<AckResource>("*.mab", ["so_homingsmash00.mab"]);
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
