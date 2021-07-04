using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Output
    {
        private IOutputProvider @out;
        private int indentationLevel = 0;
        private int position = 0;

        public Output() => new OutputProviderAnonymousInnerClass(this);

        private class OutputProviderAnonymousInnerClass : IOutputProvider
        {
            private readonly Output outerInstance;

            public OutputProviderAnonymousInnerClass(Output outerInstance) => this.outerInstance = outerInstance;

            public void print(string s) => Console.Write(s);

            public void println() => Console.WriteLine();
        }

        public Output(IOutputProvider @out) => this.@out = @out;

        public void indent() => indentationLevel += 2;

        public void dedent() => indentationLevel -= 2;

        public int getIndentationLevel() => indentationLevel;

        public int getPosition() => position;

        public void setIndentationLevel(int indentationLevel) => this.indentationLevel = indentationLevel;

        private void start()
        {
            if (position == 0)
            {
                for (int i = indentationLevel; i != 0; i--)
                {
                    @out.print(" ");
                    position++;
                }
            }
        }

        public void print(string s)
        {
            start();
            @out.print(s);
            position += s.Length;
        }

        public void println()
        {
            start();
            @out.println();
            position = 0;
        }

        public void println(string s)
        {
            print(s);
            println();
        }
    }
}
