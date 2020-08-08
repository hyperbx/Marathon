namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public interface IOutputProvider
    {
        void print(string s);

        void println();
    }
}
