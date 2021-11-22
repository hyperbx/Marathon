namespace Marathon.Formats.Script.Lua.Types
{
    public class LSourceLines
    {
        public static LSourceLines Parse(BinaryReaderEx reader)
        {
            int number = reader.ReadInt32();

            while (number-- > 0)
                reader.ReadInt32();

            return null;
        }
    }
}
