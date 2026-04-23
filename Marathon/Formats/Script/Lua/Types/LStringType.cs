using Marathon.Extensions;
using Marathon.IO;
using System.Text;
using UtfUnknown;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class LStringType : BObjectType<LString>
    {
        public override LString Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var sizeT = in_header.SizeT.Parse(in_reader, in_header);
            var bytes = in_reader.ReadArray<byte>(sizeT.AsInt());

            if (bytes.Length <= 0)
                return new LString(sizeT, string.Empty);

            // FIX (Hyper): detect correct encoding from string bytes.
            // test_object_dtd.lub and stageselect.lub contain a lot of Shift-JIS
            // encoded strings, which result in garbage when run through UTF-8.
            var result = CharsetDetector.DetectFromBytes(bytes);
            var encoding = result.Detected?.Encoding;

            // HACK (Hyper): stageselect.lub has a few mangled Shift-JIS strings
            // that CharsetDetector doesn't work correctly with. As a last ditch
            // effort, we'll just force Shift-JIS anyway and see what happens.
            if (result.Details.Count <= 0)
                encoding = Encoding.ShiftJIS;

            return new LString(sizeT, encoding.GetString(bytes), encoding);
        }
    }
}
