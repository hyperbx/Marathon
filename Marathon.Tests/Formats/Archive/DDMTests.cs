using Marathon.Formats.Archive;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Archive
{
    internal class DDMTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<DDM>("*.ddm");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
