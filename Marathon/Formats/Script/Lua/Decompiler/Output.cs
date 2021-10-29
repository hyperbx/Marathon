using System;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Output
    {
        private IOutputProvider _out;
        private int _indentationLevel = 0;
        private int _position = 0;

        public Output() => new OutputProviderAnonymousInnerClass(this);

        private class OutputProviderAnonymousInnerClass : IOutputProvider
        {
            private readonly Output _outerInstance;

            public OutputProviderAnonymousInnerClass(Output outerInstance) => _outerInstance = outerInstance;

            public void Write(string str)
                => Console.Write(str);

            public void WriteLine()
                => Console.WriteLine();
        }

        public Output(IOutputProvider @out) => _out = @out;

        public void Indent()
            => _indentationLevel += 2;

        public void Dedent()
            => _indentationLevel -= 2;

        public int GetIndentationLevel() => _indentationLevel;

        public int GetPosition() => _position;

        public void SetIndentationLevel(int indentationLevel) => _indentationLevel = indentationLevel;

        private void Start()
        {
            if (_position == 0)
            {
                for (int i = _indentationLevel; i != 0; i--)
                {
                    _out.Write(" ");
                    _position++;
                }
            }
        }

        public void Write(string str)
        {
            Start();
            _out.Write(str);
            _position += str.Length;
        }

        public void WriteLine()
        {
            Start();
            _out.WriteLine();
            _position = 0;
        }

        public void WriteLine(string str)
        {
            Write(str);
            WriteLine();
        }
    }
}
