using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Types.BINA;
using System;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class LightFactory
    {
        public static object ReadLightByType(BinaryObjectReaderEx in_reader, LightType in_lightType)
        {
            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_STANDARD_GL))
                return in_reader.Read<LightStandardGL>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_PARALLEL))
                return in_reader.Read<LightParallel>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_POINT))
                return in_reader.Read<LightPoint>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_SPOT))
                return in_reader.Read<LightTargetSpot>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_SPOT))
                return in_reader.Read<LightRotationSpot>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_DIRECTIONAL))
                return in_reader.Read<LightTargetDirectional>();

            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_DIRECTIONAL))
                return in_reader.Read<LightRotationDirectional>();

            throw new NotImplementedException();
        }

        public static void WriteLightByType(BinaryObjectWriterEx in_writer, LightType in_lightType, object in_light)
        {
            if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_STANDARD_GL))
            {
                in_writer.Write((LightStandardGL)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_PARALLEL))
            {
                in_writer.Write((LightParallel)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_POINT))
            {
                in_writer.Write((LightPoint)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_SPOT))
            {
                in_writer.Write((LightTargetSpot)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_SPOT))
            {
                in_writer.Write((LightRotationSpot)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_TARGET_DIRECTIONAL))
            {
                in_writer.Write((LightTargetDirectional)in_light);
            }
            else if (in_lightType.HasFlag(LightType.NND_LIGHTTYPE_ROTATION_DIRECTIONAL))
            {
                in_writer.Write((LightRotationDirectional)in_light);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
