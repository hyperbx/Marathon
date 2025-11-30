using Marathon.Formats.Ninja;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Ninja
{
    internal class NinjaNextTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<NinjaNext>("*.xn*", in_ignorePattern: "*.xncp");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
