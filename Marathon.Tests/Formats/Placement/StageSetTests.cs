using Marathon.Formats.Placement;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Placement
{
    internal class StageSetTests : ITest
    {
        private Func<bool>[] _tests = [BinaryIdenticalTest];

        private static bool BinaryIdenticalTest()
        {
            // These files contain broken string lengths which can be ignored.
            List<string> ignoreList =
            [
                "aqa_mapB_effects.set",
                "set_wap_a_sonic.set",
                "set_mission_0012c_01.set",
                "set_mission_0012c_02.set",
                "set_wap_a_shadow.set",
                "set_mission_0105a.set",
                "set_mission_0202c_01.set",
                "set_mission_0202c_02.set",
                "set_mission_0202c.set",
                "set_mission_0203b01.set",
                "set_wap_a_sonic_h.set",
                "set_wap_a_shadow_h.set"
            ];

            return TestHelper.CheckAllBinaries<StageSet>("*.set", ignoreList);
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
