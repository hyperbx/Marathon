using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System;
using System.Runtime.InteropServices;

namespace Marathon.Formats.Ninja.Types
{
    public class KeyframeFactory
    {
        public static object ReadKeyframeByType(BinaryObjectReaderEx in_reader, SubMotionType in_subMotionType)
        {
            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK) ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SCALING_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_AMBIENT_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_DIFFUSE_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SPECULAR_MASK)    ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_LIGHT_COLOR_MASK))
            {
                return in_reader.Read<KeyframeVector>();
            }

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_ROTATION_XYZ))
                return in_reader.Read<KeyframeRotateS16>();

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_FLOAT))
                return in_reader.Read<KeyframeF32>();

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_SINT16))
                return in_reader.Read<KeyframeS16>();

            throw new NotImplementedException();
        }

        public static void WriteKeyframeByType(BinaryObjectWriterEx in_writer, SubMotionType in_subMotionType, object in_keyframe)
        {
            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK) ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SCALING_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_AMBIENT_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_DIFFUSE_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SPECULAR_MASK)    ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_LIGHT_COLOR_MASK))
            {
                in_writer.Write((KeyframeVector)in_keyframe);
            }
            else if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_ROTATION_XYZ))
            {
                in_writer.Write((KeyframeRotateS16)in_keyframe);
            }
            else if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_FLOAT))
            {
                in_writer.Write((KeyframeF32)in_keyframe);
            }
            else if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_SINT16))
            {
                in_writer.Write((KeyframeS16)in_keyframe);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static int GetKeyframeSize(SubMotionType in_subMotionType)
        {
            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK) ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SCALING_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_AMBIENT_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_DIFFUSE_MASK)     ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_SPECULAR_MASK)    ||
                in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_LIGHT_COLOR_MASK))
            {
                return Marshal.SizeOf<KeyframeVector>();
            }

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_ROTATION_XYZ))
                return Marshal.SizeOf<KeyframeRotateS16>();

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_FLOAT))
                return Marshal.SizeOf<KeyframeF32>();

            if (in_subMotionType.HasFlag(SubMotionType.NND_SMOTTYPE_FRAME_SINT16))
                return Marshal.SizeOf<KeyframeS16>();

            return 0;
        }
    }
}
