// NinjaMaterial.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
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

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaObjectMaterial
    {
        public NinjaMaterialType Type { get; set; }

        /* ---------- NNS_MATERIAL_DESC Values ---------- */

        public uint Description_Flag { get; set; }

        public uint Description_UserDefined { get; set; }

        /* ---------- NNS_MATERIAL_COLOR Values ---------- */

        public Vector4 Colour_Diffuse { get; set; }

        public Vector4 Colour_Ambient { get; set; }

        public Vector4 Colour_Specular { get; set; }

        public Vector4 Colour_Emissive { get; set; }

        public float Colour_Power { get; set; }

        public uint Colour_Reserved_0 { get; set; }

        public uint Colour_Reserved_1 { get; set; }

        public uint Colour_Reserved_2 { get; set; }

        /* ---------- NNS_MATERIAL_LOGIC Values ---------- */

        public bool Logic_BlendEnable { get; set; }

        public uint Logic_SRCBlend { get; set; } // TODO: Figure out what SRC means.

        public uint Logic_DSTBlend { get; set; } // TODO: Figure out what DST means.

        public uint Logic_BlendFactor { get; set; }

        public NinjaMaterialBlendType Logic_BlendOP { get; set; } // TODO: Figure out what the OP part means. Blend Operation?

        public NinjaMaterialLogicType Logic_LogicOP { get; set; } // TODO: Figure out what the OP part means. Logic Operation?

        public bool Logic_AlphaEnable { get; set; }

        public NinjaMaterialAlphaCompareType Logic_AlphaFunction { get; set; }

        public uint Logic_AlphaRef { get; set; } // TODO: Figure out what the Ref part means. Alpha Reflectivity?

        public bool Logic_ZCompEnable { get; set; } // TODO: Figure out what the ZComp part means. Z-Compensation?

        public NinjaMaterialAlphaCompareType Logic_ZFunction { get; set; }

        public bool Logic_ZUpdateEnable { get; set; }

        public uint Logic_Reserved_0 { get; set; }

        public uint Logic_Reserved_1 { get; set; }

        public uint Logic_Reserved_2 { get; set; }

        public uint Logic_Reserved_3 { get; set; }

        /* ---------- NNS_MATERIAL_TEXMAP2_DESC Values ---------- */

        public uint Texture_Type { get; set; }

        public uint Texture_ID { get; set; }

        public Vector2 Texture_Offset { get; set; }

        public float Texture_Blend { get; set; }

        public float Texture_InfoOffset { get; set; }

        public uint Texture_MinFilter { get; set; } // TODO: Find out what the Min part means. All set to NND_MIN_LINEAR_MIPMAP_NEAREST in the XTO.

        public uint Texture_MagFilter { get; set; } // TODO: Find out what the Mag part means. All set to NND_MAG_LINEAR in the XTO.

        public float Texture_MipMapBias { get; set; }

        public uint Texture_MaxMipMapLevel { get; set; }

        public uint Texture_Reserved_0 { get; set; }

        public uint Texture_Reserved_1 { get; set; }

        public uint Texture_Reserved_2 { get; set; }

        public NinjaObjectMaterial(ExtendedBinaryReader reader)
        {
            Type = (NinjaMaterialType)reader.ReadUInt32();        // This material's type.
            uint MaterialDescriptionOffset = reader.ReadUInt32(); // The offset to this material's NNS_MATERIAL_DESC chunk.

            long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next material after we've read all the data for this one.

            // NNS_MATERIAL_DESC chunk
            // Jump to this material's description chunk.
            reader.JumpTo(MaterialDescriptionOffset, true);

            Description_Flag        = reader.ReadUInt32(); // The flags for this material.
            Description_UserDefined = reader.ReadUInt32(); // The user defined value for this material.

            uint MaterialColourOffset             = reader.ReadUInt32(); // The offset to this material's colour data.
            uint MaterialLogicOffset              = reader.ReadUInt32(); // The offset to this material's logic data.
            uint MaterialTextureDescriptionOffset = reader.ReadUInt32(); // The offset to this material's texture data.

            // NNS_MATERIAL_COLOR chunk
            if (MaterialColourOffset != 0)
            {
                // Jump to this material's colour data.
                reader.JumpTo(MaterialColourOffset, true);

                Colour_Diffuse  = reader.ReadVector4(); // The diffuse value for this material.
                Colour_Ambient  = reader.ReadVector4(); // The ambient value for this material.
                Colour_Specular = reader.ReadVector4(); // The specular value for this material.
                Colour_Emissive = reader.ReadVector4(); // The diffuse value for this material.
                Colour_Power    = reader.ReadSingle();  // How much the other values are affected? 
            }

            // NNS_MATERIAL_LOGIC chunk.
            if (MaterialLogicOffset != 0)
            {
                // Jump to this material's logic data.
                reader.JumpTo(MaterialLogicOffset, true);

                Logic_BlendEnable   = reader.ReadBoolean32(); // Whether this material can do blending?
                Logic_SRCBlend      = reader.ReadUInt32();
                Logic_DSTBlend      = reader.ReadUInt32();
                Logic_BlendFactor   = reader.ReadUInt32();    // Something to do with how much this material blends by?
                Logic_BlendOP       = (NinjaMaterialBlendType)reader.ReadUInt32();
                Logic_LogicOP       = (NinjaMaterialLogicType)reader.ReadUInt32();
                Logic_AlphaEnable   = reader.ReadBoolean32(); // Whether this material supports alpha tranparency?
                Logic_AlphaFunction = (NinjaMaterialAlphaCompareType)reader.ReadUInt32();
                Logic_AlphaRef      = reader.ReadUInt32();    // How reflective the alpha channel is maybe?
                Logic_ZCompEnable   = reader.ReadBoolean32();
                Logic_ZFunction     = (NinjaMaterialAlphaCompareType)reader.ReadUInt32();
                Logic_ZUpdateEnable = reader.ReadBoolean32();
            }

            // NNS_MATERIAL_TEXMAP2_DESC chunk.
            if (MaterialTextureDescriptionOffset != 0)
            {
                // Jump to this material's logic data.
                reader.JumpTo(MaterialTextureDescriptionOffset, true);

                Texture_Type           = reader.ReadUInt32();
                Texture_ID             = reader.ReadUInt32();  // Which texture in the _NinjaTextureFileList this material should use.
                Texture_Offset         = reader.ReadVector2();
                Texture_Blend          = reader.ReadSingle();
                Texture_InfoOffset     = reader.ReadUInt32();  // This is always null in '06's XNOs.
                Texture_MinFilter      = reader.ReadUInt32();
                Texture_MagFilter      = reader.ReadUInt32();
                Texture_MipMapBias     = reader.ReadSingle();
                Texture_MaxMipMapLevel = reader.ReadUInt32();  // The highest level mipmap this material is allowed to use?
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }
    }
}
