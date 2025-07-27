using Marathon.Formats.Parameter;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Parameter
{
    internal class EnemyShotParameterListTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<EnemyShotParameterList>("ShotParameter.bin");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
