using Marathon.Formats.Script.Lua.Types;
using System;
using System.Collections.Generic;
using System.Linq;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Constant
    {
        private readonly int _type;
        private readonly bool _bool;
        private readonly LNumber _number;
        private readonly LString _string;

        private static readonly HashSet<string> _keywords =
        [
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
        ];

        public Constant(int in_constant)
        {
            _type = 2;
            _bool = false;
            _number = LNumber.MakeInteger(in_constant);
            _string = null;
        }

        public Constant(LObject in_constant)
        {
            if (in_constant is LNil)
            {
                _type = 0;
                _bool = false;
                _number = null;
                _string = null;
            }
            else if (in_constant is LBoolean)
            {
                _type = 1;
                _bool = in_constant == LBoolean.True;
                _number = null;
                _string = null;
            }
            else if (in_constant is LNumber)
            {
                _type = 2;
                _bool = false;
                _number = (LNumber)in_constant;
                _string = null;
            }
            else if (in_constant is LString)
            {
                _type = 3;
                _bool = false;
                _number = null;
                _string = (LString)in_constant;
            }
            else
            {
                throw new Exception($"Illegal constant type: {in_constant}");
            }
        }

        public void Write(Output in_output)
        {
            switch (_type)
            {
                case 0:
                    in_output.Write("nil");
                    break;

                case 1:
                    in_output.Write(_bool ? "true" : "false");
                    break;

                case 2:
                    in_output.Write(_number.ToString());
                    break;

                case 3:
                {
                    var str = _string.Dereference();
                    var newlines = 0;
                    var unwritable = 0;
                    var isShiftJIS = _string.Encoding.WebName == "shift_jis";

                    for (int i = 0; i < str.Length; i++)
                    {
                        var c = str[i];

                        if (c == '\n')
                        {
                            newlines++;
                        }
                        else if ((c <= 31 && c != '\t') || c >= 127)
                        {
                            unwritable++;
                        }
                    }

                    if (unwritable == 0 && !str.Contains("[[") && (newlines > 1 || (newlines == 1 && str.IndexOf('\n') != str.Length - 1)))
                    {
                        var pipe = 0;
                        var pipeStr = "]]";

                        while (str.IndexOf(pipeStr) >= 0)
                        {
                            pipe++;
                            pipeStr = "]";

                            var i = pipe;

                            while (i-- > 0)
                                pipeStr += "=";

                            pipeStr += "]";
                        }

                        in_output.Write("[");

                        while (pipe-- > 0)
                            in_output.Write("=");

                        in_output.Write("[");

                        var currentIndentation = in_output.IndentationLevel;

                        in_output.IndentationLevel = 0;

                        in_output.WriteLine();
                        in_output.Write(str);
                        in_output.Write(pipeStr);

                        in_output.IndentationLevel = currentIndentation;
                    }
                    else
                    {
                        in_output.Write("\"");

                        for (int i = 0; i < str.Length; i++)
                        {
                            var c = str[i];

                            if (c <= 31 || c >= 127)
                            {
                                switch (c)
                                {
                                    case (char)7:
                                        in_output.Write("\\a");
                                        break;

                                    case (char)8:
                                        in_output.Write("\\b");
                                        break;

                                    case (char)9:
                                        in_output.Write("\\t");
                                        break;

                                    case (char)10:
                                        in_output.Write("\\n");
                                        break;

                                    case (char)11:
                                        in_output.Write("\\v");
                                        break;

                                    case (char)12:
                                        in_output.Write("\\f");
                                        break;

                                    case (char)13:
                                        in_output.Write("\\r");
                                        break;

                                    default:
                                    {
                                        if (isShiftJIS)
                                        {
                                            // FIX (Hyper): write Shift-JIS encoded strings.
                                            in_output.Write(c.ToString());
                                        }
                                        else
                                        {
                                            var bytes = _string.Encoding.GetBytes([c]);

                                            // FIX (Hyper): write unknown characters as decimal escape sequence.
                                            in_output.Write(string.Concat(bytes.Select(x => $"\\{x:D3}")));
                                        }

                                        break;
                                    }
                                }
                            }
                            else if (c == 34)
                            {
                                in_output.Write("\\\"");
                            }
                            else if (c == 92)
                            {
                                in_output.Write("\\\\");
                            }
                            else
                            {
                                in_output.Write(c.ToString());
                            }
                        }

                        in_output.Write("\"");
                    }

                    break;
                }

                default:
                    throw new Exception();
            }
        }

        public bool IsNil()
        {
            return _type == 0;
        }

        public bool IsBoolean()
        {
            return _type == 1;
        }

        public bool IsNumber()
        {
            return _type == 2;
        }

        public bool IsString()
        {
            return _type == 3;
        }

        public bool IsInteger()
        {
            return _number.Value() == Math.Round(_number.Value());
        }

        public bool IsIdentifier()
        {
            if (!IsString())
                return false;

            var str = _string.Dereference();

            if (_keywords.Contains(str) || (str.Length == 0))
                return false;

            var start = str[0];

            if (start != '_' && !char.IsLetter(start))
                return false;

            for (int i = 1; i < str.Length; i++)
            {
                var next = str[i];

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
            if (!IsString())
                throw new InvalidCastException("This constant's data type is not a string.");

            return _string.Dereference();
        }

        public int AsInteger()
        {
            if (!IsInteger())
                throw new InvalidCastException("This constant's data type is not an integer.");

            return (int)_number.Value();
        }
    }
}
