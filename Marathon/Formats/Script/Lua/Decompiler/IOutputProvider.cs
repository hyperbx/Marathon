namespace Marathon.Formats.Script.Lua.Decompiler
{
    public interface IOutputProvider
    {
        void Write(string str);

        void WriteLine();
    }
}
