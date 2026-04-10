using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System;

namespace Marathon.Formats.Ninja.Types
{
    public class LightFactory
    {
        public static object ReadLightByType(BinaryObjectReaderEx in_reader, LightType in_lightType)
        {
            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_STANDARD_GL))
                return in_reader.ReadObject<LightStandardGL>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_PARALLEL))
                return in_reader.ReadObject<LightParallel>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_POINT))
                return in_reader.ReadObject<LightPoint>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_SPOT))
                return in_reader.ReadObject<LightTargetSpot>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_SPOT))
                return in_reader.ReadObject<LightRotationSpot>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_DIRECTIONAL))
                return in_reader.ReadObject<LightTargetDirectional>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_DIRECTIONAL))
                return in_reader.ReadObject<LightRotationDirectional>();

            throw new NotImplementedException();
        }

        public static void WriteLightByType(BinaryObjectWriterEx in_writer, LightType in_lightType, object in_light)
        {
            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_STANDARD_GL))
            {
                in_writer.WriteObject((LightStandardGL)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_PARALLEL))
            {
                in_writer.WriteObject((LightParallel)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_POINT))
            {
                in_writer.WriteObject((LightPoint)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_SPOT))
            {
                in_writer.WriteObject((LightTargetSpot)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_SPOT))
            {
                in_writer.WriteObject((LightRotationSpot)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_DIRECTIONAL))
            {
                in_writer.WriteObject((LightTargetDirectional)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_DIRECTIONAL))
            {
                in_writer.WriteObject((LightRotationDirectional)in_light);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
