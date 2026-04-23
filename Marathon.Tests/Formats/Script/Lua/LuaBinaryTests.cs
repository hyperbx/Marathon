using Marathon.Extensions;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Helpers;
using Marathon.Tests.Helpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Marathon.Tests.Formats.Script.Lua
{
    internal class LuaBinaryTests : ITest
    {
        private Func<bool>[] _tests = [ValidDecompilationTest];

        private static bool ValidDecompilationTest()
        {
            var result = true;
            var files = Program.GameFileSystem.EnumerateFiles("*.lub", SearchOption.AllDirectories);
            var i = 0;

            var ignoreList = new List<string>()
            {
                // Bad decompilations.
                "object.lub",
                "render_gamemode_multi.lub",
                "standard.lub",

                // Contains Shift-JIS.
                "test_object_dtd.lub",
                "stageselect.lub"
            };

            foreach (var file in files)
            {
                if (ignoreList.Contains(file.Name))
                    continue;

                var fileCount = files.Count() - ignoreList.Count;

                Logger.Log($"│    ├── File:      {file.Path}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                using var uncompressedFile = file.Decompress();

                var lub = new LuaBinary(uncompressedFile);
                var dec = "";
                var decException = false;

                try
                {
                    lub.LoadSymbols(JsonConvert.DeserializeObject<List<Symbol>>(Encoding.UTF8.GetString(Resources.Symbols)));

                    dec = lub.Decompile(new SymbolResolverOptions(file.Name));
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

                    var output = luacheck.StandardOutput.ReadToEnd().SplitLineBreaks();

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
                        var exhibitPath = TestHelper.CreateExhibit(file.Path, lub,
                            (exhibit, path) => File.WriteAllText(path, exhibit.Decompile()));

                        ConsoleHelper.ReturnToPreviousLine();
                        Logger.Error($"│    └── Exhibit:   {exhibitPath}");

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
