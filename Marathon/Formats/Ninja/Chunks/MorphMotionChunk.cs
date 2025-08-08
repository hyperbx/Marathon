using Marathon.IO;

namespace Marathon.Formats.Ninja.Chunks
{
    public class MorphMotionChunk : MotionChunk
    {
        public new const string ID = "NXMM"; // Ninja directX Motion Morph

        public MorphMotionChunk() { }

        public MorphMotionChunk(BinaryObjectReaderEx in_reader) : base(in_reader) { }

        public new void Read(BinaryObjectReaderEx in_reader)
        {
            base.Read(in_reader);
        }

        public new void Write(BinaryObjectWriterEx in_writer)
        {
            base.Write(in_writer);
        }

        public override string GetChunkID()
        {
            return ID;
        }
    }
}
