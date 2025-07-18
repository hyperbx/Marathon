using System;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Output(IOutputProvider in_outputProvider, IndentationType in_indentationType = IndentationType.Spaces)
    {
        private int _position = 0;

        public IndentationType IndentationType { get; } = in_indentationType;

        public int IndentationLevel { get; set; } = 0;

        public void Indent()
        {
            IndentationLevel += IndentationType == IndentationType.Spaces ? 4 : 1;
        }

        public void Dedent()
        {
            IndentationLevel -= IndentationType == IndentationType.Spaces ? 4 : 1;
        }

        private void Start()
        {
            if (_position != 0)
                return;

            for (int i = IndentationLevel; i != 0; i--)
            {
                in_outputProvider.Write(IndentationType == IndentationType.Spaces ? " " : "\t");

                _position++;
            }
        }

        public void Write(string in_str)
        {
            Start();

            in_outputProvider.Write(in_str);

            _position += in_str.Length;
        }

        public void WriteLine()
        {
            Start();

            in_outputProvider.WriteLine();

            _position = 0;
        }

        public void WriteLine(string in_str)
        {
            Write(in_str);
            WriteLine();
        }
    }
}
