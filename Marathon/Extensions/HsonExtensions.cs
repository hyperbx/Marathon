using libHSON;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Marathon.Extensions
{
    public static class HsonExtensions
    {
        public static string ToJsonString(this Project in_project, ProjectWriteOptions in_hsonOptions = default, JsonWriterOptions in_jsonOptions = default)
        {
            using var ms = new MemoryStream();

            in_project.Write(ms, jsonOptions: new() { Indented = true });

            return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
        }
    }
}
