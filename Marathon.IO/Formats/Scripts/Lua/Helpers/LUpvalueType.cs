namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LUpvalueType : BObjectType<LUpvalue>
    {
        public override LUpvalue parse(ExtendedBinaryReader buffer, BHeader header)
        {
            LUpvalue upvalue = new LUpvalue
            {
                instack = buffer.ReadByte() != 0,
                idx = 0xFF & buffer.ReadByte()
            };

            return upvalue;
        }
    }
}
