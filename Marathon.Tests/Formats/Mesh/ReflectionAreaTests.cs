using Marathon.Formats.Mesh;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Mesh
{
    internal class ReflectionAreaTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<ReflectionArea>("*.rab");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
