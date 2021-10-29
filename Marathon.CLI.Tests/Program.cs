using Marathon.Formats.Script.Lua;

namespace Marathon.CLI.Tests
{
    class Program
    {
        static void Main()
        {
            string common = @"C:\Users\gabe1\AppData\Local\Hyper_Development_Team\Sonic '06 Toolkit\Archives\43893\vee5352x.3y0\player\xenon\player\common.lub";
            string output = @"C:\Users\gabe1\AppData\Local\Hyper_Development_Team\Sonic '06 Toolkit\Archives\43893\vee5352x.3y0\player\xenon\player\common_decomp.lub";

            unluac.Decompile(common, output);
        }
    }
}
