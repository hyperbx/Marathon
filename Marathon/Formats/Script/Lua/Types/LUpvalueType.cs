namespace Marathon.Formats.Script.Lua.Types
{
    public class LUpvalueType : BObjectType<LUpvalue>
    {
        public override LUpvalue Parse(BinaryReaderEx reader, BHeader header)
        {
            LUpvalue upvalue = new()
            {
                InStack = reader.ReadByte() != 0,
                Index = reader.ReadByte()
            };

            return upvalue;
        }
    }
}
