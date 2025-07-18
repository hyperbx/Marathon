namespace Marathon.Formats.Script.Lua.Decompiler
{
    public interface IOutputProvider
    {
        public void Write(string in_str);

        public void WriteLine();
    }
}
