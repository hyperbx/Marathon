using System;
using System.Text;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LStringType : BObjectType<LString>
    {
        protected StringBuilder InitialValue() => new();

        public override LString Parse(BinaryReaderEx reader, BHeader header)
        {
            BSizeT sizeT = header.SizeT.Parse(reader, header);
            StringBuilder b = new();

            sizeT.Iterate(Run);

            void Run()
                => b.Append((char)reader.ReadByte());

            string s = b.ToString();

            Console.WriteLine($"Parsed string: \"{s}\"");

            return new LString(sizeT, s);
        }
    }
}
