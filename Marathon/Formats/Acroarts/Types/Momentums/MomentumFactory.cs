using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MomentumFactory
    {
        public static IMomentumParamSet ReadMomentumByType(BinaryObjectReaderEx in_reader, IChunk in_parentChunk, MomentumType in_type)
        {
            return in_type switch
            {
                MomentumType.TranslateRandomAdd => new TranslateRandomAdd(in_reader, in_parentChunk),
                MomentumType.RotateNormal => new RotateNormal(in_reader, in_parentChunk),
                MomentumType.RotateAdd => new RotateAdd(in_reader, in_parentChunk),
                MomentumType.RotateRandomNormal => new RotateRandomNormal(in_reader, in_parentChunk),
                MomentumType.RotateRandomAdd => new RotateRandomAdd(in_reader, in_parentChunk),
                MomentumType.ScaleNormal => new ScaleNormal(in_reader, in_parentChunk),
                MomentumType.ScaleAdd => new ScaleAdd(in_reader, in_parentChunk),
                MomentumType.ScaleAccel => new ScaleAccel(in_reader, in_parentChunk),
                MomentumType.ScaleRandomAdd => new ScaleRandomAdd(in_reader, in_parentChunk),
                MomentumType.PlaceFanShaped => new PlaceFanShaped(in_reader, in_parentChunk),
                MomentumType.BillboardTail => new BillboardTail(in_reader, in_parentChunk),
                MomentumType.TurnCamera => new TurnCamera(in_reader, in_parentChunk),
                MomentumType.MaterialColorNormal => new MaterialColorNormal(in_reader, in_parentChunk),
                MomentumType.MaterialColorGoal => new MaterialColorGoal(in_reader, in_parentChunk),
                MomentumType.ShadowOn => new ShadowOn(in_reader, in_parentChunk),
                MomentumType.DirectionalLight => new DirectionalLight(in_reader, in_parentChunk),
                MomentumType.MotionSet => new MotionSet(in_reader, in_parentChunk),
                MomentumType.SoundPlay => new SoundPlay(in_reader, in_parentChunk),
                MomentumType.Sound3DPlay => new Sound3DPlay(in_reader, in_parentChunk),
                MomentumType.ParticlePlay => new ParticlePlay(in_reader, in_parentChunk),
                MomentumType.Subtitle => new Subtitle(in_reader, in_parentChunk),
                _ => null
            };
        }
    }
}
