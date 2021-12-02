namespace Marathon.Formats.Mesh.Ninja
{
    public enum NinjaNext_MinFilter : uint
    {
        NND_MIN_NEAREST = 0,
        NND_MIN_LINEAR = 1,
        NND_MIN_NEAREST_MIPMAP_NEAREST = 2,
        NND_MIN_NEAREST_MIPMAP_LINEAR = 3,
        NND_MIN_LINEAR_MIPMAP_NEAREST = 4,
        NND_MIN_LINEAR_MIPMAP_LINEAR = 5,
        NND_MIN_ANISOTROPIC = 6,
        NND_MIN_ANISOTROPIC2 = 6,
        NND_MIN_ANISOTROPIC_MIPMAP_NEAREST = 7,
        NND_MIN_ANISOTROPIC2_MIPMAP_NEAREST = 7,
        NND_MIN_ANISOTROPIC_MIPMAP_LINEAR = 8,
        NND_MIN_ANISOTROPIC2_MIPMAP_LINEAR = 8,
        NND_MIN_ANISOTROPIC4 = 9,
        NND_MIN_ANISOTROPIC4_MIPMAP_NEAREST = 10,
        NND_MIN_ANISOTROPIC4_MIPMAP_LINEAR = 11,
        NND_MIN_ANISOTROPIC8 = 12,
        NND_MIN_ANISOTROPIC8_MIPMAP_NEAREST = 13,
        NND_MIN_ANISOTROPIC8_MIPMAP_LINEAR = 14
    };

    public enum NinjaNext_MagFilter : uint
    {
        NND_MAG_NEAREST = 0,
        NND_MAG_LINEAR = 1,
        NND_MAG_ANISOTROPIC = 2
    };

    public enum NinjaNext_MaterialType : uint
    {
        NND_MATTYPE_TEXTURE = 1,
        NND_MATTYPE_TEXTURE2 = 2,
        NND_MATTYPE_TEXTURE3 = 3,
        NND_MATTYPE_TEXTURE4 = 4,
        NND_MATTYPE_TEXMATTYPE2 = 16,
    };

    public enum NinjaNext_BlendMode : uint
    {
        NNE_BLENDMODE_SRCALPHA = 0x302,
        NNE_BLENDMODE_INVSRCALPHA = 0x303
    };
    public enum NinjaNext_BlendOperation : uint
    {
        NNE_BLENDOP_ADD = 0x8006
    };

    public enum NinjaNext_LogicOperation : uint
    {
        NNE_LOGICOP_NONE = 0
    };

    public enum NinjaNext_CMPFunction : uint
    {
        NNE_CMPFUNC_NEVER = 0x200,
        NNE_CMPFUNC_LESS = 0x201,
        NNE_CMPFUNC_EQUAL = 0x202,
        NNE_CMPFUNC_LESSEQUAL = 0x203,
        NNE_CMPFUNC_GREATER = 0x204,
        NNE_CMPFUNC_NOTEQUAL = 0x205,
        NNE_CMPFUNC_GREATEREQUAL = 0x206,
        NNE_CMPFUNC_ALWAYS = 0x207
    };

    public enum NinjaNext_VertexType : uint
    {
        /* Masks */
        NND_VTXTYPE_PLATFORM_MASK = 0xFFFF,
        NND_VTXTYPE_COMMON_MASK = 0xFF0000,
        NND_VTXTYPE_USER_MASK = 0xFF000000,

        /* Xbox/PC types */
        NND_VTXTYPE_DX_VERTEXDESC = 1,

        /* Common types */
        NND_VTXTYPE_COMMON_VTXDESC = 0x10000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET = 0x20000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_TARGET_NULL = 0x40000,
        NND_VTXTYPE_COMMON_VTXDESC_MORPH_OBJECT = 0x80000
    };

    [Flags]
    public enum NinjaNext_XboxVertexType : uint
    {
        /* Xbox types */
        // TODO: Is all of this right?    
        NND_VTXTYPE_XB_POSITION = 0x01U,
        NND_VTXTYPE_XB_NORMAL = 0x02U,
        NND_VTXTYPE_XB_COLOR = 0x08U,
        NND_VTXTYPE_XB_COLOR2 = 0x10U,
        NND_VTXTYPE_XB_TANGENT = 0x40U, /* TODO: Is this right? Might be 0x100. */
        NND_VTXTYPE_XB_BINORMAL = 0x100U, /* TODO: Is this right? Might be 0x40. */
        NND_VTXTYPE_XB_MTX_INDEX4 = 0x400U,
        NND_VTXTYPE_XB_WEIGHT3 = 0x7000U,
        NND_VTXTYPE_XB_TEXCOORD = 0x10000U,

        NND_VTXTYPE_XB_PNCT = (NND_VTXTYPE_XB_POSITION |
            NND_VTXTYPE_XB_NORMAL | NND_VTXTYPE_XB_COLOR |
            NND_VTXTYPE_XB_TEXCOORD),

        NND_VTXTYPE_XB_PW4INCT = (NND_VTXTYPE_XB_POSITION |
            NND_VTXTYPE_XB_WEIGHT3 | NND_VTXTYPE_XB_MTX_INDEX4 |
            NND_VTXTYPE_XB_NORMAL | NND_VTXTYPE_XB_COLOR |
            NND_VTXTYPE_XB_TEXCOORD),

        NND_VTXTYPE_XB_PW4INCTAB = (NND_VTXTYPE_XB_POSITION |
            NND_VTXTYPE_XB_WEIGHT3 | NND_VTXTYPE_XB_MTX_INDEX4 |
            NND_VTXTYPE_XB_NORMAL | NND_VTXTYPE_XB_COLOR |
            NND_VTXTYPE_XB_TEXCOORD | NND_VTXTYPE_XB_TANGENT |
            NND_VTXTYPE_XB_BINORMAL),
    }

    public enum NinjaNext_FlexibleVertexFormat : uint
    {
        /* Masks */
        NND_D3DFVF_POSITION_MASK = 0x400EU,
        NND_D3DFVF_TEXCOUNT_MASK = 0xf00U,

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
        NND_D3DFVF_XYZW = 0x4002U,

        NND_D3DFVF_NORMAL = 0x010U,
        NND_D3DFVF_PSIZE = 0x020U,
        NND_D3DFVF_DIFFUSE = 0x040U,
        NND_D3DFVF_SPECULAR = 0x080U,

        NND_D3DFVF_TEX0 = 0x000U,
        NND_D3DFVF_TEX1 = 0x100U,
        NND_D3DFVF_TEX2 = 0x200U,
        NND_D3DFVF_TEX3 = 0x300U,
        NND_D3DFVF_TEX4 = 0x400U,
        NND_D3DFVF_TEX5 = 0x500U,
        NND_D3DFVF_TEX6 = 0x600U,
        NND_D3DFVF_TEX7 = 0x700U,
        NND_D3DFVF_TEX8 = 0x800U,

        NND_D3DFVF_LASTBETA_UBYTE4 = 0x1000U,
        NND_D3DFVF_LASTBETA_D3DCOLOR = 0x8000U
    };

    public enum NinjaNext_PrimitiveType : uint
    {
        /* Xbox types */
        NND_PRIMTYPE_DX_STRIPLIST = 1
    };
}
