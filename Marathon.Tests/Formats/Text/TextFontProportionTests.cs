using Marathon.Formats.Text;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Text
{
    internal class TextFontProportionTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<TextFontProportion>("*.pfi");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
