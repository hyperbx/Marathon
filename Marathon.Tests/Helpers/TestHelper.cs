using Marathon.Helpers;
using Marathon.IO;
using System.Diagnostics;

namespace Marathon.Tests.Helpers
{
    internal class TestHelper
    {
        public static bool RunSubTests(Func<bool>[] in_tests, bool in_cancelOnFail = true)
        {
            var result = true;

            Logger.Log("│");

            foreach (var test in in_tests)
            {
                Logger.Log($"│    Test:          {test.Method.Name}");

                var testStart = DateTime.Now;

                result = test();

                var testEnd = DateTime.Now;
                var testDuration = testEnd - testStart;

                Logger.Log($"│    ├── Duration:  {testDuration.TotalMilliseconds} ms");

                if (result)
                {
                    Logger.Utility($"│    └── Result:    PASS");
                }
                else
                {
                    Logger.Error($"│    └── Result:    FAIL");

                    if (in_cancelOnFail)
                    {
                        Logger.Log("│");
                        break;
                    }
                }

                Logger.Log("│");
            }

            return result;
        }

        public static bool CheckAllBinaries<T>(string in_searchPattern, List<string> in_ignoreList = null) where T : FileBase, new()
        {
            var result = true;
            var files = Directory.GetFiles(Program.GameDirectory, in_searchPattern, SearchOption.AllDirectories);
            var i = 0;

            foreach (var file in files)
            {
                var fileCount = files.Length;

                if (in_ignoreList != null)
                {
                    fileCount -= in_ignoreList.Count;

                    if (in_ignoreList.Contains(Path.GetFileName(file)))
                        continue;
                }

                Logger.Log($"│    ├── File:      {file[(Program.GameDirectory.Length + 1)..]}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                var bin = new T();
                bin.Read(file);

                using (var ms = new MemoryStream())
                {
                    bin.Write(ms);

                    var oldHash = HashHelper.ComputeFileXxHash3(file);
                    var newHash = HashHelper.ComputeBufferXxHash3(ms.ToArray());

                    result = oldHash == newHash;
                }

                if (!result)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    var badFile = $"{file}.bad";

                    bin.Write(badFile);

                    ConsoleHelper.ReturnToPreviousLine();
                    Logger.Error($"│    └── Exhibit:   {badFile[(Program.GameDirectory.Length + 1)..]}");

                    if (Debugger.IsAttached)
                        Debugger.Break();

                    break;
                }

                ConsoleHelper.ReturnToPreviousLine(2);

                i++;
            }

            return result;
        }
    }
}
