// NinjaMotionType.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
 * Copyright (c) 2021 Radfordhound
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    [Flags]
    public enum NinjaMotionType : uint
    {
        /* Masks */
        NND_MOTIONTYPE_CATEGORY_MASK = 31,
        NND_MOTIONTYPE_REPEAT_MASK   = 0x1F0040u,

        /* Flags */
        NND_MOTIONTYPE_VERSION2 = 0x10000000u,

        /* Repeat Types */
        NND_MOTIONTYPE_TRIGGER     = 64,
        NND_MOTIONTYPE_NOREPEAT    = 0x10000u,
        NND_MOTIONTYPE_CONSTREPEAT = 0x20000u,
        NND_MOTIONTYPE_REPEAT      = 0x40000u,
        NND_MOTIONTYPE_MIRROR      = 0x80000u,
        NND_MOTIONTYPE_OFFSET      = 0x100000u,

        /* Motion Types */
        NND_MOTIONTYPE_NODE     = 1,
        NND_MOTIONTYPE_CAMERA   = 2,
        NND_MOTIONTYPE_LIGHT    = 4,
        NND_MOTIONTYPE_MORPH    = 8,
        NND_MOTIONTYPE_MATERIAL = 16
    }

    [Flags]
    public enum NinjaSubMotionType : uint
    {
        /* Masks */
        NND_SMOTTYPE_FRAME_MASK       = 3,
        NND_SMOTTYPE_ANGLE_MASK       = 28,
        NND_SMOTTYPE_TRANSLATION_MASK = 0x700u,
        NND_SMOTTYPE_DIFFUSE_MASK     = 0xE00u,
        NND_SMOTTYPE_ROTATION_MASK    = 0x7800u,
        NND_SMOTTYPE_SPECULAR_MASK    = 0xE000u,
        NND_SMOTTYPE_SCALING_MASK     = 0x38000u,
        NND_SMOTTYPE_USER_MASK        = 0xC0000u,
        NND_SMOTTYPE_LIGHT_COLOR_MASK = 0xE00000u,
        NND_SMOTTYPE_OFFSET_MASK      = 0x1800000u,
        NND_SMOTTYPE_TARGET_MASK      = 0x1C0000u,
        NND_SMOTTYPE_AMBIENT_MASK     = 0x1C0000u,
        NND_SMOTTYPE_UPTARGET_MASK    = 0x1C00000u,
        NND_SMOTTYPE_TEXTURE_MASK     = 0x1E00000u,
        NND_SMOTTYPE_UPVECTOR_MASK    = 0xE000000u,
        NND_SMOTTYPE_VALUETYPE_MASK   = 0xFFFFFF00u,

        /* Frame Types */
        NND_SMOTTYPE_FRAME_FLOAT  = 1,
        NND_SMOTTYPE_FRAME_SINT16 = 2,

        /* Angle Types */
        NND_SMOTTYPE_ANGLE_RADIAN  = 4,
        NND_SMOTTYPE_ANGLE_ANGLE32 = 8,
        NND_SMOTTYPE_ANGLE_ANGLE16 = 16,

        /* Node Types */
        NND_SMOTTYPE_TRANSLATION_X = 0x100u,
        NND_SMOTTYPE_TRANSLATION_Y = 0x200u,
        NND_SMOTTYPE_TRANSLATION_Z = 0x400u,

        NND_SMOTTYPE_TRANSLATION_XYZ =
        (
            NND_SMOTTYPE_TRANSLATION_X |
            NND_SMOTTYPE_TRANSLATION_Y |
            NND_SMOTTYPE_TRANSLATION_Z
        ),

        NND_SMOTTYPE_ROTATION_X = 0x800u,
        NND_SMOTTYPE_ROTATION_Y = 0x1000u,
        NND_SMOTTYPE_ROTATION_Z = 0x2000u,

        NND_SMOTTYPE_ROTATION_XYZ =
        (
            NND_SMOTTYPE_ROTATION_X |
            NND_SMOTTYPE_ROTATION_Y |
            NND_SMOTTYPE_ROTATION_Z
        ),

        NND_SMOTTYPE_QUATERNION = 0x4000u,

        NND_SMOTTYPE_SCALING_X = 0x8000u,
        NND_SMOTTYPE_SCALING_Y = 0x10000u,
        NND_SMOTTYPE_SCALING_Z = 0x20000u,

        NND_SMOTTYPE_SCALING_XYZ =
        (
            NND_SMOTTYPE_SCALING_X |
            NND_SMOTTYPE_SCALING_Y |
            NND_SMOTTYPE_SCALING_Z
        ),

        NND_SMOTTYPE_USER_UINT32 = 0x40000u,
        NND_SMOTTYPE_USER_FLOAT  = 0x80000u,

        NND_SMOTTYPE_NODEHIDE = 0x100000u,

        /* Camera Types */
        NND_SMOTTYPE_TARGET_X = 0x40000u,
        NND_SMOTTYPE_TARGET_Y = 0x80000u,
        NND_SMOTTYPE_TARGET_Z = 0x100000u,

        NND_SMOTTYPE_TARGET_XYZ =
        (
            NND_SMOTTYPE_TARGET_X |
            NND_SMOTTYPE_TARGET_Y |
            NND_SMOTTYPE_TARGET_Z
        ),

        NND_SMOTTYPE_ROLL       = 0x200000u,
        NND_SMOTTYPE_UPTARGET_X = 0x400000u,
        NND_SMOTTYPE_UPTARGET_Y = 0x800000u,
        NND_SMOTTYPE_UPTARGET_Z = 0x1000000u,

        NND_SMOTTYPE_UPTARGET_XYZ =
        (
            NND_SMOTTYPE_UPTARGET_X |
            NND_SMOTTYPE_UPTARGET_Y |
            NND_SMOTTYPE_UPTARGET_Z
        ),

        NND_SMOTTYPE_UPVECTOR_X = 0x2000000u,
        NND_SMOTTYPE_UPVECTOR_Y = 0x4000000u,
        NND_SMOTTYPE_UPVECTOR_Z = 0x8000000u,

        NND_SMOTTYPE_UPVECTOR_XYZ =
        (
            NND_SMOTTYPE_UPVECTOR_X |
            NND_SMOTTYPE_UPVECTOR_Y |
            NND_SMOTTYPE_UPVECTOR_Z
        ),

        NND_SMOTTYPE_FOVY   = 0x10000000u,
        NND_SMOTTYPE_ZNEAR  = 0x20000000u,
        NND_SMOTTYPE_ZFAR   = 0x40000000u,
        NND_SMOTTYPE_ASPECT = 0x80000000u,

        /* Light Types */
        NND_SMOTTYPE_LIGHT_COLOR_R = 0x200000u,
        NND_SMOTTYPE_LIGHT_COLOR_G = 0x400000u,
        NND_SMOTTYPE_LIGHT_COLOR_B = 0x800000u,

        NND_SMOTTYPE_LIGHT_COLOR_RGB =
        (
            NND_SMOTTYPE_LIGHT_COLOR_R |
            NND_SMOTTYPE_LIGHT_COLOR_G |
            NND_SMOTTYPE_LIGHT_COLOR_B
        ),

        NND_SMOTTYPE_LIGHT_ALPHA     = 0x1000000u,
        NND_SMOTTYPE_LIGHT_INTENSITY = 0x2000000u,

        NND_SMOTTYPE_FALLOFF_START = 0x4000000u,
        NND_SMOTTYPE_FALLOFF_END   = 0x8000000u,

        NND_SMOTTYPE_INNER_ANGLE = 0x10000000u,
        NND_SMOTTYPE_OUTER_ANGLE = 0x20000000u,
        NND_SMOTTYPE_INNER_RANGE = 0x40000000u,
        NND_SMOTTYPE_OUTER_RANGE = 0x80000000u,

        /* Morph Types */
        NND_SMOTTYPE_MORPH_WEIGHT = 0x1000000u,

        /* Material Types */
        NND_SMOTTYPE_HIDE = 0x100u,

        NND_SMOTTYPE_DIFFUSE_R = 0x200u,
        NND_SMOTTYPE_DIFFUSE_G = 0x400u,
        NND_SMOTTYPE_DIFFUSE_B = 0x800u,

        NND_SMOTTYPE_DIFFUSE_RGB =
        (
            NND_SMOTTYPE_DIFFUSE_R |
            NND_SMOTTYPE_DIFFUSE_G |
            NND_SMOTTYPE_DIFFUSE_B
        ),

        NND_SMOTTYPE_ALPHA = 0x1000u,

        NND_SMOTTYPE_SPECULAR_R = 0x2000u,
        NND_SMOTTYPE_SPECULAR_G = 0x4000u,
        NND_SMOTTYPE_SPECULAR_B = 0x8000u,

        NND_SMOTTYPE_SPECULAR_RGB =
        (
            NND_SMOTTYPE_SPECULAR_R |
            NND_SMOTTYPE_SPECULAR_G |
            NND_SMOTTYPE_SPECULAR_B
        ),

        NND_SMOTTYPE_SPECULAR_LEVEL = 0x10000u,
        NND_SMOTTYPE_SPECULAR_GLOSS = 0x20000u,

        NND_SMOTTYPE_AMBIENT_R = 0x40000u,
        NND_SMOTTYPE_AMBIENT_G = 0x80000u,
        NND_SMOTTYPE_AMBIENT_B = 0x100000u,

        NND_SMOTTYPE_AMBIENT_RGB =
        (
            NND_SMOTTYPE_AMBIENT_R |
            NND_SMOTTYPE_AMBIENT_G |
            NND_SMOTTYPE_AMBIENT_B
        ),

        NND_SMOTTYPE_TEXTURE_INDEX = 0x200000u,
        NND_SMOTTYPE_TEXTURE_BLEND = 0x400000u,

        NND_SMOTTYPE_OFFSET_U = 0x800000u,
        NND_SMOTTYPE_OFFSET_V = 0x1000000u,
        NND_SMOTTYPE_OFFSET_UV = (NND_SMOTTYPE_OFFSET_U | NND_SMOTTYPE_OFFSET_V),

        NND_SMOTTYPE_MATCLBK_USER = 0x2000000u
    }

    [Flags]
    public enum NinjaSubMotionInterpolationType : uint
    {
        /* Masks */
        NND_SMOTIPTYPE_IP_MASK     = 0xE77u,
        NND_SMOTIPTYPE_REPEAT_MASK = 0x1F0000u,

        /* Repeat Types */
        NND_SMOTIPTYPE_NOREPEAT    = 0x10000u,
        NND_SMOTIPTYPE_CONSTREPEAT = 0x20000u,
        NND_SMOTIPTYPE_REPEAT      = 0x40000u,
        NND_SMOTIPTYPE_MIRROR      = 0x80000u,
        NND_SMOTIPTYPE_OFFSET      = 0x100000u,

        /* Interpolation Types */
        NND_SMOTIPTYPE_SPLINE     = 1,
        NND_SMOTIPTYPE_LINEAR     = 2,
        NND_SMOTIPTYPE_CONSTANT   = 4,
        NND_SMOTIPTYPE_BEZIER     = 16,
        NND_SMOTIPTYPE_SI_SPLINE  = 32,
        NND_SMOTIPTYPE_TRIGGER    = 64,
        NND_SMOTIPTYPE_QUAT_LERP  = 512,
        NND_SMOTIPTYPE_QUAT_SLERP = 1024,
        NND_SMOTIPTYPE_QUAT_SQUAD = 2048
    }
}
