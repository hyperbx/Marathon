// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public sealed class Op
    {
        private readonly OpcodeFormat _format;

        public string CodepointToString(int in_codepoint, CodeExtractor in_extractor)
        {
            return _format switch
            {
                OpcodeFormat.A => $"{ToString()} {in_extractor.ExtractA(in_codepoint)}",
                OpcodeFormat.AB => $"{ToString()} {in_extractor.ExtractA(in_codepoint)} {in_extractor.ExtractB(in_codepoint)}",
                OpcodeFormat.AC => $"{ToString()} {in_extractor.ExtractA(in_codepoint)} {in_extractor.ExtractC(in_codepoint)}",
                OpcodeFormat.ABC => $"{ToString()} {in_extractor.ExtractA(in_codepoint)} {in_extractor.ExtractB(in_codepoint)} {in_extractor.ExtractC(in_codepoint)}",
                OpcodeFormat.ABx => $"{ToString()} {in_extractor.ExtractA(in_codepoint)} {in_extractor.ExtractBx(in_codepoint)}",
                OpcodeFormat.AsBx => $"{ToString()} {in_extractor.ExtractA(in_codepoint)} {in_extractor.ExtractsBx(in_codepoint)}",
                OpcodeFormat.Ax => $"{ToString()} <Ax>",
                OpcodeFormat.sBx => $"{ToString()} {in_extractor.ExtractsBx(in_codepoint)}",
                _ => ToString(),
            };
        }
    }

}
