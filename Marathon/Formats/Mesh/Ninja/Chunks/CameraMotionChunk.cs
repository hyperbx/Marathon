using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class CameraMotionChunk : MotionChunk
    {
        public new const string ID = "NXMC";

        public CameraMotionChunk() { }

        public CameraMotionChunk(BinaryObjectReaderEx in_reader) : base(in_reader) { }

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
