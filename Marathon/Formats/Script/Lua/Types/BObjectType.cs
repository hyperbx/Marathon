namespace Marathon.Formats.Script.Lua.Types
{
    public abstract class BObjectType<T> where T : BObject
    {
        public abstract T Parse(BinaryReaderEx reader, BHeader header);

        public BList<T> ParseList(BinaryReaderEx reader, BHeader header)
        {
            BInteger length = header.Integer.Parse(reader, header);
            List<T> values = new();

            length.Iterate(Run);

            void Run()
                => values.Add(Parse(reader, header));

            return new BList<T>(length, values);
        }
    }
}
