using Marathon.IO.Formats.Textures;

namespace Marathon.Tests
{
    /// <summary>
    /// <para>A simple command-line application used for testing APIs.</para>
    /// <para>If any code is left over here from a previous commit, feel free to erase it.</para>
    /// </summary>
    class Program
    {
        static void Main()
        {
            DirectDrawMatrix DDM = new DirectDrawMatrix();
            DDM.Load(@"C:\Users\gabe1\AppData\Local\Hyper_Development_Team\Sonic '06 Toolkit\Archives\23325\1qmosoc4.ck1\enemy\win32\enemy\eSearcher\eSearcher.ddm");
        }
    }
}