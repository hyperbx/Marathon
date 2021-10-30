using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LBooleanType : BObjectType<LBoolean>
    {
        public override LBoolean Parse(BinaryReaderEx reader, BHeader header)
        {
            int value = reader.ReadByte();

            if ((value & 0xFFFFFFFE) != 0)
            {
                throw new Exception();
            }
            else
            {
                return value == 0 ? LBoolean.LFALSE : LBoolean.LTRUE;
            }
        }
    }
}
