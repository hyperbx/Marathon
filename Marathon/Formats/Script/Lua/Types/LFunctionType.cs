using System;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LFunctionType : BObjectType<LFunction>
    {
        public static readonly LFunctionType TYPE50 = new LFunctionType50();
        public static readonly LFunctionType TYPE51 = new();
        public static readonly LFunctionType TYPE52 = new LFunctionType52();

        public class LFunctionParseState
        {
            public LString Name;

            public int LineBegin,
                       LineEnd,
                       LenUpvalues,
                       LenParameter,
                       Vararg,
                       MaximumStackSize,
                       Length;

            public int[] Code;

            public BList<LObject> Constants;
            public BList<LFunction> Functions;
            public BList<BInteger> Lines;
            public BList<LLocal> Locals;
            public LUpvalue[] Upvalues;
        }

        public override LFunction Parse(BinaryReaderEx reader, BHeader header)
        {
            Console.WriteLine("Parsing function...");

            LFunctionParseState s = new();
            ParseMain(reader, header, s);

            return new LFunction
            (
                header,
                s.Code,
                s.Locals.AsArray(new LLocal[s.Locals.Length.AsInt()]),
                s.Constants.AsArray(new LObject[s.Constants.Length.AsInt()]),
                s.Upvalues,
                s.Functions.AsArray(new LFunction[s.Functions.Length.AsInt()]),
                s.MaximumStackSize,
                s.LenUpvalues,
                s.LenParameter,
                s.Vararg
            );
        }

        public virtual void ParseMain(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            s.Name = header.String.Parse(reader, header);
            s.LineBegin = header.Integer.Parse(reader, header).AsInt();
            s.LineEnd = header.Integer.Parse(reader, header).AsInt();
            s.LenUpvalues = reader.ReadByte();
            s.LenParameter = reader.ReadByte();
            s.Vararg = reader.ReadByte();
            s.MaximumStackSize = reader.ReadByte();

            ParseCode(reader, header, s);
            ParseConstants(reader, header, s);
            ParseUpvalues(reader, header, s);
            ParseDebug(reader, header, s);
        }

        public void ParseCode(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            Console.WriteLine("Parsing bytecode...");

            s.Length = header.Integer.Parse(reader, header).AsInt();
            s.Code = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                s.Code[i] = reader.ReadInt32();
                Console.WriteLine($"Parsed codepoint: {s.Code[i]:X}");
            }
        }

        public void ParseConstants(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            Console.WriteLine("Parsing constants list...");
            s.Constants = header.Constant.ParseList(reader, header);

            Console.WriteLine("Parsing functions list...");
            s.Functions = header.Function.ParseList(reader, header);
        }

        public virtual void ParseDebug(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            Console.WriteLine("Parsing source lines...");
            s.Lines = header.Integer.ParseList(reader, header);

            Console.WriteLine("Parsing locals...");
            s.Locals = header.Local.ParseList(reader, header);

            Console.WriteLine("Parsing upvalues...");
            BList<LString> upvalueNames = header.String.ParseList(reader, header);

            for (int i = 0; i < upvalueNames.Length.AsInt(); i++)
                s.Upvalues[i].Name = upvalueNames.Get(i).Dereference();
        }

        public virtual void ParseUpvalues(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            s.Upvalues = new LUpvalue[s.LenUpvalues];

            for (int i = 0; i < s.LenUpvalues; i++)
                s.Upvalues[i] = new LUpvalue();
        }
    }

    class LFunctionType50 : LFunctionType
    {
        public override void ParseMain(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            s.Name = header.String.Parse(reader, header);
            s.LineBegin = header.Integer.Parse(reader, header).AsInt();
            s.LineEnd = 0;

            int lenUpvalues = reader.ReadByte();
            s.Upvalues = new LUpvalue[lenUpvalues];

            for (int i = 0; i < lenUpvalues; i++)
                s.Upvalues[i] = new LUpvalue();

            s.LenParameter = reader.ReadByte();
            s.Vararg = reader.ReadByte();
            s.MaximumStackSize = reader.ReadByte();

            ParseDebug(reader, header, s);
            ParseConstants(reader, header, s);
            ParseCode(reader, header, s);
        }

        public override void ParseUpvalues(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            BList<LUpvalue> upvalues = header.Upvalue.ParseList(reader, header);

            s.LenUpvalues = upvalues.Length.AsInt();
            s.Upvalues = upvalues.AsArray(new LUpvalue[s.LenUpvalues]);
        }
    }

    class LFunctionType52 : LFunctionType
    {
        public override void ParseMain(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            s.LineBegin = header.Integer.Parse(reader, header).AsInt();
            s.LineEnd = header.Integer.Parse(reader, header).AsInt();
            s.LenParameter = reader.ReadByte();
            s.Vararg = reader.ReadByte();
            s.MaximumStackSize = reader.ReadByte();

            ParseCode(reader, header, s);
            ParseConstants(reader, header, s);
            ParseUpvalues(reader, header, s);
            ParseDebug(reader, header, s);
        }

        public override void ParseDebug(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            s.Name = header.String.Parse(reader, header);

            ParseDebug(reader, header, s);
        }

        public override void ParseUpvalues(BinaryReaderEx reader, BHeader header, LFunctionParseState s)
        {
            BList<LUpvalue> upvalues = header.Upvalue.ParseList(reader, header);

            s.LenUpvalues = upvalues.Length.AsInt();
            s.Upvalues = upvalues.AsArray(new LUpvalue[s.LenUpvalues]);
        }
    }
}
