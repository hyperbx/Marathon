using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Constant
    {
        private static readonly HashSet<string> reservedWords = new()
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

        private readonly int _type;
        private readonly bool _bool;
        private readonly LNumber _number;
        private readonly string _string;

        public Constant(int constant)
        {
            _type = 2;
            _bool = false;
            _number = LNumber.MakeInteger(constant);
            _string = null;
        }

        public Constant(LObject constant)
        {
            if (constant is LNil)
            {
                _type = 0;
                _bool = false;
                _number = null;
                _string = null;
            }
            else if (constant is LBoolean)
            {
                _type = 1;
                _bool = constant == LBoolean.LTRUE;
                _number = null;
                _string = null;
            }
            else if (constant is LNumber)
            {
                _type = 2;
                _bool = false;
                _number = (LNumber)constant;
                _string = null;
            }
            else if (constant is LString)
            {
                _type = 3;
                _bool = false;
                _number = null;
                _string = ((LString)constant).Dereference();
            }
            else
            {
                throw new Exception($"Illegal constant type: {constant}");
            }
        }

        public void Write(Output @out)
        {
            switch (_type)
            {
                case 0:
                    @out.Write("nil");
                    break;

                case 1:
                    @out.Write(_bool ? "true" : "false");
                    break;

                case 2:
                    @out.Write(_number.ToString());
                    break;

                case 3:
                {
                    int newlines = 0,
                        unwritable = 0;

                    for (int i = 0; i < _string.Length; i++)
                    {
                        char c = _string[i];

                        if (c == '\n')
                        {
                            newlines++;
                        }
                        else if ((c <= 31 && c != '\t') || c >= 127)
                        {
                            unwritable++;
                        }
                    }

                    if (unwritable == 0 && !_string.Contains("[[") && (newlines > 1 || (newlines == 1 && _string.IndexOf('\n') != _string.Length - 1)))
                    {
                        int pipe = 0;
                        string pipeString = "]]";

                        while (_string.IndexOf(pipeString) >= 0)
                        {
                            pipe++;
                            pipeString = "]";

                            int i = pipe;

                            while (i-- > 0)
                                pipeString += "=";

                            pipeString += "]";
                        }

                        @out.Write("[");

                        while (pipe-- > 0)
                            @out.Write("=");

                        @out.Write("[");

                        int indent = @out.GetIndentationLevel();

                        @out.SetIndentationLevel(0);
                        @out.WriteLine();
                        @out.Write(_string);
                        @out.Write(pipeString);
                        @out.SetIndentationLevel(indent);
                    }
                    else
                    {
                        @out.Write("\"");

                        for (int i = 0; i < _string.Length; i++)
                        {
                            char c = _string[i];

                            if (c <= 31 || c >= 127)
                            {
                                switch (c)
                                {
                                    case (char)7:
                                        @out.Write("\\a");
                                        break;

                                    case (char)8:
                                        @out.Write("\\b");
                                        break;

                                    case (char)9:
                                        @out.Write("\\t");
                                        break;

                                    case (char)10:
                                        @out.Write("\\n");
                                        break;

                                    case (char)11:
                                        @out.Write("\\v");
                                        break;

                                    case (char)12:
                                        @out.Write("\\f");
                                        break;

                                    case (char)13:
                                        @out.Write("\\r");
                                        break;

                                    default:
                                    {
                                        string dec = c.ToString();
                                        int len = dec.Length;

                                        @out.Write("\\");

                                        while (len++ < 3)
                                            @out.Write("0");

                                        @out.Write(dec);

                                        break;
                                    }
                                }
                            }
                            else if (c == 34)
                            {
                                @out.Write("\\\"");
                            }
                            else if (c == 92)
                            {
                                @out.Write("\\\\");
                            }
                            else
                            {
                                @out.Write(c.ToString());
                            }
                        }

                        @out.Write("\"");
                    }

                    break;
                }

                default:
                    throw new Exception();
            }
        }

        public bool IsNil() => _type == 0;

        public bool IsBoolean() => _type == 1;

        public bool IsNumber() => _type == 2;

        public bool IsInteger() => _number.Value() == Math.Round(_number.Value());

        public int AsInteger()
        {
            if (!IsInteger())
                throw new Exception();

            return (int)_number.Value();
        }

        public bool IsString() => _type == 3;

        public bool IsIdentifier()
        {
            if (!IsString())
                return false;

            if (reservedWords.Contains(_string))
                return false;

            if (_string.Length == 0)
                return false;

            char start = _string[0];

            if (start != '_' && !char.IsLetter(start))
                return false;

            for (int i = 1; i < _string.Length; i++)
            {
                char next = _string[i];

                if (char.IsLetterOrDigit(next))
                    continue;

                if (next == '_')
                    continue;

                return false;
            }

            return true;
        }

        public string AsName()
        {
            if (_type != 3)
                throw new Exception();

            return _string;
        }
    }
}
