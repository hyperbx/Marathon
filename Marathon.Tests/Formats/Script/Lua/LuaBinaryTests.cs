using Marathon.Extensions;
using Marathon.Formats.Script.Lua;
using Marathon.Helpers;
using Marathon.IO.Types.FileSystem;
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
            var nodes = Program.GameFileSystem.GetNodes("*.lub", true).Where(x => !x.IsDirectory);
            var i = 0;

            // Known bad decompilations.
            var ignoreList = new List<string>()
            {
                "object.lub",
                "render_gamemode_multi.lub",
                "actionarea.lub",
                "actionstage.lub"
            };

            foreach (var node in nodes)
            {
                var file = node as IFile;

                if (ignoreList.Contains(node.Name))
                    continue;

                var fileCount = nodes.Count() - ignoreList.Count;

                Logger.Log($"│    ├── File:      {node.Path}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                var lub = new LuaBinary();
                var dec = "";
                var decException = false;

                if (file.Decompress?.Invoke(file) == false)
                    goto OnError;

                lub.Read(file.Open());

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
                        var exhibitPath = TestHelper.CreateExhibit(node.Path, lub,
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
