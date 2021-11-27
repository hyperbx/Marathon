using Marathon.Formats.Archive;
using Marathon.Formats.Audio;
using Marathon.Formats.Event;
using Marathon.Formats.Mesh;
using Marathon.Formats.Package;
using Marathon.Formats.Particle;
using Marathon.Formats.Placement;
using Marathon.Formats.Save;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Text;
using Marathon.Helpers;
using Marathon.Shared;
using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Marathon.CLI
{
    class Program
    {
        internal static CompressionLevel _compressionLevel { get; set; } = CompressionLevel.Optimal;

        static void Main(string[] args)
        {
            Console.Title = "Marathon Command Line";

            Console.WriteLine
            (
                $"Marathon - Version {AssemblyExtensions.GetInformationalVersion()}\n\n" +
                "" +
                "All your '06 formats are belong to us.\n"
            );

#if !DEBUG
            // Log to file if an unhandled exception occurs.
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                File.WriteAllText($"{AssemblyExtensions.GetAssemblyName()}.log", ((Exception)e.ExceptionObject).CreateLog());
            };
#endif

            // Force culture info 'en-GB' to prevent errors with values altered by culture-specific differences.
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    // Set compression level.
                    switch (arg)
                    {
                        case "--no-compression":
                            _compressionLevel = CompressionLevel.NoCompression;
                            continue;

                        case "--fast-compression":
                            _compressionLevel = CompressionLevel.Fastest;
                            continue;

                        case "--optimal-compression":
                            _compressionLevel = CompressionLevel.Optimal;
                            continue;
                    }

                    if (Directory.Exists(arg))
                    {
                        U8Archive arc = new(arg, true, _compressionLevel);
                        arc.Save(StringHelper.ReplaceFilename(arg, Path.GetFileName(arg) + arc.Extension));
                    }

                    if (File.Exists(arg))
                    {
                        Console.WriteLine($"File: {arg}\n");

                        // Get last extension for overwritable formats.
                        switch (Path.GetExtension(arg))
                        {
                            case ".arc":
                                U8Archive arc = new(arg, IO.ReadMode.IndexOnly);
                                arc.Extract(Path.Combine(Path.GetDirectoryName(arg), Path.GetFileNameWithoutExtension(arg)));
                                break;

                            case ".lub":
                                LuaBinary lub = new(arg, true);
                                break;
                        }

                        // Get full extension for serialisable formats.
                        switch (StringHelper.GetFullExtension(arg))
                        {
                            case ".bin":
                            case ".bin.json":
                            {
                                Console.WriteLine
                                (
                                    "This file is of a generic type, please specify what format it is;\n" +
                                    "1. Collision (collision.bin)\n" +
                                    "2. Common Package (Common.bin)\n" +
                                    "3. Explosion Package (Explosion.bin)\n" +
                                    "4. Path Package (PathObj.bin)\n" +
                                    "5. Save Data (SonicNextSaveData.bin)\n" +
                                    "6. Script Package (ScriptParameter.bin)\n" +
                                    "7. Shot Package (ShotParameter.bin)"
                                );

                                switch (Console.ReadKey().KeyChar)
                                {
                                    case '1':
                                        Collision collision = new(arg, true);
                                        break;

                                    case '2':
                                        CommonPackage common = new(arg, true);
                                        break;

                                    case '3':
                                        ExplosionPackage explosion = new(arg, true);
                                        break;

                                    case '4':
                                        PathPackage pathObj = new(arg, true);
                                        break;

                                    case '5':
                                        SonicNextSaveData saveData = new(arg, true);
                                        break;

                                    case '6':
                                        ScriptPackage scriptParameter = new(arg, true);
                                        break;

                                    case '7':
                                        ShotPackage shotParameter = new(arg, true);
                                        break;
                                }

                                // Pad with two line breaks.
                                Console.WriteLine('\n');

                                break;
                            }

                            case ".bin.obj":
                                Collision collisionOBJ = new(arg, true);
                                break;

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

                            case ".rab":
                            case ".rab.json":
                                ReflectionZone rab = new(arg, true);
                                break;

                            case ".set":
                            case ".set.json":
                                ObjectPlacement set = new(arg, true, !args.Contains("--no-index"));
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
            }
            else
            {
                Console.WriteLine("Arguments:");
                Console.WriteLine("--no-index - disables index display for serialised Object Placement data.");
                Console.WriteLine("--no-compression - writes U8 Archive files uncompressed.");
                Console.WriteLine("--fast-compression - writes U8 Archive files using fast Zlib compression.");
                Console.WriteLine("--optimal-compression - writes U8 Archive files using optimal Zlib compression.\n");

                Console.WriteLine("Archive:");
                Console.WriteLine("- U8 Archive (*.arc)\n");

                Console.WriteLine("Audio:");
                Console.WriteLine("- Sound Bank (*.sbk)\n");

                Console.WriteLine("Event:");
                Console.WriteLine("- Event Playbook (*.epb)");
                Console.WriteLine("- Time Event (*.tev)\n");

                Console.WriteLine("Mesh:");
                Console.WriteLine("- Collision (*.bin)");
                Console.WriteLine("- Reflection Zone (*.rab)\n");

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

                Console.WriteLine("Save:");
                Console.WriteLine("- Save Data (SonicNextSaveData.bin)\n");

                Console.WriteLine("Script:");
                Console.WriteLine("- Lua Bytecode (*.lub)\n");

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