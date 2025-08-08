using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class ChunkFactory
    {
        public static IChunk GetChunkByFourCC(BinaryObjectReaderEx in_reader, FourCC in_signature)
        {
            switch (in_signature.ToString())
            {
                case InfoChunk.ID:
                    return new InfoChunk(in_reader);

                case TextureListChunk.ID:
                    return new TextureListChunk(in_reader);

                case EffectListChunk.ID:
                    return new EffectListChunk(in_reader);

                case NodeNameChunk.ID:
                    return new NodeNameChunk(in_reader);

                case ObjectChunk.ID:
                    return new ObjectChunk(in_reader);

                case CameraChunk.ID:
                    return new CameraChunk(in_reader);

                case LightChunk.ID:
                    return new LightChunk(in_reader);

                case MotionChunk.ID:
                    return new MotionChunk(in_reader);

                case MaterialMotionChunk.ID:
                    return new MaterialMotionChunk(in_reader);

                case CameraMotionChunk.ID:
                    return new CameraMotionChunk(in_reader);

                case LightMotionChunk.ID:
                    return new LightMotionChunk(in_reader);

                case MorphMotionChunk.ID:
                    return new MorphMotionChunk(in_reader);

                case MorphTargetChunk.ID:
                    return new MorphTargetChunk(in_reader);

                case OffsetChunk.ID:
                    return new OffsetChunk(in_reader);

                case FileNameChunk.ID:
                    return new FileNameChunk(in_reader);

                case EndChunk.ID:
                    return new EndChunk(in_reader);
            }

            return new UndefinedChunk(in_reader);
        }
    }
}
