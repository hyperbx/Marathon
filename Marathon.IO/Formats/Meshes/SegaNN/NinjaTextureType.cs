// NinjaTextureType.cs is licensed under the MIT License:
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
    public enum NinjaTextureFileType
    {
        /* Masks */
        NND_TEXFTYPE_TEXTYPE_MASK = 255,

        /* Flags */
        NND_TEXFTYPE_NO_FILENAME = 256,
        NND_TEXFTYPE_NO_FILTER   = 512,
        NND_TEXFTYPE_LISTGLBIDX  = 1024,
        NND_TEXFTYPE_LISTBANK    = 2048,

        /* Types */
        NND_TEXFTYPE_GVRTEX = 0,
        NND_TEXFTYPE_SVRTEX = 1, // SVR format (used on PS2).
        NND_TEXFTYPE_XVRTEX = 2
    }

    [Flags]
    public enum NinjaTextureFilterType
    {
        NND_MIN_NEAREST                     = 0,
        NND_MIN_LINEAR                      = 1,
        NND_MIN_NEAREST_MIPMAP_NEAREST      = 2,
        NND_MIN_NEAREST_MIPMAP_LINEAR       = 3,
        NND_MIN_LINEAR_MIPMAP_NEAREST       = 4,
        NND_MIN_LINEAR_MIPMAP_LINEAR        = 5,
        NND_MIN_ANISOTROPIC                 = 6,
        NND_MIN_ANISOTROPIC2                = 6,
        NND_MIN_ANISOTROPIC_MIPMAP_NEAREST  = 7,
        NND_MIN_ANISOTROPIC2_MIPMAP_NEAREST = 7,
        NND_MIN_ANISOTROPIC_MIPMAP_LINEAR   = 8,
        NND_MIN_ANISOTROPIC2_MIPMAP_LINEAR  = 8,
        NND_MIN_ANISOTROPIC4                = 9,
        NND_MIN_ANISOTROPIC4_MIPMAP_NEAREST = 10,
        NND_MIN_ANISOTROPIC4_MIPMAP_LINEAR  = 11,
        NND_MIN_ANISOTROPIC8                = 12,
        NND_MIN_ANISOTROPIC8_MIPMAP_NEAREST = 13,
        NND_MIN_ANISOTROPIC8_MIPMAP_LINEAR  = 14
    }

    [Flags]
    public enum NinjaTextureFilterSetting
    {
        NND_MAG_NEAREST     = 0,
        NND_MAG_LINEAR      = 1,
        NND_MAG_ANISOTROPIC = 2
    }
}
