using Marathon.Formats.Parameter;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Parameter
{
    internal class EnemyParameterListTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<EnemyParameterList>("ScriptParameter.bin");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
