using Marathon.Formats.Text;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Text
{
    internal class TextFontMapTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<TextFontMap>("*.ftm");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
