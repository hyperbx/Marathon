using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MomentumFactory
    {
        public static IMomentumParamSet CreateMomentumParamsByType(MomentumType in_type)
        {
            return in_type switch
            {
                MomentumType.TranslateNormal => new TranslateNormal(),
                MomentumType.TranslateAdd => new TranslateAdd(),
                MomentumType.TranslateAccel => new TranslateAccel(),
                MomentumType.TranslateRandomNormal => new TranslateRandomNormal(),
                MomentumType.TranslateRandomAdd => new TranslateRandomAdd(),
                MomentumType.TranslateRandomSin => new TranslateRandomSin(),
                MomentumType.TranslateGoal => new TranslateGoal(),
                MomentumType.RotateNormal => new RotateNormal(),
                MomentumType.RotateAdd => new RotateAdd(),
                MomentumType.RotateRandomNormal => new RotateRandomNormal(),
                MomentumType.RotateRandomAdd => new RotateRandomAdd(),
                MomentumType.RotateGoal => new RotateGoal(),
                MomentumType.ScaleNormal => new ScaleNormal(),
                MomentumType.ScaleAdd => new ScaleAdd(),
                MomentumType.ScaleAccel => new ScaleAccel(),
                MomentumType.ScaleRandomNormal => new ScaleRandomNormal(),
                MomentumType.ScaleRandomAdd => new ScaleRandomAdd(),
                MomentumType.ScaleGoal => new ScaleGoal(),
                MomentumType.ScaleRandomGoal => new ScaleRandomGoal(),
                MomentumType.ScaleAddGoal => new ScaleAddGoal(),
                MomentumType.PlaceFanShaped => new PlaceFanShaped(),
                MomentumType.PlaceLineShaped => new PlaceLineShaped(),
                MomentumType.ParticleBillboardPV => new ParticleBillboardPV(),
                MomentumType.SparklingTail => new SparklingTail(),
                MomentumType.BlurBelt => new BlurBelt(),
                MomentumType.BillboardTail => new BillboardTail(),
                MomentumType.TurnCamera => new TurnCamera(),
                MomentumType.MaterialColorNormal => new MaterialColorNormal(),
                MomentumType.MaterialColorSin => new MaterialColorSin(),
                MomentumType.MaterialColorRandomNormal => new MaterialColorRandomNormal(),
                MomentumType.MaterialColorGoal => new MaterialColorGoal(),
                MomentumType.MaterialColorRandomGoal => new MaterialColorRandomGoal(),
                MomentumType.ModelJoin => new ModelJoin(),
                MomentumType.ShadowOn => new ShadowOn(),
                MomentumType.PointLight => new PointLight(),
                MomentumType.DirectionalLight => new DirectionalLight(),
                MomentumType.AmbientLight => new AmbientLight(),
                MomentumType.MotionSet => new MotionSet(),
                MomentumType.DetachCoordinate => new DetachCoordinate(),
                MomentumType.SendParamGoal => new SendParamGoal(),
                MomentumType.SoundPlay => new SoundPlay(),
                MomentumType.Sound3DPlay => new Sound3DPlay(),
                MomentumType.ParticlePlay => new ParticlePlay(),
                MomentumType.CellSpriteSceneSet => new CellSpriteSceneSet(),
                MomentumType.Subtitle => new Subtitle(),
                MomentumType.ClipPlane => new ClipPlane(),
                MomentumType.FilterColorCorrection => new FilterColorCorrection(),
                _ => null
            };
        }

        public static IMomentumParamSet ReadMomentumParamsByType(BinaryObjectReaderEx in_reader, MomentumType in_type)
        {
            var result = CreateMomentumParamsByType(in_type);

            result?.Read(in_reader);

            return result;
        }

        public static Momentum CreateMomentumByType(MomentumType in_type)
        {
            return new Momentum()
            {
                Type = in_type,
                Params = CreateMomentumParamsByType(in_type)
            };
        }
    }
}
