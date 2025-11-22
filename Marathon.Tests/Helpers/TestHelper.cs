using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Types.FileSystem;
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

        public static bool CheckBinary<T>(IFile in_file, out T out_file) where T : FileBase, new()
        {
            out_file = new T();

            if (in_file.Decompress?.Invoke(in_file) == false)
                return false;

            var fileStream = in_file.Open();

            out_file.Read(fileStream);

            using var compareStream = new MemoryStream();
            out_file.Write(compareStream);

            fileStream.Position = 0;
            compareStream.Position = 0;

            var oldHash = HashHelper.ComputeStreamXxHash3(fileStream);
            var newHash = HashHelper.ComputeStreamXxHash3(compareStream);

            return oldHash == newHash;
        }

        public static bool CheckAllBinaries<T>(string in_searchPattern, List<string> in_ignoreList = null) where T : FileBase, new()
        {
            var result = true;
            var nodes = Program.GameFileSystem.GetNodes(in_searchPattern, true).Where(x => !x.IsDirectory);
            var i = 0;

            foreach (var node in nodes)
            {
                var file = node as IFile;
                var fileCount = nodes.Count();

                if (in_ignoreList != null)
                {
                    fileCount -= in_ignoreList.Count;

                    if (in_ignoreList.Contains(node.Name))
                        continue;
                }

                Logger.Log($"│    ├── File:      {node.Path}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                if (!CheckBinary<T>(file, out var out_exhibit))
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    var exhibitPath = CreateExhibit(node.Path, out_exhibit);

                    ConsoleHelper.ReturnToPreviousLine();
                    Logger.Error($"│    └── Exhibit:   {exhibitPath}");

                    if (Debugger.IsAttached)
                        Debugger.Break();

                    break;
                }

                ConsoleHelper.ReturnToPreviousLine(2);

                i++;
            }

            return result;
        }

        public static string CreateExhibit<T>(string in_path, T in_exhibit, Action<T, string> in_creator = null) where T : FileBase
        {
            var path = Path.Combine(Program.Temp.FullName, in_path);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (in_creator == null)
            {
                in_exhibit.Write(path);
            }
            else
            {
                in_creator(in_exhibit, path);
            }

            return path;
        }
    }
}
