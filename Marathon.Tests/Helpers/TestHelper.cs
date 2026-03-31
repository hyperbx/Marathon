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

            TreeLogger.Log();

            foreach (var test in in_tests)
            {
                TreeLogger.Log($"Test:          {test.Method.Name}", 1, TreeLogger.NodeType.Root);

                try
                {
                    var testStart = DateTime.Now;

                    result = test();

                    var testEnd = DateTime.Now;
                    var testDuration = testEnd - testStart;

                    TreeLogger.Log($"Duration:  {testDuration.FormatHoursMinutesSeconds()}", 1);

                    if (result)
                    {
                        TreeLogger.Utility($"Result:    PASS", 1, TreeLogger.NodeType.End);
                    }
                    else
                    {
                        TreeLogger.Error($"Result:    FAIL", 1, TreeLogger.NodeType.End);

                        if (in_cancelOnFail)
                        {
                            TreeLogger.Log();
                            break;
                        }
                    }
                }
                catch (NotImplementedException)
                {
                    TreeLogger.Warning("Result:    Not implemented.", 1, TreeLogger.NodeType.End);
                }

                TreeLogger.Log();
            }

            return result;
        }

        public static bool CheckBinary<T>(IFile in_file, out T out_file) where T : FileBase, new()
        {
            out_file = new T();
            out_file.Read(in_file);

            using var compareStream = new MemoryStream();
            out_file.Write(compareStream);

            in_file.BaseStream.Position = 0;
            compareStream.Position = 0;

            var oldHash = HashHelper.ComputeStreamXxHash3(in_file.BaseStream);
            var newHash = HashHelper.ComputeStreamXxHash3(compareStream);

            return oldHash == newHash;
        }

        public static bool CheckAllBinaries<T>(string in_searchPattern, List<string> in_ignoreList = null, string in_ignorePattern = "") where T : FileBase, new()
        {
            var result = true;
            var files = Program.GameFileSystem.GetFiles(in_searchPattern, SearchOption.AllDirectories);
            var fileCount = files.Length;

            for (int i = 0; i < fileCount; i++)
            {
                var file = files[i];

                if (FileSystemName.MatchesSimpleExpression(in_ignorePattern, file.Path))
                    continue;

                if (in_ignoreList != null)
                {
                    fileCount -= in_ignoreList.Count;

                    if (in_ignoreList.Contains(file.Name))
                        continue;
                }

                if (result && i > 0)
                    ConsoleHelper.ReturnToPreviousLine(2);

                TreeLogger.Log($"File:      {file.Path}", 1);
                TreeLogger.Log($"Progress:  {((float)i / (float)fileCount):P0} ({i} / {fileCount})", 1, TreeLogger.NodeType.End);

                var decompressedFile = file.Decompress();

                if (!(result = CheckBinary<T>(decompressedFile, out var out_exhibit)))
                {
                    var exhibitPath = CreateExhibit(file.Path, out_exhibit);

                    ConsoleHelper.ReturnToPreviousLine(Program.CancelOnTestFailure ? 1 : 2);
                    TreeLogger.Error($"Exhibit:   {exhibitPath}", 1);

                    if (Program.CancelOnTestFailure)
                        return result;
                }
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
