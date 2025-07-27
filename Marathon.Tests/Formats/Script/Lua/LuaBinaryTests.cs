using Marathon.Extensions;
using Marathon.Formats.Script.Lua;
using Marathon.Helpers;
using Marathon.Tests.Helpers;
using System.Diagnostics;

namespace Marathon.Tests.Formats.Script.Lua
{
    internal class LuaBinaryTests : ITest
    {
        private Func<bool>[] _tests = [ValidDecompilationTest];

        private static bool ValidDecompilationTest()
        {
            var result = true;
            var files = Directory.GetFiles(Program.GameDirectory, "*.lub", SearchOption.AllDirectories);
            var i = 0;

            // Known bad decompilations.
            var ignoreList = new List<string>()
            {
                "object.lub",
                "render_gamemode_multi.lub",
                "actionarea.lub",
                "actionstage.lub"
            };

            foreach (var file in files)
            {
                if (ignoreList.Contains(Path.GetFileName(file)))
                    continue;

                var fileCount = files.Length - ignoreList.Count;

                Logger.Log($"│    ├── File:      {file[(Program.GameDirectory.Length + 1)..]}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                var lub = new LuaBinary(file);
                var dec = "";
                var decException = false;

                try
                {
                    dec = lub.Decompile();
                }
                catch
                {
                    result = false;
                    decException = true;
                    goto OnError;
                }

                var luacheck = Process.Start
                (
                    new ProcessStartInfo
                    {
                        FileName = Path.Combine(Environment.CurrentDirectory, "Tools", "luacheck.exe"),
                        Arguments = $"-g -u -a -",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                );

                if (luacheck == null)
                {
                    result = false;
                }
                else
                {
                    luacheck.StandardInput.Write(dec);
                    luacheck.StandardInput.Close();

                    var output = luacheck.StandardOutput.ReadToEnd().ParseLineBreaks();

                    foreach (var line in output)
                    {
                        if (!line.StartsWith("Total: "))
                            continue;

                        var split = line.Split('/');
                        var right = split[1].TrimStart(' ');
                        var error = Convert.ToInt32(right[..right.IndexOf(' ')]);

                        if (error > 0)
                        {
                            result = false;
                            break;
                        }    
                    }

                    luacheck.WaitForExit();
                }

            OnError:
                if (!result)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    if (!decException)
                    {
                        var badFile = $"{file}.bad";

                        File.WriteAllText(badFile, lub.Decompile());

                        ConsoleHelper.ReturnToPreviousLine();
                        Logger.Error($"│    └── Exhibit:   {badFile[(Program.GameDirectory.Length + 1)..]}");

                        if (Debugger.IsAttached)
                            Debugger.Break();
                    }

                    ConsoleHelper.ReturnToPreviousLine(2);

                    break;
                }

                ConsoleHelper.ReturnToPreviousLine(2);

                i++;
            }

            return result;
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
