// CompressedU8Archive.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 GerbilSoft
 * Copyright (c) 2020 HyperPolygon64
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.SonicNext
{
    /// <summary>
    /// File base for the Sonic '06 ARC format.
    /// </summary>
    public class CompressedU8Archive : FileBase
    {
        public const uint Signature = 0x55AA382D;
        public const string Extension = ".arc";

        public override void Load(Stream stream)
        {
            // Header
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            uint signature = reader.ReadUInt32();
            if (signature != Signature) throw new InvalidSignatureException(Signature.ToString(), signature.ToString());

            uint FileTableOffset = reader.ReadUInt32();
            uint FileTableLength = reader.ReadUInt32();
            uint FileDataOffset = reader.ReadUInt32();

            // TODO: Unknown.
            uint UnknownUInt32_2 = reader.ReadUInt32();
            uint UnknownUInt32_3 = reader.ReadUInt32();
            uint UnknownUInt32_4 = reader.ReadUInt32();
            uint UnknownUInt32_5 = reader.ReadUInt32();

            // TODO: Everything else...
        }
    }
}
