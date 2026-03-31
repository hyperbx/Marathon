using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Extensions;
using Marathon.Formats.Acroarts;
using Marathon.Formats.Audio;
using Marathon.Formats.Event;
using Marathon.Formats.Kynapse;
using Marathon.Formats.Mesh;
using Marathon.Formats.Parameter;
using Marathon.Formats.Particle;
using Marathon.Formats.Placement;
using Marathon.Formats.Save;
using Marathon.Formats.Script;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Text;
using Marathon.Helpers;
using Marathon.IO;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;

Console.Title = "Marathon";

Console.WriteLine($"Marathon v{Assembly.GetExecutingAssembly().GetInformationalVersion()}\n");

#if !DEBUG
AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
{
    File.WriteAllText($"{Assembly.GetExecutingAssembly().GetAssemblyName()}.log", ((Exception)e.ExceptionObject).CreateLog());
};
#endif

// Force culture info to 'en-GB' to prevent errors with values being altered by culture-specific differences.
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

if (args.Length <= 0)
{
    Console.WriteLine
    (
        """
        Options:
        --endianness {big|little} - specify the endianness of the output file.
        --compression {none|fast|optimal} - specify the compression level for *.arc files.
        --indentation {tabs|spaces} - specify the indentation type for decompiled Lua scripts.
        --output "{path}" - specify the output path for the processed data (resets for next file if not a directory).
        --unattended - don't wait for keyboard input when a message is displayed.

        Usage: Marathon.CLI [options] [path]

        Press any key to continue...
        """
    );

    Console.ReadKey();

    return;
}

var isEndianness = false;
var endianness = Endianness.Big;

var isCompression = false;
var compression = CompressionLevel.Optimal;

var isIndentation = false;
var indentation = IndentationType.Tabs;

var isOutput = false;
var output = string.Empty;

var isUnattended = false;

void Log(string in_message, LogLevel in_logLevel = LogLevel.None)
{
    Logger.Log(in_message, in_logLevel, null);

    if (!isUnattended && in_logLevel == LogLevel.Error)
    {
        Logger.Log("\nPress any key to continue...");
        Console.ReadKey();
    }
}

for (int i = 0; i < args.Length; i++)
{
    var arg = args[i];

    switch (arg.ToLower())
    {
        case "--endianness":  isEndianness = true;  continue;
        case "--compression": isCompression = true; continue;
        case "--indentation": isIndentation = true; continue;
        case "--output":      isOutput = true;      continue;
        case "--unattended":  isUnattended = true;  continue;
    }

    if (isEndianness)
    {
        isEndianness = false;

        switch (arg.ToLower().Trim('"'))
        {
            case "big":    endianness = Endianness.Big;    continue;
            case "little": endianness = Endianness.Little; continue;
        }
    }

    if (isCompression)
    {
        isCompression = false;

        switch (arg.ToLower().Trim('"'))
        {
            case "none":    compression = CompressionLevel.NoCompression; continue;
            case "fast":    compression = CompressionLevel.Fastest;       continue;
            case "optimal": compression = CompressionLevel.Optimal;       continue;
        }
    }

    if (isIndentation)
    {
        isIndentation = false;

        switch (arg.ToLower().Trim('"'))
        {
            case "tabs":   indentation = IndentationType.Tabs;   continue;
            case "spaces": indentation = IndentationType.Spaces; continue;
        }
    }

    if (isOutput)
    {
        isOutput = false;
        output = arg;
        continue;
    }

    if (string.IsNullOrEmpty(output))
    {
        Log($"File: \"{arg}\"");
        output = null;
    }
    else
    {
        Log($"Source:      \"{arg}\"");
        Log($"Destination: \"{output}\"");
    }
    
    Console.WriteLine();

    if (File.Exists(arg))
    {
        void ExportFile<T>(string in_path, string in_importExtension = ".json", Func<T, bool>? in_callback = null) where T : FileBase, new()
        {
            var file = new T
            {
                Endianness = endianness
            };

            if (in_callback?.Invoke(file) == false)
                return;

            if (Path.GetExtension(in_path) == in_importExtension)
            {
                file.Import(arg);
                file.Write(output ?? FileSystemHelper.TruncateLastExtension(arg));
                return;
            }

            try
            {
                file.Read(arg);
                file.Export(output);
            }
            catch (InvalidSignatureException)
            {
                Log("Invalid file format.", LogLevel.Error);
            }
        }

        var extension = '.' + string.Join('.', FileSystemHelper.GetAllExtensions(arg));
        var success = true;

        switch (extension.ToLower())
        {
            case ".ddm":
                ExportFile<DirectDrawMap>(arg);
                break;

            case ".sbk":
            case ".sbk.json":
                ExportFile<SoundBank>(arg);
                break;

            case ".epb":
            case ".epb.json":
                ExportFile<EventPlaybook>(arg);
                break;

            case ".tev":
            case ".tev.json":
                ExportFile<TimeEvent>(arg);
                break;

            case ".kbf":
            case ".kbf.json":
                ExportFile<KynapseBigFile>(arg);
                break;

            case ".bin":
            case ".bin.json":
            case ".bin.obj":
            {
                void ExportGenericFile(string in_type)
                {
                    switch (in_type)
                    {
                        case "collision":         ExportFile<LandCollision>(arg, ".obj");        break;
                        case "ScriptParameter":   ExportFile<EnemyParameterList>(arg);           break;
                        case "ShotParameter":     ExportFile<EnemyShotParameterList>(arg);       break;
                        case "Explosion":         ExportFile<ObjectExplosionParameterList>(arg); break;
                        case "Common":            ExportFile<ObjectPhysicsParameterList>(arg);   break;
                        case "PathObj":           ExportFile<PathObjParameterList>(arg);         break;
                        case "SonicNextSaveData": ExportFile<SaveData>(arg);                     break;

                        default:
                        {
                            Console.WriteLine
                            (
                                """
                                The file type could not be determined automatically, please specify:

                                1. Land Collision (collision.bin)
                                2. Enemy Parameter List (ScriptParameter.bin)
                                3. Enemy Shot Parameter List (ShotParameter.bin)
                                4. Object Explosion Parameter List (Explosion.bin)
                                5. Object Physics Parameter List (Common.bin)
                                6. Path Obj Parameter List (PathObj.bin)
                                7. Save Data (SonicNextSaveData.bin)

                                """
                            );

                            var type = string.Empty;

                            switch (Console.ReadKey().KeyChar)
                            {
                                case '1': type = "collision";         break;
                                case '2': type = "ScriptParameter";   break;
                                case '3': type = "ShotParameter";     break;
                                case '4': type = "Explosion";         break;
                                case '5': type = "Common";            break;
                                case '6': type = "PathObj";           break;
                                case '7': type = "SonicNextSaveData"; break;
                            }

                            Console.WriteLine('\n');

                            ExportGenericFile(type);

                            break;
                        }
                    }
                }

                ExportGenericFile(Path.GetFileNameWithoutExtension(arg));

                break;
            }

            case ".rab":
            case ".rab.json":
                ExportFile<ReflectionArea>(arg);
                break;

            case ".path":
            case ".path.json":
                ExportFile<SplinePath>(arg);
                break;

            case ".pkg":
            case ".pkg.json":
                ExportFile<Package>(arg);
                break;

            case ".plc":
            case ".plc.json":
                ExportFile<ParticleContainer>(arg);
                break;

            case ".peb":
            case ".peb.json":
                ExportFile<ParticleEffectBank>(arg);
                break;

            case ".pgs":
            case ".pgs.json":
                ExportFile<ParticleGlobalSettings>(arg);
                break;

            case ".ptb":
            case ".ptb.json":
                ExportFile<ParticleTextureBank>(arg);
                break;

            case ".prop":
            case ".prop.json":
                ExportFile<PropLibrary>(arg);
                break;

            case ".set":
            case ".set.hson":
            {
                ExportFile<StageSet>(arg, ".hson", (x) =>
                {
                    var templatesPath = "./Resources/Templates.json";

                    if (!File.Exists(templatesPath))
                    {
                        Log($"Could not find \"{templatesPath}\" for stage set exporting.", LogLevel.Error);
                        return false;
                    }

                    x.AddTemplatesFromFile(templatesPath);

                    return true;
                });

                break;
            }

            case ".lub":
            {
                var lub = new LuaBinary(arg)
                {
                    IndentationType = indentation
                };

                lub.Export(output);

                break;
            }

            case string mst when mst.EndsWith(".mst"):
            case string mstJson when mstJson.EndsWith(".mst.json"):
                ExportFile<TextBook>(arg);
                break;

            case ".ftm":
            case ".ftm.json":
                ExportFile<TextFontMap>(arg);
                break;

            case ".pft":
            case ".pft.json":
                ExportFile<TextFontPicture>(arg);
                break;

            case ".pfi":
            case ".pfi.json":
                ExportFile<TextFontProportion>(arg);
                break;

            default:
                Log("Unsupported file type.", LogLevel.Error);
                success = false;
                break;
        }

        if (success)
            Logger.Utility("The file was exported successfully.");
    }
    else if (Directory.Exists(arg))
    {
        Log("Archives are not implemented yet.", LogLevel.Error);
    }
    else
    {
        Log("File does not exist.", LogLevel.Error);
    }

    // Reset output path if it's not a directory.
    if (File.Exists(output))
        output = string.Empty;
}
