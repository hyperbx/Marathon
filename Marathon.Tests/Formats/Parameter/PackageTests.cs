using Marathon.Formats.Parameter;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Parameter
{
    internal class PackageTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<Package>("*.pkg");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
