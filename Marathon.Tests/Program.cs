using Marathon.Extensions;
using Marathon.Formats.Archive;
using Marathon.Helpers;
using Marathon.IO.Types.FileSystem;
using System.Diagnostics;

namespace Marathon.Tests
{
    static class Program
    {
        public static string? GameDirectory { get; private set; }

        public static VirtualDirectory GameFileSystem { get; private set; } = new();

        public static List<string> RequestedTests { get; private set; } = [];

        public static string? Platform { get; private set; }

        public static DirectoryInfo? Temp { get; private set; }

        public static bool CancelOnTestFailure { get; private set; } = true;

        public static void Main(string[] args)
        {
            Console.Title = "Marathon Tests";

            Logger.Log("Marathon Tests\n");

            if (args.Length <= 0)
            {
                Logger.Error("No game directory specified.");
                Environment.ExitCode = -1;
                return;
            }

            GameDirectory = args[0];

            Logger.Log($"Game:          {GameDirectory}");

            if (File.Exists(Path.Combine(GameDirectory, "default.xex")))
            {
                Platform = "xenon";
            }
            else if (File.Exists(Path.Combine(GameDirectory, "EBOOT.BIN")))
            {
                Platform = "ps3";
            }
            else
            {
                Logger.Error("Unable to determine game platform.");
                Environment.ExitCode = -1;
                return;
            }

            Logger.Log($"Platform:      {(Platform == "xenon" ? "Xbox 360" : "PlayStation 3")}\n");

            if (args.Length > 1)
                RequestedTests.AddRange(args[1].Split(',', StringSplitOptions.RemoveEmptyEntries));

            Logger.Log("Indexing filesystem...");

            var indexTimer = Stopwatch.StartNew();

            foreach (var file in Directory.EnumerateFiles(GameDirectory, "*.arc", SearchOption.AllDirectories))
            {
                var arc = new ArcFile(file);

                foreach (var node in arc.EnumerateNodes())
                {
                    if (node.IsDirectory)
                    {
                        GameFileSystem.AddDirectory(node as IDirectory);
                    }
                    else
                    {
                        GameFileSystem.AddFile(node as IFile);
                    }
                }
            }

            indexTimer.Stop();

            Logger.Log($"Done. Took {indexTimer.Elapsed.FormatHoursMinutesSeconds()}.\n");

            var start = DateTime.Now;

            Logger.Log($"Start:         {start:dd/MM/yyyy hh:mm:ss.fff tt}\n");

            var result = true;

            Temp = Directory.CreateTempSubdirectory();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ITest).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
                .Reverse();

            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is not ITest test)
                    continue;

                var testName = $"{type.Namespace[15..]}.{type.Name}";

                if (RequestedTests.Count > 0 && !RequestedTests.Contains(testName))
                    continue;

                Logger.Log($"Test:          {testName}");

                try
                {
                    var testStart = DateTime.Now;

                    result = test.Run();

                    var testEnd = DateTime.Now;
                    var testDuration = testEnd - testStart;

                    Logger.Log($"├── Duration:  {testDuration.FormatHoursMinutesSeconds()}");

                    if (result)
                    {
                        Logger.Utility("└── Result:    PASS");
                    }
                    else
                    {
                        Logger.Error("└── Result:    FAIL\n");
                        break;
                    }
                }
                catch (NotImplementedException)
                {
                    Logger.Warning("└── Result:    Not implemented.");
                }

                Logger.Log("");
            }

            if (!result && Debugger.IsAttached)
                Debugger.Break();

            Temp.Delete(true);

            var end = DateTime.Now;
            var duration = end - start;

            Logger.Log($"End:           {end:dd/MM/yyyy hh:mm:ss.fff tt}");
            Logger.Log($"Duration:      {duration.FormatHoursMinutesSeconds()}\n");

            if (result)
            {
                Logger.Utility($"Result:        PASS");
            }
            else
            {
                Logger.Error($"Result:        FAIL");

                if (Debugger.IsAttached)
                    Debugger.Break();
            }

            Environment.ExitCode = result ? 0 : -1;
        }
    }
}
