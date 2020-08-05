using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LBooleanType : BObjectType<LBoolean>
    {
        public override LBoolean parse(ExtendedBinaryReader buffer, BHeader header)
        {
            int value = buffer.ReadByte();

            if ((value & 0xFFFFFFFE) != 0)
                throw new Exception();

            else
            {
                LBoolean @bool = value == 0 ? LBoolean.LFALSE : LBoolean.LTRUE;

                if (header.debug)
                    Console.WriteLine($"-- parsed <boolean> {@bool}");

                return @bool;
            }
        }
    }
}
