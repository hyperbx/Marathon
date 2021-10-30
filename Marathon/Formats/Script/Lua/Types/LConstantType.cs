using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LConstantType : BObjectType<LObject>
    {
        public override LObject Parse(BinaryReaderEx reader, BHeader header)
        {
            int type = reader.ReadByte();

            switch (type)
            {
                case 0:
                    return LNil.NIL;

                case 1:
                    return header.Bool.Parse(reader, header);

                case 3:
                    return header.Number.Parse(reader, header);

                case 4:
                    return header.String.Parse(reader, header);

                default:
                    throw new Exception();
            }
        }
    }
}
