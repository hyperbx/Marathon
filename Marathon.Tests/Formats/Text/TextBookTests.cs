using Marathon.Formats.Text;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Text
{
    internal class TextBookTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<TextBook>("*.mst");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
