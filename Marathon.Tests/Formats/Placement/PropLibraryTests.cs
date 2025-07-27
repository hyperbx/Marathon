using Marathon.Formats.Placement;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Placement
{
    internal class PropLibraryTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<PropLibrary>("*.prop");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
