using System.IO;
using Toolkit.Text;

namespace Toolkit.Tools
{
    class ToolkitCommandLine
    {
        public static async void UnpackARC(string ARC) {
            if (File.Exists(ARC) && Verification.VerifyMagicNumberCommon(ARC))
                await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                      $"\"{ARC}\"",
                      Path.GetDirectoryName(ARC),
                      100000);
        }
    }
}
