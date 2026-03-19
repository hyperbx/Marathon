using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MomentumFactory
    {
        public static IMomentumParamSet ReadMomentumByType(BinaryObjectReaderEx in_reader, MomentumType in_type)
        {
            return in_type switch
            {
                MomentumType.TranslateNormal => new TranslateNormal(in_reader),
                MomentumType.TranslateAdd => new TranslateAdd(in_reader),
                MomentumType.TranslateAccel => new TranslateAccel(in_reader),
                MomentumType.TranslateRandomNormal => new TranslateRandomNormal(in_reader),
                MomentumType.TranslateRandomAdd => new TranslateRandomAdd(in_reader),
                MomentumType.TranslateRandomSin => new TranslateRandomSin(in_reader),
                MomentumType.TranslateGoal => new TranslateGoal(in_reader),
                MomentumType.RotateNormal => new RotateNormal(in_reader),
                MomentumType.RotateAdd => new RotateAdd(in_reader),
                MomentumType.RotateRandomNormal => new RotateRandomNormal(in_reader),
                MomentumType.RotateRandomAdd => new RotateRandomAdd(in_reader),
                MomentumType.RotateGoal => new RotateGoal(in_reader),
                MomentumType.ScaleNormal => new ScaleNormal(in_reader),
                MomentumType.ScaleAdd => new ScaleAdd(in_reader),
                MomentumType.ScaleAccel => new ScaleAccel(in_reader),
                MomentumType.ScaleRandomNormal => new ScaleRandomNormal(in_reader),
                MomentumType.ScaleRandomAdd => new ScaleRandomAdd(in_reader),
                MomentumType.ScaleGoal => new ScaleGoal(in_reader),
                MomentumType.PlaceFanShaped => new PlaceFanShaped(in_reader),
                MomentumType.BlurBelt => new BlurBelt(in_reader),
                MomentumType.BillboardTail => new BillboardTail(in_reader),
                MomentumType.TurnCamera => new TurnCamera(in_reader),
                MomentumType.MaterialColorNormal => new MaterialColorNormal(in_reader),
                MomentumType.MaterialColorSin => new MaterialColorSin(in_reader),
                MomentumType.MaterialColorGoal => new MaterialColorGoal(in_reader),
                MomentumType.ModelJoin => new ModelJoin(in_reader),
                MomentumType.ShadowOn => new ShadowOn(in_reader),
                MomentumType.PointLight => new PointLight(in_reader),
                MomentumType.DirectionalLight => new DirectionalLight(in_reader),
                MomentumType.AmbientLight => new AmbientLight(in_reader),
                MomentumType.MotionSet => new MotionSet(in_reader),
                MomentumType.DetachCoordinate => new DetachCoordinate(in_reader),
                MomentumType.SendParamGoal => new SendParamGoal(in_reader),
                MomentumType.SoundPlay => new SoundPlay(in_reader),
                MomentumType.Sound3DPlay => new Sound3DPlay(in_reader),
                MomentumType.ParticlePlay => new ParticlePlay(in_reader),
                MomentumType.CellSpriteSceneSet => new CellSpriteSceneSet(in_reader),
                MomentumType.Subtitle => new Subtitle(in_reader),
                MomentumType.ClipPlane => new ClipPlane(in_reader),
                MomentumType.FilterColorCorrection => new FilterColorCorrection(in_reader),
                _ => null
            };
        }
    }
}
