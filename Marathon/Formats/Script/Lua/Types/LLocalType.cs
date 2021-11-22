namespace Marathon.Formats.Script.Lua.Types
{
    public class LLocalType : BObjectType<LLocal>
    {
        public override LLocal Parse(BinaryReaderEx reader, BHeader header)
        {
            LString name = header.String.Parse(reader, header);

            BInteger start = header.Integer.Parse(reader, header),
                     end = header.Integer.Parse(reader, header);

            return new LLocal(name, start, end);
        }
    }
}
