using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System;

namespace Marathon.Formats.Ninja.Types
{
    public class CameraFactory
    {
        public static ICamera ReadCameraByType(BinaryObjectReaderEx in_reader, CameraType in_cameraType)
        {
            if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_ROLL))
                return in_reader.Read<CameraTargetRoll>();

            if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_UPVECTOR))
                return in_reader.Read<CameraTargetUpVector>();

            if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_UPTARGET))
                return in_reader.Read<CameraTargetUpTarget>();

            if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_ROTATION))
                return in_reader.Read<CameraRotation>();

            throw new NotImplementedException();
        }

        public static void WriteCameraByType(BinaryObjectWriterEx in_writer, CameraType in_cameraType, ICamera in_camera)
        {
            if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_ROLL))
            {
                in_writer.Write((CameraTargetRoll)in_camera);
            }
            else if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_UPVECTOR))
            {
                in_writer.Write((CameraTargetUpVector)in_camera);
            }
            else if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_TARGET_UPTARGET))
            {
                in_writer.Write((CameraTargetUpTarget)in_camera);
            }
            else if (in_cameraType.HasFlag(CameraType.NND_CAMERATYPE_ROTATION))
            {
                in_writer.Write((CameraRotation)in_camera);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
