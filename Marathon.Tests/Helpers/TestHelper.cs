using Marathon.Extensions;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Types.FileSystem;
using System.IO.Enumeration;

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

                Logger.Log($"│    ├── Duration:  {testDuration.FormatHoursMinutesSeconds()}");

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

            using var file = in_file.Decompress();
            out_file.Read(file);

            using var compareStream = new MemoryStream();
            out_file.Write(compareStream);

            file.BaseStream.Position = 0;
            compareStream.Position = 0;

            var oldHash = HashHelper.ComputeStreamXxHash3(file.BaseStream);
            var newHash = HashHelper.ComputeStreamXxHash3(compareStream);

            return oldHash == newHash;
        }

        public static bool CheckAllBinaries<T>(string in_searchPattern, List<string> in_ignoreList = null, string in_ignorePattern = "") where T : FileBase, new()
        {
            var result = true;
            var shouldCancel = Program.CancelOnTestFailure;
            var files = Program.GameFileSystem.EnumerateFiles(in_searchPattern, SearchOption.AllDirectories);
            var i = 0;

            foreach (var file in files)
            {
                if (FileSystemName.MatchesSimpleExpression(in_ignorePattern, file.Path))
                    continue;

                var fileCount = files.Count();

                if (in_ignoreList != null)
                {
                    fileCount -= in_ignoreList.Count;

                    if (in_ignoreList.Contains(file.Name))
                        continue;
                }

                if (result && i > 0)
                    ConsoleHelper.ReturnToPreviousLine(2);

                Logger.Log($"│    ├── File:      {file.Path}");
                Logger.Log($"│    └── Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})");

                if (!(result = CheckBinary<T>(file, out var out_exhibit)))
                {
                    var exhibitPath = CreateExhibit(file.Path, out_exhibit);

                    ConsoleHelper.ReturnToPreviousLine(shouldCancel ? 1 : 2);
                    Logger.Error($"│    ├── Exhibit:   {exhibitPath}");

                    if (shouldCancel)
                        return result;
                }

                i++;
            }

            ConsoleHelper.ReturnToPreviousLine(2);

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
