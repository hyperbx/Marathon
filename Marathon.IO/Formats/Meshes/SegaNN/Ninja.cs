// Ninja.cs is licensed under the MIT License:
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

using System;
using System.IO;
using System.Collections.Generic;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class Ninja : FileBase
    {
        public List<NinjaTexture> Textures;
        public NinjaEffect Effects;
        public NinjaNodeList Nodes;
        public NinjaObject Object;
        public NinjaMotion Motion;

        public override void Load(Stream fileStream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(fileStream) { Offset = 0x20 };

            // Use info node as file magic.
            string InfoNodeType = new string(reader.ReadChars(4)); // The Type of this Info Node.
            if (InfoNodeType != "NXIF")
                throw new InvalidSignatureException("NXIF", InfoNodeType); // Only supporting XN* (for now?).

            uint NodeLength        = reader.ReadUInt32(); // The length of the Info Node.
            uint DataChunkCount    = reader.ReadUInt32(); // How many other Nodes make up this NN file.
            uint DataChunkOffset   = reader.ReadUInt32(); // Seems to always be 0x20 (at least in XN*s)? Offset amount?
            uint DataSize          = reader.ReadUInt32(); // Footer Offset Table Start?
            uint OffsetTableOffset = reader.ReadUInt32(); // Footer Offset Table Data Start?
            uint OffsetTableSize   = reader.ReadUInt32(); // Footer Offset Table Length?
            uint Version           = reader.ReadUInt32(); // Seems to always be 1 (at least in XN*s)?

            // Loop through nodes.
            for (int i = 0; i < DataChunkCount; i++)
            {
                // Determine type of node to read and its length.
                string NextNodeType = new string(reader.ReadChars(4));
                uint NextNodeLength = reader.ReadUInt32();
                reader.JumpBehind(8);

                switch (NextNodeType)
                {
                    case "NXTL":
                        ReadNinjaTextures(reader);
                        break;

                    case "NXEF":
                    case "NXNN":
                    case "NXOB":
                        ReadNinjaNodeGeneric(reader);
                        break;

                    case "NXMO":
                    case "NXMA":
                        ReadNinjaMotion(reader);
                        break;

                    default:
                        reader.JumpAhead(8);
                        reader.JumpAhead(NextNodeLength);
                        Console.WriteLine($"Node {NextNodeType} not implemented!");
                        break;
                }
            }

            // TODO: Read Footer.
        }

        public void ReadNinjaTextures(ExtendedBinaryReader reader)
        {
            // Create the _NinjaTextureFileList so we can write to it.
            Textures = new List<NinjaTexture>();

            string TextureListNodeType = new string(reader.ReadChars(4)); // The type of this Texture List Node.
            uint NodeLength            = reader.ReadUInt32();             // The length of this Node.
            long returnPosition        = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset            = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the texture node's data.
            reader.JumpTo(NodeOffset, true);

            uint TextureCount      = reader.ReadUInt32(); // The amount of texture data in this Node.
            uint TextureListOffset = reader.ReadUInt32(); // The offset to the texture data in this Node.

            // Jump to the texture list.
            reader.JumpTo(TextureListOffset, true);

            // Loop through based on amount of textures in this Node.
            for (int i = 0; i < TextureCount; i++)
            {
                // Save texture entry into the _NinjaTextureFileList.
                Textures.Add(new NinjaTexture(reader));
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaNodeGeneric(ExtendedBinaryReader reader)
        {
            string NodeType = new string(reader.ReadChars(4)); // The Type of this Node Name List Node.
            uint NodeLength = reader.ReadUInt32();             // Length of this Node.

            long returnPosition = reader.BaseStream.Position; // Save position so we can use it with the length to jump to the next Node afterwards.

            uint NodeOffset = reader.ReadUInt32(); // Where this Node's Data starts.

            // Jump to the name list node's data.
            reader.JumpTo(NodeOffset, true);

            // Set lists based on input node.
            switch (NodeType)
            {
                case "NXEF":
                    Effects = new NinjaEffect(reader);
                    break;

                case "NXNN":
                    Nodes = new NinjaNodeList(reader);
                    break;

                case "NXOB":
                    Object = new NinjaObject(reader);
                    break;
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaMotion(ExtendedBinaryReader reader)
        {
            // TODO: holy shit rewrite this lol

            // Create the _NinjaMotion so we can write to it.
            Motion = new NinjaMotion();

            string MotionNodeType = new string(reader.ReadChars(4)); // The type of this Motion Node.
            uint NodeLength       = reader.ReadUInt32();                   // The length of this Node.
            long returnPosition   = reader.BaseStream.Position;        // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset       = reader.ReadUInt32();                   // Where this Node's Data starts.

            // Jump to the motion node's data.
            reader.JumpTo(NodeOffset, true);

            Motion.Type              = reader.ReadUInt32();
            Motion.StartFrame        = reader.ReadSingle();
            Motion.EndFrame          = reader.ReadSingle();
            uint SubMotionCount      = reader.ReadUInt32();
            uint SubMotionListOffset = reader.ReadUInt32();
            Motion.Framerate         = reader.ReadSingle();
            Motion.Reserved0         = reader.ReadUInt32();
            Motion.Reserved1         = reader.ReadUInt32();

            // Sub Motions
            reader.JumpTo(SubMotionListOffset, true);

            for (int i = 0; i < SubMotionCount; i++)
            {
                NinjaSubMotion _NinjaSubMotion    = new NinjaSubMotion();
                _NinjaSubMotion.SubMot_Type       = reader.ReadUInt32();  // This value is needed to figure out what the data should be read as. Is all in S06XnFile.h in LibS06
                _NinjaSubMotion.SubMot_IPType     = reader.ReadUInt32();
                _NinjaSubMotion.SubMot_ID         = reader.ReadUInt32();
                _NinjaSubMotion.SubMot_StartFrame = reader.ReadSingle();
                _NinjaSubMotion.SubMot_EndFrame   = reader.ReadSingle();
                _NinjaSubMotion.SubMot_StartKey   = reader.ReadSingle();
                _NinjaSubMotion.SubMot_EndKey     = reader.ReadSingle();
                uint KeyFrameCount                = reader.ReadUInt32();
                uint KeySize                      = reader.ReadUInt32();
                uint KeyOffset                    = reader.ReadUInt32();

                // Store current position to jump back to for the next sub motion.
                long position = reader.BaseStream.Position;

                // Keys
                reader.JumpTo(KeyOffset, true);
                for (int k = 0; k < KeyFrameCount; k++)
                {
                    NinjaSubMotionKey _NinjaSubMotionKey = new NinjaSubMotionKey();
                    switch (_NinjaSubMotion.SubMot_Type)
                    {
                        case 257:
                        case 513:
                        case 1025:
                        case 16777217:
                            _NinjaSubMotionKey.Key_Translation_MFRM = reader.ReadSingle();
                            _NinjaSubMotionKey.Key_Translation_MVAL = reader.ReadSingle();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;

                        case 2066:
                        case 4114:
                        case 8210:
                            _NinjaSubMotionKey.Key_Rotation_MFRM = reader.ReadInt16();
                            _NinjaSubMotionKey.Key_Rotation_MVAL = reader.ReadHalf();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;

                        case 3585:
                            _NinjaSubMotionKey.Key_Diffuse_MFRM = reader.ReadSingle();
                            _NinjaSubMotionKey.Key_Diffuse_MVAL = reader.ReadVector3();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;

                        default:
                            Console.WriteLine($"NinjaSubMotion Type of 0x{_NinjaSubMotion.SubMot_Type.ToString("X").PadLeft(8, '0')} ({_NinjaSubMotion.SubMot_Type}) not currently supported!");
                            break;
                    }
                }

                // Jump back to the saved position to read the next sub motion.
                reader.JumpTo(position);

                Motion.SubMotions.Add(_NinjaSubMotion);
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public unsafe override void Save(Stream fileStream)
        {
            // TODO: everything.

            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(fileStream);
            writer.Write("NXIF");
            writer.Write(0x18);
            int NodeCount = 0;
            if (Effects != null) { NodeCount++; }
            if (Motion != null) { NodeCount++; }
            if (Nodes != null) { NodeCount++; }
            if (Object != null) { NodeCount++; }
            if (Textures != null) { NodeCount++; }
            writer.Write(NodeCount);
            writer.Write(0x20);
            writer.Write("SIZE");
            writer.AddOffset("NOF0Offset");
            writer.Write("SIZE");
            writer.Write(1);

            if (Textures != null)
            {
                writer.Write("NXTL");
                writer.Write("SIZE");
                writer.AddOffset("NXTLDataOffset");
                writer.Write(0);
            }
        }
    }
}
