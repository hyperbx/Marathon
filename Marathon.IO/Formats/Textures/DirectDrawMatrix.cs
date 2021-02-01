// DirectDrawMatrix.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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

using System.IO;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Textures
{
    /// <summary>
    /// <para>File base for the DDM format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for storing multiple <see cref="DirectDraw"/> textures.</para>
    /// </summary>
    public class DirectDrawMatrix : FileBase
    {
        public DirectDrawMatrix() { }

        public DirectDrawMatrix(string file)
        {
            switch (Path.GetExtension(file))
            {
                // TODO: add importing.

                default:
                    Load(file);
                    break;
            }
        }

        public enum MatrixType
        {
            Root    = 0x204D4444, // DDM 
            Names   = 0x4E465344, // DSFN
            Texture = 0x4B435344  // DSCK
        }

        public class MatrixNode
        {
            /// <summary>
            /// The name of the current node.
            /// </summary>
            public string Name;

            /// <summary>
            /// The signature of the current node.
            /// </summary>
            public int Signature;

            /// <summary>
            /// The length of the current node.
            /// </summary>
            public uint Length;

            /// <summary>
            /// TODO: Unknown.
            /// </summary>
            public uint UnknownUInt32_1;

            /// <summary>
            /// TODO: Unknown - doesn't seem to ever be assigned.
            /// </summary>
            public uint UnknownUInt32_2;

            /// <summary>
            /// The data pertaining to this node.
            /// </summary>
            public byte[] Data;

            public MatrixType Type
                => (MatrixType)Signature;
        }

        public const string Extension = ".ddm";

        public List<MatrixNode> Nodes = new List<MatrixNode>();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

            List<string> names = new List<string>();

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                MatrixNode node = new MatrixNode
                {
                    Signature       = reader.ReadInt32(),
                    Length          = reader.ReadUInt32() - 8,
                    UnknownUInt32_1 = reader.ReadUInt32(),
                    UnknownUInt32_2 = reader.ReadUInt32()
                };

                if (node.Type == MatrixType.Names)
                {
                    long position = reader.BaseStream.Position;

                    while (reader.BaseStream.Position != (position + node.Length))
                    {
                        string name = reader.ReadNullTerminatedString();

                        // Add current string to the list.
                        if (!string.IsNullOrEmpty(name))
                            names.Add(name);
                    }
                }
                else if (node.Type == MatrixType.Texture)
                {
                    // Assign name to this node and remove it to prevent duplicates.
                    if (names.Count != 0)
                    {
                        node.Name = names[0];
                        names.RemoveAt(0);
                    }

                    node.Data = reader.ReadBytes((int)node.Length);
                }

                Nodes.Add(node);
            }
        }

        /* TODO: Add writing support - the game never uses this format, so this is very low priority.
                 Each node has 32-byte alignment, so there are an extra set of nulls at the end of each one. */
    }
}
