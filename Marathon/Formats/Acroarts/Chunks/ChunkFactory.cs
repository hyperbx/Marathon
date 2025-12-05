using Marathon.IO.Types;
using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class ChunkFactory
    {
        public static IChunk GetChunkByFourCC(BINAReader in_reader, FourCC in_signature)
        {
            return in_signature.ToString() switch
            {
                AcroartsBinaryArchive.ID => new AcroartsBinaryArchive(in_reader),
                EndOfChunk.ID => new EndOfChunk(in_reader),
                _ => new UndefinedChunk(in_reader)
            };
        }
    }
}
