// NinjaEffect.cs is licensed under the MIT License:
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
    public class NinjaEffect
    {
        public uint Type { get; set; }

        public uint EffectFilesCount { get; set; }

        public uint EffectFilesOffset { get; set; }

        public List<NinjaEffectFile> EffectFiles = new List<NinjaEffectFile>();

        public uint TechniqueNamesCount { get; set; }

        public uint TechniqueNamesOffset { get; set; }

        public List<NinjaTechnique> EffectTechniques = new List<NinjaTechnique>();

        public uint TechniqueIndicesCount { get; set; }

        public uint TechniqueIndicesOffset { get; set; }

        public List<short> TechniqueIndices = new List<short>();

        public NinjaEffect(ExtendedBinaryReader reader)
        {                       
            Type                   = reader.ReadUInt32();
            EffectFilesCount       = reader.ReadUInt32();
            EffectFilesOffset      = reader.ReadUInt32();
            TechniqueNamesCount    = reader.ReadUInt32();
            TechniqueNamesOffset   = reader.ReadUInt32();
            TechniqueIndicesCount  = reader.ReadUInt32();
            TechniqueIndicesOffset = reader.ReadUInt32();

            long position = reader.BaseStream.Position;

            // Jump to and read the effect files.
            reader.JumpTo(EffectFilesOffset, true);

            // Loop through based on amount of effect files in this effect list.
            for (int i = 0; i < EffectFilesCount; i++)
            {
                // Save effect file entry into EffectFileList in the _NinjaEffectList.
                EffectFiles.Add(new NinjaEffectFile(reader));
            }

            // Jump to and read the technique names.
            reader.JumpTo(TechniqueNamesOffset, true);

            // Loop through based on amount of techniques in this Node.
            for (int i = 0; i < TechniqueNamesCount; i++)
            {
                // Save technique entry into EffectTechniqueList in the _NinjaEffectList.
                EffectTechniques.Add(new NinjaTechnique(reader));
            }

            // Jump to and read the technique indices.
            reader.JumpTo(TechniqueIndicesOffset, true);

            // Loop through based on amount of technique ID entries in this Node and save them into TechniqueIDList in the _NinjaEffectList.
            for (int i = 0; i < TechniqueIndicesCount; i++)
            {
                TechniqueIndices.Add(reader.ReadInt16());
            }

            reader.JumpTo(position);
        }
    }

    public class NinjaEffectFile
    {
        public string Name { get; set; }

        public uint Type { get; set; }

        public uint NameOffset { get; set; }

        public NinjaEffectFile(ExtendedBinaryReader reader)
        {
            Type = reader.ReadUInt32();
            NameOffset = reader.ReadUInt32();

            // Store current position to jump back to.
            long position = reader.BaseStream.Position;

            // Jump to and read this effect's name.
            reader.JumpTo(NameOffset, true);
            Name = reader.ReadNullTerminatedString();

            // Jump back to the saved position.
            reader.JumpTo(position);
        }
    }
}
