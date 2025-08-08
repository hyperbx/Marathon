using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class ChunkFactory
    {
        public static IChunk GetChunkByFourCC(BinaryObjectReaderEx in_reader, FourCC in_signature)
        {
            return in_signature.ToString() switch
            {
                InfoChunk.ID           => new InfoChunk(in_reader),
                TextureListChunk.ID    => new TextureListChunk(in_reader),
                EffectListChunk.ID     => new EffectListChunk(in_reader),
                NodeNameChunk.ID       => new NodeNameChunk(in_reader),
                ObjectChunk.ID         => new ObjectChunk(in_reader),
                CameraChunk.ID         => new CameraChunk(in_reader),
                LightChunk.ID          => new LightChunk(in_reader),
                MotionChunk.ID         => new MotionChunk(in_reader),
                MaterialMotionChunk.ID => new MaterialMotionChunk(in_reader),
                CameraMotionChunk.ID   => new CameraMotionChunk(in_reader),
                LightMotionChunk.ID    => new LightMotionChunk(in_reader),
                MorphMotionChunk.ID    => new MorphMotionChunk(in_reader),
                MorphTargetChunk.ID    => new MorphTargetChunk(in_reader),
                OffsetChunk.ID         => new OffsetChunk(in_reader),
                FileNameChunk.ID       => new FileNameChunk(in_reader),
                EndChunk.ID            => new EndChunk(in_reader),
                _                      => new UndefinedChunk(in_reader)
            };
        }
    }
}
