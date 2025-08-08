using Marathon.Formats.Mesh.Ninja.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class MorphTarget
    {
        public uint Target { get; set; }

        public VertexList VertexList { get; set; }

        public MorphTarget() { }

        public MorphTarget(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Target = in_reader.Read<uint>();
            var morphTargetOffset = in_reader.Read<uint>();

            var pos = in_reader.Position;

            in_reader.JumpTo(InfoChunk.Size + morphTargetOffset);

            var unkField = in_reader.Read<uint>(); // TODO: unknown - always 2?
            var vertexListOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + vertexListOffset);

            VertexList = new VertexList(in_reader, true);

            in_reader.JumpTo(pos);
        }
    }
}
