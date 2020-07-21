using Marathon;
using Marathon.IO;
using Marathon.IO.Helpers;
using Marathon.IO.Headers;
using Marathon.IO.Formats;
using Marathon.IO.Formats.SonicNext;

namespace Marathon.Tests
{
    class Program
    {
        /// <summary>
        /// A simple command-line application used for testing APIs.
        /// If any code is left over here from a previous commit, feel free to erase it.
        /// </summary>
        static void Main()
        {
            // CompressedU8Archive tests...
            //CompressedU8Archive arc = new CompressedU8Archive();
            //arc.Load(@"D:\Xenia\games\Games\SONIC THE HEDGEHOG\xenon\archives\text.arc.bak");
            //arc.Save(@"D:\Xenia\games\Games\SONIC THE HEDGEHOG\xenon\archives\text.arc");

            // SoundBank tests...
            SoundBank sbk = new SoundBank();
            sbk.ImportXML(@"C:\Users\gabe1\AppData\Local\Hyper_Development_Team\Sonic '06 Toolkit\Archives\9410\1lkn2d33.1ie\sound\xenon\sound\bgm.xml");
            sbk.Save(@"C:\Users\gabe1\AppData\Local\Hyper_Development_Team\Sonic '06 Toolkit\Archives\9410\1lkn2d33.1ie\sound\xenon\sound\bgm_new.sbk");
        }
    }
}