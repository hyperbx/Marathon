using Marathon.Formats.Mesh.Ninja;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Mesh.Ninja
{
    internal class NinjaNextTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTestXNO];

        private static bool BinaryIdenticalTestXNO()
        {
            return TestHelper.CheckAllBinaries<NinjaNext>("*.xno", ["en_ewv_Arm.xno", "en_ewv_eggman.xno", "en_ewv_Head.xno", "en_eWyvern.xno"]);
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
