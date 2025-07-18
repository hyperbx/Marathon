using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class LFunctionType : BObjectType<LFunction>
    {
        public override LFunction Parse(BinaryObjectReaderEx in_reader, BHeader in_header)
        {
            var state = new LFunctionParseState();

            ParseMain(in_reader, in_header, state);

            return new LFunction
            (
                in_header,
                state.Code,
                state.Locals.AsArray(new LLocal[state.Locals.Length.AsInt()]),
                state.Constants.AsArray(new LObject[state.Constants.Length.AsInt()]),
                state.Upvalues,
                state.Functions.AsArray(new LFunction[state.Functions.Length.AsInt()]),
                state.MaximumStackSize,
                state.LenUpvalues,
                state.LenParameter,
                state.Vararg
            );
        }

        public virtual void ParseMain(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Name = in_header.String.Parse(in_reader, in_header);
            in_state.LineBegin = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.LineEnd = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.LenUpvalues = in_reader.Read<byte>();
            in_state.LenParameter = in_reader.Read<byte>();
            in_state.Vararg = in_reader.Read<byte>();
            in_state.MaximumStackSize = in_reader.Read<byte>();

            ParseCode(in_reader, in_header, in_state);
            ParseConstants(in_reader, in_header, in_state);
            ParseUpvalues(in_reader, in_header, in_state);
            ParseDebug(in_reader, in_header, in_state);
        }

        public static void ParseCode(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Length = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.Code = new int[in_state.Length];

            for (int i = 0; i < in_state.Length; i++)
                in_state.Code[i] = in_reader.Read<int>();
        }

        public static void ParseConstants(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Constants = in_header.Constant.ParseList(in_reader, in_header);
            in_state.Functions = in_header.Function.ParseList(in_reader, in_header);
        }

        public virtual void ParseDebug(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Lines = in_header.Integer.ParseList(in_reader, in_header);
            in_state.Locals = in_header.Local.ParseList(in_reader, in_header);

            var upvalueNames = in_header.String.ParseList(in_reader, in_header);

            for (int i = 0; i < upvalueNames.Length.AsInt(); i++)
                in_state.Upvalues[i].Name = upvalueNames.Get(i).Dereference();
        }

        public virtual void ParseUpvalues(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Upvalues = new LUpvalue[in_state.LenUpvalues];

            for (int i = 0; i < in_state.LenUpvalues; i++)
                in_state.Upvalues[i] = new();
        }
    }

    public class LFunctionType50 : LFunctionType
    {
        public override void ParseMain(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Name = in_header.String.Parse(in_reader, in_header);
            in_state.LineBegin = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.LineEnd = 0;

            int lenUpvalues = in_reader.Read<byte>();
            in_state.Upvalues = new LUpvalue[lenUpvalues];

            for (int i = 0; i < lenUpvalues; i++)
                in_state.Upvalues[i] = new LUpvalue();

            in_state.LenParameter = in_reader.Read<byte>();
            in_state.Vararg = in_reader.Read<byte>();
            in_state.MaximumStackSize = in_reader.Read<byte>();

            ParseDebug(in_reader, in_header, in_state);
            ParseConstants(in_reader, in_header, in_state);
            ParseCode(in_reader, in_header, in_state);
        }

        public override void ParseUpvalues(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            BList<LUpvalue> upvalues = in_header.Upvalue.ParseList(in_reader, in_header);

            in_state.LenUpvalues = upvalues.Length.AsInt();
            in_state.Upvalues = upvalues.AsArray(new LUpvalue[in_state.LenUpvalues]);
        }
    }

    public class LFunctionType52 : LFunctionType
    {
        public override void ParseMain(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.LineBegin = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.LineEnd = in_header.Integer.Parse(in_reader, in_header).AsInt();
            in_state.LenParameter = in_reader.Read<byte>();
            in_state.Vararg = in_reader.Read<byte>();
            in_state.MaximumStackSize = in_reader.Read<byte>();

            ParseCode(in_reader, in_header, in_state);
            ParseConstants(in_reader, in_header, in_state);
            ParseUpvalues(in_reader, in_header, in_state);
            ParseDebug(in_reader, in_header, in_state);
        }

        public override void ParseDebug(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            in_state.Name = in_header.String.Parse(in_reader, in_header);

            ParseDebug(in_reader, in_header, in_state);
        }

        public override void ParseUpvalues(BinaryObjectReaderEx in_reader, BHeader in_header, LFunctionParseState in_state)
        {
            BList<LUpvalue> upvalues = in_header.Upvalue.ParseList(in_reader, in_header);

            in_state.LenUpvalues = upvalues.Length.AsInt();
            in_state.Upvalues = upvalues.AsArray(new LUpvalue[in_state.LenUpvalues]);
        }
    }

    public class LFunctionParseState
    {
        public LString Name { get; set; }

        public int LineBegin { get; set; }

        public int LineEnd { get; set; }

        public int LenUpvalues { get; set; }

        public int LenParameter { get; set; }

        public int Vararg { get; set; }

        public int MaximumStackSize { get; set; }

        public int Length { get; set; }

        public int[] Code { get; set; }

        public BList<LObject> Constants { get; set; }

        public BList<LFunction> Functions { get; set; }

        public BList<BInteger> Lines { get; set; }

        public BList<LLocal> Locals { get; set; }

        public LUpvalue[] Upvalues { get; set; }
    }
}
