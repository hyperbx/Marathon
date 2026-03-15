using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class Sound3DPlay : SoundPlay
    {
        public Sound3DPlay() { }

        public Sound3DPlay(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
            : base(in_reader, in_parentChunk) { }
    }
}
