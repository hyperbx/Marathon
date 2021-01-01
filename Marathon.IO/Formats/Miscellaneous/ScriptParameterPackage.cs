// CommonPackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
 * Copyright (c) 2021 Sajid
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
using System.IO;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    public class ScriptParameterPackage : FileBase
    {
        public List<ScriptParameter> Parameters { get; set; } = new List<ScriptParameter>();

        public override void Load(Stream fileStream)
        {
            var reader = new BINAReader(fileStream);
            reader.ReadHeader();

            var begin = reader.BaseStream.Position;
            var firstStringOffset = reader.ReadInt32() + reader.Offset;
            var count = firstStringOffset / ScriptParameter.Size;
            
            reader.JumpTo(begin);

            for (int i = 0; i < count; i++)
            {
                Parameters.Add(new ScriptParameter(reader));
            }
        }

        public override void Save(Stream fileStream)
        {
            var header = new BINAv1Header();
            var writer = new BINAWriter(fileStream, header);

            foreach (var parameter in Parameters)
            {
                parameter.Write(writer);
            }

            writer.WriteNulls(4);
            writer.FinishWrite(header);
        }
    }

    public class ScriptParameter
    {
        public const uint Size = 0x54;

        public string ScriptObject { get; set; }

        public float UnknownInt1 { get; set; }
        public float UnknownInt2 { get; set; }
        public float UnknownInt3 { get; set; }

        public float UnknownSingle1 { get; set; }
        public float UnknownSingle2 { get; set; }
        public float UnknownSingle3 { get; set; }
        public float UnknownSingle4 { get; set; }
        public float UnknownSingle5 { get; set; }
        public float UnknownSingle6 { get; set; }
        public float UnknownSingle7 { get; set; }
        public float UnknownSingle8 { get; set; }
        public float UnknownSingle9 { get; set; }
        public float UnknownSingle10 { get; set; }
        public float UnknownSingle11 { get; set; }
        public float UnknownSingle12 { get; set; }
        public float UnknownSingle13 { get; set; }
        public float UnknownSingle14 { get; set; }
        public float UnknownSingle15 { get; set; }
        public float UnknownSingle16 { get; set; }
        public float UnknownSingle17 { get; set; }

        public ScriptParameter()
        {

        }

        public ScriptParameter(BINAReader reader)
        {
            Read(reader);
        }

        public void Read(BINAReader reader)
        {
            var nameOffset = reader.ReadInt32();
            UnknownInt1 = reader.ReadInt32();
            UnknownInt2 = reader.ReadInt32();
            UnknownInt3 = reader.ReadInt32();

            UnknownSingle1 = reader.ReadSingle();
            UnknownSingle2 = reader.ReadSingle();
            UnknownSingle3 = reader.ReadSingle();
            UnknownSingle4 = reader.ReadSingle();

            UnknownSingle5 = reader.ReadSingle();
            UnknownSingle6 = reader.ReadSingle();
            UnknownSingle7 = reader.ReadSingle();
            UnknownSingle8 = reader.ReadSingle();

            UnknownSingle9 = reader.ReadSingle();
            UnknownSingle10 = reader.ReadSingle();
            UnknownSingle11 = reader.ReadSingle();
            UnknownSingle12 = reader.ReadSingle();

            UnknownSingle13 = reader.ReadSingle();
            UnknownSingle14 = reader.ReadSingle();
            UnknownSingle15 = reader.ReadSingle();
            UnknownSingle16 = reader.ReadSingle();

            UnknownSingle17 = reader.ReadSingle();

            var pos = reader.BaseStream.Position;

            reader.JumpTo(nameOffset, true);
            ScriptObject = reader.ReadNullTerminatedString();

            reader.JumpTo(pos);
        }

        public void Write(BINAWriter writer)
        {
            writer.AddString($"StringOffset{writer.BaseStream.Position}", ScriptObject);
            writer.Write(UnknownInt1);
            writer.Write(UnknownInt2);
            writer.Write(UnknownInt3);

            writer.Write(UnknownSingle1);
            writer.Write(UnknownSingle2);
            writer.Write(UnknownSingle3);
            writer.Write(UnknownSingle4);

            writer.Write(UnknownSingle5);
            writer.Write(UnknownSingle6);
            writer.Write(UnknownSingle7);
            writer.Write(UnknownSingle8);

            writer.Write(UnknownSingle9);
            writer.Write(UnknownSingle10);
            writer.Write(UnknownSingle11);
            writer.Write(UnknownSingle12);

            writer.Write(UnknownSingle13);
            writer.Write(UnknownSingle14);
            writer.Write(UnknownSingle15);
            writer.Write(UnknownSingle16);

            writer.Write(UnknownSingle17);
        }

        public override string ToString()
        {
            return ScriptObject;
        }
    }
}
