namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LSourceLines
    {
        public static LSourceLines parse(ExtendedBinaryReader buffer)
        {
            int number = buffer.ReadInt32();

            while (number-- > 0)
                buffer.ReadInt32();

            return null;
        }
    }
}
