using Marathon.Formats.Text;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Text
{
    internal class TextFontPictureTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<TextFontPicture>("*.pft");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
