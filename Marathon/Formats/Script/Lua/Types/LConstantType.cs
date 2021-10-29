using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LConstantType : BObjectType<LObject>
    {
        public override LObject Parse(BinaryReaderEx reader, BHeader header)
        {
            int type = reader.ReadByte() - 1;

            Console.WriteLine("Parsing constant of type: ");

            switch (type)
            {
                case 0:
                    Console.Write("nil\n");
                    return LNil.NIL;

                case 1:
                    Console.Write("Boolean\n");
                    return header.Bool.Parse(reader, header);

                case 2:
                    Console.Write("number\n");
                    return header.Number.Parse(reader, header);

                case 3:
                    Console.Write("string\n");
                    return header.String.Parse(reader, header);

                default:
                    Console.Write($"{type} (illegal)\n");
                    throw new Exception();
            }
        }
    }
}
