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

using System.Collections.Generic;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaObjectMaterial
    {
        public NinjaMaterialType Type { get; set; }

        public uint Description_Flag { get; set; }

        public uint Description_UserDefined { get; set; }

        public NinjaObjectMaterialColour Colour { get; set; }

        public NinjaObjectMaterialLogic Logic { get; set; }

        public List<NinjaObjectMaterialTexture> Textures = new List<NinjaObjectMaterialTexture>();

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
                Colour = new NinjaObjectMaterialColour(reader);
            }

            // NNS_MATERIAL_LOGIC chunk.
            if (MaterialLogicOffset != 0)
            {
                // Jump to this material's logic data.
                reader.JumpTo(MaterialLogicOffset, true);
                Logic = new NinjaObjectMaterialLogic(reader);
            }

            // NNS_MATERIAL_TEXMAP2_DESC chunk.
            if (MaterialTextureDescriptionOffset != 0)
            {
                // Jump to this material's logic data.
                reader.JumpTo(MaterialTextureDescriptionOffset, true);
                int count = 0;
                if (Type.HasFlag(NinjaMaterialType.NND_MATTYPE_TEXTURE)) { count = 1; }
                if (Type.HasFlag(NinjaMaterialType.NND_MATTYPE_TEXTURE2)) { count = 2; }
                if (Type.HasFlag(NinjaMaterialType.NND_MATTYPE_TEXTURE3)) { count = 3; }
                if (Type.HasFlag(NinjaMaterialType.NND_MATTYPE_TEXTURE4)) { count = 4; }
                for (int i = 0; i < count; i++)
                {
                    Textures.Add(new NinjaObjectMaterialTexture(reader));
                }
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }
    }

    /* ---------- NNS_MATERIAL_COLOR Values ---------- */
    public class NinjaObjectMaterialColour
    {
        public Vector4 Diffuse { get; set; }

        public Vector4 Ambient { get; set; }

        public Vector4 Specular { get; set; }

        public Vector4 Emissive { get; set; }

        public float Power { get; set; }

        public uint Reserved_0 { get; set; }

        public uint Reserved_1 { get; set; }

        public uint Reserved_2 { get; set; }

        public NinjaObjectMaterialColour(ExtendedBinaryReader reader)
        {
            Diffuse  = reader.ReadVector4(); // The diffuse value for this material.
            Ambient  = reader.ReadVector4(); // The ambient value for this material.
            Specular = reader.ReadVector4(); // The specular value for this material.
            Emissive = reader.ReadVector4(); // The diffuse value for this material.
            Power    = reader.ReadSingle();  // How much the other values are affected?
        }
    }

    /* ---------- NNS_MATERIAL_LOGIC Values ---------- */
    public class NinjaObjectMaterialLogic
    {
        public bool BlendEnable { get; set; }

        public uint SRCBlend { get; set; } // TODO: Figure out what SRC means.

        public uint DSTBlend { get; set; } // TODO: Figure out what DST means.

        public uint BlendFactor { get; set; }

        public NinjaMaterialBlendType BlendOP { get; set; } // TODO: Figure out what the OP part means. Blend Operation?

        public NinjaMaterialLogicType LogicOP { get; set; } // TODO: Figure out what the OP part means. Logic Operation?

        public bool AlphaEnable { get; set; }

        public NinjaMaterialAlphaCompareType AlphaFunction { get; set; }

        public uint AlphaRef { get; set; } // TODO: Figure out what the Ref part means. Alpha Reflectivity?

        public bool ZCompEnable { get; set; } // TODO: Figure out what the ZComp part means. Z-Compensation?

        public NinjaMaterialAlphaCompareType ZFunction { get; set; }

        public bool ZUpdateEnable { get; set; }

        public uint Reserved_0 { get; set; }

        public uint Reserved_1 { get; set; }

        public uint Reserved_2 { get; set; }

        public uint Reserved_3 { get; set; }

        public NinjaObjectMaterialLogic(ExtendedBinaryReader reader)
        {
            BlendEnable   = reader.ReadBoolean32(); // Whether this material can do blending?
            SRCBlend      = reader.ReadUInt32();
            DSTBlend      = reader.ReadUInt32();
            BlendFactor   = reader.ReadUInt32();    // Something to do with how much this material blends by?
            BlendOP       = (NinjaMaterialBlendType)reader.ReadUInt32();
            LogicOP       = (NinjaMaterialLogicType)reader.ReadUInt32();
            AlphaEnable   = reader.ReadBoolean32(); // Whether this material supports alpha tranparency?
            AlphaFunction = (NinjaMaterialAlphaCompareType)reader.ReadUInt32();
            AlphaRef      = reader.ReadUInt32();    // How reflective the alpha channel is maybe?
            ZCompEnable   = reader.ReadBoolean32();
            ZFunction     = (NinjaMaterialAlphaCompareType)reader.ReadUInt32();
            ZUpdateEnable = reader.ReadBoolean32();
        }
    }

    /* ---------- NNS_MATERIAL_TEXMAP2_DESC Values ---------- */
    public class NinjaObjectMaterialTexture
    {
        public uint Type { get; set; }

        public uint ID { get; set; }

        public Vector2 Offset { get; set; }

        public float Blend { get; set; }

        public float InfoOffset { get; set; }

        public NinjaTextureFilterType MinFilter { get; set; }

        public NinjaTextureFilterSetting MagFilter { get; set; }

        public float MipMapBias { get; set; }

        public uint MaxMipMapLevel { get; set; }

        public uint Reserved_0 { get; set; }

        public uint Reserved_1 { get; set; }

        public uint Reserved_2 { get; set; }

        public NinjaObjectMaterialTexture(ExtendedBinaryReader reader)
        {
            Type           = reader.ReadUInt32();
            ID             = reader.ReadUInt32();  // Which texture in the _NinjaTextureFileList this material should use.
            Offset         = reader.ReadVector2();
            Blend          = reader.ReadSingle();
            InfoOffset     = reader.ReadUInt32();  // This is always null in '06's XNOs.
            MinFilter      = (NinjaTextureFilterType)reader.ReadUInt16();  
            MagFilter      = (NinjaTextureFilterSetting)reader.ReadUInt16();
            MipMapBias     = reader.ReadSingle();
            MaxMipMapLevel = reader.ReadUInt32();  // The highest level mipmap this material is allowed to use?
            reader.FixPadding(0x10);
        }
    }
}
