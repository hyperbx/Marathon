using System;
using Marathon.Formats.Audio;
using Marathon.Formats.Event;
using Marathon.Formats.Package;
using Marathon.Formats.Particle;
using Marathon.Formats.Placement;
using Marathon.Formats.Text;
using Marathon.Helpers;

namespace Marathon.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine
            (
                "Marathon\n" +
                "Written by Hyper and Knux\n"
            );

            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    Console.WriteLine($"File: {arg}\n");

                    switch (StringHelper.GetFullExtension(arg))
                    {
                        case ".bin":
                        case ".bin.json":
                        {
                            Console.WriteLine
                            (
                                "This file is of a generic type, please specify what format it is;\n" +
                                "1. Common Package (Common.bin)\n" +
                                "2. Explosion Package (Explosion.bin)\n" +
                                "3. Path Package (PathObj.bin)\n" +
                                "4. Script Package (ScriptParameter.bin)\n" +
                                "5. Shot Package (ShotParameter.bin)"
                            );

                            switch (Console.ReadKey().KeyChar)
                            {
                                case '1':
                                    CommonPackage common = new(arg, true);
                                    break;

                                case '2':
                                    ExplosionPackage explosion = new(arg, true);
                                    break;

                                case '3':
                                    PathPackage pathObj = new(arg, true);
                                    break;

                                case '4':
                                    ScriptPackage scriptParameter = new(arg, true);
                                    break;

                                case '5':
                                    ShotPackage shotParameter = new(arg, true);
                                    break;
                            }

                            break;
                        }

                        case ".sbk":
                        case ".sbk.json":
                            SoundBank sbk = new(arg, true);
                            break;

                        case ".epb":
                        case ".epb.json":
                            EventPlaybook epb = new(arg, true);
                            break;

                        case ".tev":
                        case ".tev.json":
                            TimeEvent tev = new(arg, true);
                            break;

                        case ".pkg":
                        case ".pkg.json":
                            AssetPackage pkg = new(arg, true);
                            break;

                        case ".plc":
                        case ".plc.json":
                            ParticleContainer plc = new(arg, true);
                            break;

                        case ".peb":
                        case ".peb.json":
                            ParticleEffectBank peb = new(arg, true);
                            break;

                        case ".pgs":
                        case ".pgs.json":
                            ParticleGenerationSystem pgs = new(arg, true);
                            break;

                        case ".ptb":
                        case ".ptb.json":
                            ParticleTextureBank ptb = new(arg, true);
                            break;

                        case ".set":
                        case ".set.json":
                            ObjectPlacement set = new(arg, true);
                            break;

                        case ".prop":
                        case ".prop.json":
                            ObjectPropertyDatabase prop = new(arg, true);
                            break;

                        case string mstEx when mstEx.EndsWith(".mst"):
                        case string mstJsonEx when mstJsonEx.EndsWith(".mst.json"):
                            MessageTable mst = new(arg, true);
                            break;

                        case ".pft":
                        case ".pft.json":
                            PictureFont pft = new(arg, true);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Audio:");
                Console.WriteLine("- Sound Bank (*.sbk)\n");

                Console.WriteLine("Event:");
                Console.WriteLine("- Event Playbook (*.epb)");
                Console.WriteLine("- Time Event (*.tev)\n");

                Console.WriteLine("Package:");
                Console.WriteLine("- Asset Package (*.pkg)");
                Console.WriteLine("- Common Package (Common.bin)");
                Console.WriteLine("- Explosion Package (Explosion.bin)");
                Console.WriteLine("- Path Package (PathObj.bin)");
                Console.WriteLine("- Script Package (ScriptParameter.bin)");
                Console.WriteLine("- Shot Package (ShotParameter.bin)\n");

                Console.WriteLine("Particle:");
                Console.WriteLine("- Particle Container (*.plc)");
                Console.WriteLine("- Particle Effect Bank (*.peb)");
                Console.WriteLine("- Particle Generation System (*.pgs)");
                Console.WriteLine("- Particle Texture Bank (*.ptb)\n");

                Console.WriteLine("Placement:");
                Console.WriteLine("- Object Placement (*.set)");
                Console.WriteLine("- Object Property Database (*.prop)\n");

                Console.WriteLine("Text:");
                Console.WriteLine("- Message Table (*.mst)");
                Console.WriteLine("- Picture Font (*.pft)\n");

                Console.WriteLine
                (
                    "Usage:\n" +
                    "Marathon.CLI.exe \"some_supported_file_format.pkg\" ...\n" +
                    "Marathon.CLI.exe \"some_supported_serialised_format.pkg.json\" ...\n"
                );

                Console.WriteLine("Press any key to continue...");

                Console.ReadKey();
            }
        }
    }
}