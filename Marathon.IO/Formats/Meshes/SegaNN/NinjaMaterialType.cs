// NinjaMaterialType.cs is licensed under the MIT License:
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
    public enum NinjaMaterialType
    {
        // 2 and 3 are educated guesses.
        // 0 might mean no textures at all?
        NND_MATTYPE_TEXTURE     = 1,
        NND_MATTYPE_TEXTURE2    = 2,
        NND_MATTYPE_TEXTURE3    = 3,
        NND_MATTYPE_TEXTURE4    = 4,
        NND_MATTYPE_TEXMATTYPE2 = 16
    }

    [Flags]
    public enum NinjaMaterialBlendType
    {
        NNE_BLENDOP_ADD = 0x8006
    }

    [Flags]
    public enum NinjaMaterialLogicType
    {
        NNE_LOGICOP_NONE = 0
    }

    [Flags]
    public enum NinjaMaterialAlphaCompareType
    {
        NNE_CMPFUNC_NEVER        = 0x200,
        NNE_CMPFUNC_LESS         = 0x201,
        NNE_CMPFUNC_EQUAL        = 0x202,
        NNE_CMPFUNC_LESSEQUAL    = 0x203,
        NNE_CMPFUNC_GREATER      = 0x204,
        NNE_CMPFUNC_NOTEQUAL     = 0x205,
        NNE_CMPFUNC_GREATEREQUAL = 0x206,
        NNE_CMPFUNC_ALWAYS       = 0x207
    }
}
