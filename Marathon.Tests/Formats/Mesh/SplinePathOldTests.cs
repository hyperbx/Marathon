using Marathon.Formats.Mesh;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Mesh
{
    internal class SplinePathOldTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<SplinePathOld>("kdv_a_path.bin");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
