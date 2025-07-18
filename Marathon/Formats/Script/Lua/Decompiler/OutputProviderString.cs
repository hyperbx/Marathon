using System.Text;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class OutputProviderString : IOutputProvider
    {
        private readonly StringBuilder _sb = new();

        public void Write(string in_str)
        {
            _sb.Append(in_str);
        }

        public void WriteLine()
        {
            _sb.AppendLine();
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}
