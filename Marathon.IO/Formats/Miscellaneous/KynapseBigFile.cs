// KynapseBigFile.cs is licensed under the MIT License:
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// <para>File base for the KBF format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for miscellaneous AI stuff.</para>
    /// </summary>
    public class KynapseBigFile : FileBase
    {
        public KynapseBigFile() { }

        public KynapseBigFile(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".xml":
                    // ImportXML(file); // TODO: add XML reading/writing.
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public struct KynapseParameter
        {
            public byte NameLength;

            public string Name;

            public byte ValueLength;

            public string Value;
        }

        public const string Signature = "KS BIG FILE",
                            Extension = ".kbf";

        public int Version;

        public List<KynapseParameter> Parameters = new List<KynapseParameter>();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

            string signature = reader.ReadSignature(Signature.Length);
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            Version = reader.ReadByte();

            // TODO
        }
    }
}
