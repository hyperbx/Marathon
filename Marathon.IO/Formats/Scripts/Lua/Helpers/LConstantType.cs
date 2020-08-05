using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LConstantType : BObjectType<LObject>
    {
        public override LObject parse(ExtendedBinaryReader buffer, BHeader header)
        {
            int type = 0xFF & buffer.ReadByte();

            if (header.debug)
                Console.WriteLine("-- parsing <constant>, type is...");

            switch (type)
            {
                case 0:
                {
                    if (header.debug)
                        Console.WriteLine("<nil>");

                    return LNil.NIL;
                }

                case 1:
                {
                    if (header.debug)
                        Console.WriteLine("<boolean>");

                    return header.@bool.parse(buffer, header);
                }

                case 2:
                {
                    if (header.debug)
                        Console.WriteLine("<number>");

                    return header.number.parse(buffer, header);
                }

                case 3:
                {
                    if (header.debug)
                        Console.WriteLine("<string>");

                    return header.@string.parse(buffer, header);
                }

                default:
                {
                    if (header.debug)
                        Console.WriteLine($"Illegal constant: {type}");

                    throw new Exception();
                }
            }
        }
    }
}
