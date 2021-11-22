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

            return new LString(sizeT, b.ToString());
        }
    }
}
