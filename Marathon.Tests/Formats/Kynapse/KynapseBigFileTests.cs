using Marathon.Formats.Kynapse;
using Marathon.Formats.Kynapse.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.Tests.Helpers;

namespace Marathon.Tests.Formats.Kynapse
{
    internal class KynapseBigFileTests : ITest
    {
        private Func<bool>[] _tests =
        [
            BinaryIdenticalTest,
            BinaryIdenticalTest_KynogonAstarData,
            BinaryIdenticalTest_KynogonFindNearestData,
            BinaryIdenticalTest_KynogonMesh,
            BinaryIdenticalTest_KynogonPathCostData,
            BinaryIdenticalTest_KynogonPathWay,
            BinaryIdenticalTest_KynogonSpatialGraph
        ];

        private static bool BinaryIdenticalTest()
        {
            return TestHelper.CheckAllBinaries<KynapseBigFile>("*.kbf");
        }

        private static bool BinaryIdenticalTest_KynogonAstarData()
        {
            return BinaryIdenticalTest_Internal<KynogonAstarData>(".adl");
        }

        private static bool BinaryIdenticalTest_KynogonFindNearestData()
        {
            return BinaryIdenticalTest_Internal<KynogonFindNearestData>(".fdl");
        }

        private static bool BinaryIdenticalTest_KynogonMesh()
        {
            throw new NotImplementedException();
        }

        private static bool BinaryIdenticalTest_KynogonPathCostData()
        {
            throw new NotImplementedException();
        }

        private static bool BinaryIdenticalTest_KynogonPathWay()
        {
            return BinaryIdenticalTest_Internal<KynogonPathWay>(".pwl");
        }

        private static bool BinaryIdenticalTest_KynogonSpatialGraph()
        {
            throw new NotImplementedException();
        }

        private static bool BinaryIdenticalTest_Internal<T>(string in_extension) where T : FileBase, new()
        {
            var result = true;

            foreach (var file in Program.GameFileSystem.EnumerateFiles("*.kbf", SearchOption.AllDirectories))
            {
                if (!result && Program.CancelOnTestFailure)
                    break;

                using var decompressedFile = file.Decompress();
                using var kbf = new KynapseBigFile(decompressedFile);
                int i = 0;

                TreeLogger.Log($"File:      {file.Name}", 1);

                kbf.WalkElements((element, type) =>
                {
                    if (!result && Program.CancelOnTestFailure)
                        return false;

                    if (type != KynapseElementType.RawData || element.GetRawDataExtension() != in_extension)
                        return true;

                    if (result && i > 0)
                        ConsoleHelper.ReturnToPreviousLine();

                    var fileName = element.GetRawDataFileName();

                    TreeLogger.Log($"File:  {fileName}", 2, TreeLogger.NodeType.End);

                    var decompressedFile = file.Decompress();

                    if (!(result = TestHelper.CheckBinary<T>(element.File, out var out_exhibit)))
                    {
                        var exhibitPath = TestHelper.CreateExhibit(fileName, out_exhibit);

                        ConsoleHelper.ReturnToPreviousLine();
                        TreeLogger.Error($"Exhibit:   {exhibitPath}", 2, TreeLogger.NodeType.End);
                    }

                    i++;

                    return true;
                });

                if (result)
                    ConsoleHelper.ReturnToPreviousLine(2);
            }

            return result;
        }

        public bool Run()
        {
            return TestHelper.RunSubTests(_tests);
        }
    }
}
