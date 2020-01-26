/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)
 * Copyright (c) 2020 David Korth <gerbilsoft@gerbilsoft.com>

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Tools
{
    class ArcPacker
    {
        // Nodes: Count is total number of files and subdirectories,
        // plus 1 for the root node.
        // NOTE: class, not struct, for pass-by-reference behavior.
        protected class U8Node
        {
            // Original U8 fields.
            public uint type_name;          // High U8 is type: 0 == file, 1 == dir
                                            // Low 24-bit value is name offset.
            public uint data_offset;        // File: Offset to data.
                                            // Dir: Parent node number.
            public uint compressed_size;    // File: Compressed file size. (0 if uncompressed)
                                            // Dir: Number of child nodes.
            public uint file_size;          // File: Actual file size.

            // Temporary data for packing.
            public string srcFilename;      // File: Source filename.
        };

        // Node structures.
        private List<U8Node> _nodes = new List<U8Node>();

        // String table.
        // NOTE: The first byte is *always* 0. (root node name)
        private List<byte> _stringTable = new List<byte>();

        /// <summary>
        /// Create a directory node.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentNode"></param>
        /// <param name="childCount"></param>        /// <returns></returns>
        private U8Node createDirNode(string name, uint parentNode)
        {
            U8Node dirNode = new U8Node();
            dirNode.type_name = 0x01000000 | (uint)_stringTable.Count;
            dirNode.data_offset = parentNode;
            dirNode.compressed_size = 0;    // initialized later
            dirNode.file_size = 0;

            // Append the name to the string table.
            byte[] utfBytes = Encoding.UTF8.GetBytes(name);
            _stringTable.AddRange(utfBytes);
            _stringTable.Add(0);

            // Increment the child count of all parent nodes.
            if (_nodes.Count > 0)
            {
                U8Node nextParent = _nodes[(int)parentNode];
                U8Node curParent;
                do
                {
                    curParent = nextParent;
                    curParent.compressed_size++;
                    nextParent = _nodes[(int)curParent.data_offset];
                } while (nextParent != curParent);
            }

            _nodes.Add(dirNode);
            return dirNode;
        }

        /// <summary>
        /// Create a file node.
        /// </summary>
        /// <param name="parentNode">Parent node.</param>
        /// <param name="srcFilename">Source filename.</param>
        /// <returns></returns>
        private U8Node createFileNode(U8Node parentNode, string srcFilename)
        {
            U8Node fileNode = new U8Node();
            fileNode.type_name = (uint)_stringTable.Count;
            // These are initialized later.
            fileNode.data_offset = 0;
            fileNode.compressed_size = 0;
            fileNode.file_size = 0;
            fileNode.srcFilename = srcFilename;

            // Append the name to the string table.
            string name = Path.GetFileName(srcFilename);
            byte[] utfBytes = Encoding.UTF8.GetBytes(name);
            _stringTable.AddRange(utfBytes);
            _stringTable.Add(0);

            // Increment the child count of all parent nodes.
            U8Node nextParent = parentNode;
            U8Node curParent;
            do
            {
                curParent = nextParent;
                curParent.compressed_size++;
                nextParent = _nodes[(int)curParent.data_offset];
            } while (nextParent != curParent);

            _nodes.Add(fileNode);
            return fileNode;
        }

        /// <summary>
        /// Write the ARC file using the specified file list.
        /// </summary>
        /// <param name="arcFile">ARC file.</param>
        /// <param name="srcDirectory">Source directory.</param>
        public void WriteArc(string arcFile, string srcDirectory)
        {
            // TODO: Verify that subdirectories are after files in a directory.
            string[] files = Directory.GetFiles(srcDirectory, "*", SearchOption.AllDirectories);
            Array.Sort(files);

            // Directory nodes.
            Dictionary<string, U8Node> dirNodes = new Dictionary<string, U8Node>();

            // Standard U8 header
            // To be filled in:
            // - $0008 (BE32): Total size of node table and string table.
            // - $000C (BE32): Start of data.
            byte[] U8Header = new byte[32]
            {
                0x55, 0xAA, 0x38, 0x2D, 0x00, 0x00, 0x00, 0x20,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0xE4, 0xF9, 0x12, 0x00, 0x00, 0x00, 0x04, 0x02,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            // Create the root node.
            _nodes.Clear();
            _stringTable.Clear();
            U8Node rootNode = createDirNode("", 0);

            // Add child nodes.
            // The actual file data will be written once we
            // determine the total size of the node table and
            // string table.
            // TODO: Handle subdirectories properly.
            // TODO: Remove directory prefix.
            foreach (string file in files)
            {
                // Do we have a directory node here yet?
                string dirPath = Path.GetDirectoryName(file).Substring(srcDirectory.Length + 1);
                U8Node dirNode;
                if (String.IsNullOrEmpty(dirPath))
                {
                    // Root node.
                    dirNode = rootNode;
                }
                else if (dirNodes.ContainsKey(dirPath))
                {
                    // Directory already exists.
                    // TODO: Make sure we have the correct nodes. (sorting?)
                    dirNode = dirNodes[dirPath];
                }
                else
                {
                    // Directory does not exist yet.
                    // TODO: Get the proper parent node for the directory.
                    dirNode = createDirNode(dirPath, 0);
                    dirNodes[dirPath] = dirNode;
                }

                createFileNode(dirNode, file);
            }

            // Update the U8 header for the string table length
            // and data offset.
            int tableLength = (_nodes.Count * 16) + _stringTable.Count;
            int dataOffset = U8Header.Length + tableLength;
            dataOffset = ((dataOffset + 0x0F) & ~0x0F);

            byte[] b_tableLength = BitConverter.GetBytes((uint)tableLength);
            byte[] b_dataOffset = BitConverter.GetBytes((uint)dataOffset);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(b_tableLength);
                Array.Reverse(b_dataOffset);
            }
            Array.Copy(b_tableLength, 0, U8Header, 0x08, 4);
            Array.Copy(b_dataOffset, 0, U8Header, 0x0C, 4);

            // Write stuff.
            using (FileStream fs = File.Create(arcFile))
            {
                // Write the U8 header.
                fs.Write(U8Header, 0, U8Header.Length);

                // Write the nodes.
                foreach (U8Node node in _nodes) {
                    byte[] b_type_name = BitConverter.GetBytes(node.type_name);
                    byte[] b_data_offset = BitConverter.GetBytes(node.data_offset);
                    byte[] b_compressed_size = BitConverter.GetBytes(node.compressed_size);
                    byte[] b_file_size = BitConverter.GetBytes(node.file_size);

                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(b_type_name);
                        Array.Reverse(b_data_offset);
                        Array.Reverse(b_compressed_size);
                        Array.Reverse(b_file_size);
                    }

                    fs.Write(b_type_name, 0, b_type_name.Length);
                    fs.Write(b_data_offset, 0, b_data_offset.Length);
                    fs.Write(b_compressed_size, 0, b_compressed_size.Length);
                    fs.Write(b_file_size, 0, b_file_size.Length);
                }

                // Write the string table.
                fs.Write(_stringTable.ToArray(), 0, _stringTable.Count);

                // If the string table isn't a multiple of 16,
                // add more 0 bytes.
                for (int pos = _stringTable.Count; pos % 16 != 0; pos++)
                {
                    byte[] zero = new byte[] { 0 };
                    fs.Write(zero, 0, zero.Length);
                }

                fs.Flush();
                fs.Close();
            }
        }
    }
}
