using Marathon.Formats.Parameter;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Parameter
{
    internal class ObjectExplosionParameterListTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<ObjectExplosionParameterList>("Explosion.bin");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
