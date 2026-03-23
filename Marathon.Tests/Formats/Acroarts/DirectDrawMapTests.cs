using Marathon.Formats.Acroarts;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Acroarts
{
    internal class DirectDrawMapTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<DirectDrawMap>("*.ddm");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
