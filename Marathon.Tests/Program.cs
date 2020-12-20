using Marathon.IO.Formats.Meshes;
using Marathon.IO.Formats.Particles;
using Marathon.IO.Formats.Placement;
using Marathon.IO.Formats.Archives;
using System.IO;
using System.Collections.Generic;

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
            string[] x360Arcs = Directory.GetFiles(@"G:\Sonic '06\Game Dump", "*.arc", SearchOption.AllDirectories);
            string[] ps3Arcs = Directory.GetFiles(@"G:\Sonic '06\PS3 Game Dump", "*.arc", SearchOption.AllDirectories);
            List<string> filetypes = new List<string>();
            filetypes.Add(".arc");

            foreach(string arcFile in x360Arcs)
            {
                System.Console.WriteLine(arcFile);
                U8Archive arc = new U8Archive(arcFile);
                List<IO.Formats.ArchiveFile> files = arc.GetFiles();
                foreach(IO.Formats.ArchiveFile file in files)
                {
                    if (!filetypes.Contains(Path.GetExtension(file.Name)))
                    {
                        filetypes.Add(Path.GetExtension(file.Name));
                    }
                }
            }

            foreach (string arcFile in ps3Arcs)
            {
                System.Console.WriteLine(arcFile);
                U8Archive arc = new U8Archive(arcFile);
                List<IO.Formats.ArchiveFile> files = arc.GetFiles();
                foreach (IO.Formats.ArchiveFile file in files)
                {
                    if (!filetypes.Contains(Path.GetExtension(file.Name)))
                    {
                        filetypes.Add(Path.GetExtension(file.Name));
                    }
                }
            }

            filetypes.Sort();
            foreach(string filetype in filetypes)
            {
                System.Console.WriteLine(filetype);
            }
        }
    }
}