// NinjaObjectVertexType.cs is licensed under the MIT License:
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
    public enum NinjaObjectVertexType : uint
    {
        NND_VTXTYPE_XB_POSITION   = 1,
        NND_VTXTYPE_XB_NORMAL     = 2,
        NND_VTXTYPE_XB_COLOR      = 8,
        NND_VTXTYPE_XB_TANGENT    = 0x40u,
        NND_VTXTYPE_XB_BINORMAL   = 0x100u,
        NND_VTXTYPE_XB_MTX_INDEX4 = 0x400u,
        NND_VTXTYPE_XB_WEIGHT3    = 0x7000u,
        NND_VTXTYPE_XB_TEXCOORD   = 0x10000u,

        NND_VTXTYPE_XB_PNCT =
        (
            NND_VTXTYPE_XB_POSITION |
            NND_VTXTYPE_XB_NORMAL   |
            NND_VTXTYPE_XB_COLOR    |
            NND_VTXTYPE_XB_TEXCOORD
        ),

        NND_VTXTYPE_XB_PW4INCT =
        (
            NND_VTXTYPE_XB_POSITION   |
            NND_VTXTYPE_XB_WEIGHT3    |
            NND_VTXTYPE_XB_MTX_INDEX4 |
            NND_VTXTYPE_XB_NORMAL     |
            NND_VTXTYPE_XB_COLOR      |
            NND_VTXTYPE_XB_TEXCOORD
        ),

        NND_VTXTYPE_XB_PW4INCTAB =
        (
            NND_VTXTYPE_XB_POSITION   |
            NND_VTXTYPE_XB_WEIGHT3    |
            NND_VTXTYPE_XB_MTX_INDEX4 |
            NND_VTXTYPE_XB_NORMAL     |
            NND_VTXTYPE_XB_COLOR      |
            NND_VTXTYPE_XB_TEXCOORD   |
            NND_VTXTYPE_XB_TANGENT    |
            NND_VTXTYPE_XB_BINORMAL
        ),
    }

    [Flags]
    public enum NinjaObjectVertexDescriptionType : uint
    {
        /* Masks */
        NND_VTXTYPE_PLATFORM_MASK = 0xFFFFu,
        NND_VTXTYPE_COMMON_MASK   = 0xFF0000u,
        NND_VTXTYPE_USER_MASK     = 0xFF000000u,

        /* Xbox/PC Types */
        NND_VTXTYPE_DX_VERTEXDESC = 1,

        /* Common Types */
        NND_VTXTYPE_COMMON_VTXDESC                   = 0x10000u,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET      = 0x20000u,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET_NULL = 0x40000u,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_OBJECT      = 0x80000U
    }

    [Flags]
    public enum NinjaObjectVertexDirect3DType : uint
    {
        /* Masks */
        NND_D3DFVF_POSITION_MASK = 0x400Eu,
        NND_D3DFVF_TEXCOUNT_MASK = 0xf00u,

        /* Types */
        NND_D3DFVF_TEXTUREFORMAT2 = 0,
        NND_D3DFVF_TEXTUREFORMAT3 = 1,
        NND_D3DFVF_TEXTUREFORMAT4 = 2,
        NND_D3DFVF_TEXTUREFORMAT1 = 3,

        NND_D3DFVF_XYZ = 2,
        NND_D3DFVF_XYZRHW = 4,
        NND_D3DFVF_XYZB1 = 6,
        NND_D3DFVF_XYZB2 = 8,
        NND_D3DFVF_XYZB3 = 10,
        NND_D3DFVF_XYZB4 = 12,
        NND_D3DFVF_XYZB5 = 14,
        NND_D3DFVF_XYZW = 0x4002u,

        NND_D3DFVF_NORMAL = 0x010u,
        NND_D3DFVF_PSIZE = 0x020u,
        NND_D3DFVF_DIFFUSE = 0x040u,
        NND_D3DFVF_SPECULAR = 0x080u,

        NND_D3DFVF_TEX0 = 0x000u,
        NND_D3DFVF_TEX1 = 0x100u,
        NND_D3DFVF_TEX2 = 0x200u,
        NND_D3DFVF_TEX3 = 0x300u,
        NND_D3DFVF_TEX4 = 0x400u,
        NND_D3DFVF_TEX5 = 0x500u,
        NND_D3DFVF_TEX6 = 0x600u,
        NND_D3DFVF_TEX7 = 0x700u,
        NND_D3DFVF_TEX8 = 0x800u,

        NND_D3DFVF_LASTBETA_UBYTE4 = 0x1000u,
        NND_D3DFVF_LASTBETA_D3DCOLOR = 0x8000U
    }
}
