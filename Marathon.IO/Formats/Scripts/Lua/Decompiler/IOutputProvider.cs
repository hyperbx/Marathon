namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public interface IOutputProvider
    {
        public void print(string s);

        public void println();
    }
}
