using Marathon.Formats.Event;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Event
{
    internal class EventPlaybookTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<EventPlaybook>("*.epb");
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
