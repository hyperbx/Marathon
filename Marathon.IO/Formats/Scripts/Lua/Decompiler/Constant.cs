using System;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Constant
    {
        private static readonly HashSet<string> reservedWords = new HashSet<string>()
        {
            "and",
            "break",
            "do",
            "else",
            "elseif",
            "end",
            "false",
            "for",
            "function",
            "if",
            "in",
            "local",
            "nil",
            "not",
            "or",
            "repeat",
            "return",
            "then",
            "true",
            "until",
            "while"
        };

        private readonly int type;
        private readonly bool @bool;
        private readonly LNumber number;
        private readonly string @string;

        public Constant(int constant)
        {
            type = 2;
            @bool = false;
            number = LNumber.makeInteger(constant);
            @string = null;
        }

        public Constant(LObject constant)
        {
            if (constant.GetType().Equals(typeof(LNil)))
            {
                type = 0;
                @bool = false;
                number = null;
                @string = null;
            }
            else if (constant.GetType().Equals(typeof(LBoolean)))
            {
                type = 1;
                @bool = constant == LBoolean.LTRUE;
                number = null;
                @string = null;
            }
            else if (constant.GetType().Equals(typeof(LNumber)))
            {
                type = 2;
                @bool = false;
                number = (LNumber)constant;
                @string = null;
            }
            else if (constant.GetType().Equals(typeof(LString)))
            {
                type = 3;
                @bool = false;
                number = null;
                @string = ((LString)constant).deref();
            }
            else
                throw new Exception($"Illegal constant type: {constant}");
        }

        public void print(Output @out)
        {
            switch (type)
            {
                case 0: @out.print("nil"); break;
                case 1: @out.print(@bool ? "true" : "false"); break;
                case 2: @out.print(number.toString()); break;

                case 3:
                {
                    int newlines = 0,
                        unprintable = 0;

                    for (int i = 0; i < @string.Length; i++)
                    {
                        char c = @string[i];

                        if (c == '\n')
                            newlines++;

                        else if ((c <= 31 && c != '\t') || c >= 127)
                            unprintable++;
                    }

                    if (unprintable == 0 && !@string.Contains("[[") && (newlines > 1 || (newlines == 1 && @string.IndexOf('\n') != @string.Length - 1)))
                    {
                        int pipe = 0;
                        string pipeString = "]]";

                        while (@string.IndexOf(pipeString) >= 0)
                        {
                            pipe++;
                            pipeString = "]";

                            int i = pipe;

                            while (i-- > 0) pipeString += "=";

                            pipeString += "]";
                        }

                        @out.print("[");

                        while (pipe-- > 0) @out.print("=");

                        @out.print("[");

                        int indent = @out.getIndentationLevel();

                        @out.setIndentationLevel(0);
                        @out.println();
                        @out.print(@string);
                        @out.print(pipeString);
                        @out.setIndentationLevel(indent);
                    }
                    else
                    {
                        @out.print("\"");

                        for (int i = 0; i < @string.Length; i++)
                        {
                            char c = @string[i];

                            if (c <= 31 || c >= 127)
                            {
                                if (c == 7)
                                    @out.print("\\a");

                                else if (c == 8)
                                    @out.print("\\b");

                                else if (c == 12)
                                    @out.print("\\f");

                                else if (c == 10)
                                    @out.print("\\n");

                                else if (c == 13)
                                    @out.print("\\r");

                                else if (c == 9)
                                    @out.print("\\t");

                                else if (c == 11)
                                    @out.print("\\v");

                                else
                                {
                                    string dec = c.ToString();
                                    int len = dec.Length;

                                    @out.print("\\");

                                    while (len++ < 3)
                                        @out.print("0");

                                    @out.print(dec);
                                }
                            }

                            else if (c == 34)
                                @out.print("\\\"");

                            else if (c == 92)
                                @out.print("\\\\");

                            else
                                @out.print(c.ToString());
                        }

                        @out.print("\"");
                    }

                    break;
                }

                default: throw new Exception();
            }
        }

        public bool isNil() => type == 0;

        public bool isBoolean() => type == 1;

        public bool isNumber() => type == 2;

        public bool isInteger() => number.value() == Math.Round(number.value());

        public int asInteger()
        {
            if (!isInteger())
                throw new Exception();

            return (int)number.value();
        }

        public bool isString() => type == 3;

        public bool isIdentifier()
        {
            if (!isString())
                return false;

            if (reservedWords.Contains(@string))
                return false;

            if (@string.Length == 0)
                return false;

            char start = @string[0];

            if (start != '_' && !char.IsLetter(start))
                return false;

            for (int i = 1; i < @string.Length; i++)
            {
                char next = @string[i];

                if (char.IsLetterOrDigit(next))
                    continue;

                if (next == '_')
                    continue;

                return false;
            }

            return true;
        }

        public string asName()
        {
            if (type != 3)
                throw new Exception();

            return @string;
        }
    }
}
