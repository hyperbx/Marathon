using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LFunctionType : BObjectType<LFunction>
    {
        public static readonly LFunctionType TYPE50 = new LFunctionType50();
        public static readonly LFunctionType TYPE51 = new LFunctionType();
        public static readonly LFunctionType TYPE52 = new LFunctionType52();

        protected class LFunctionParseState
        {
            public LString name;

            public int lineBegin,
                       lineEnd,
                       lenUpvalues,
                       lenParameter,
                       vararg,
                       maximumStackSize,
                       length;

            public int[] code;

            public BList<LObject> constants;
            public BList<LFunction> functions;
            public BList<BInteger> lines;
            public BList<LLocal> locals;
            public LUpvalue[] upvalues;
        }

        public override LFunction parse(ExtendedBinaryReader buffer, BHeader header)
        {
            if (header.debug)
            {
                Console.WriteLine("-- beginning to parse function");
                Console.WriteLine("-- parsing name...start...end...upvalues...params...varargs...stack");
            }

            LFunctionParseState s = new LFunctionParseState();
            parse_main(buffer, header, s);

            return new LFunction(header,
                                 s.code,
                                 s.locals.asArray(new LLocal[s.locals.length.asInt()]),
                                 s.constants.asArray(new LObject[s.constants.length.asInt()]),
                                 s.upvalues,
                                 s.functions.asArray(new LFunction[s.functions.length.asInt()]),
                                 s.maximumStackSize,
                                 s.lenUpvalues,
                                 s.lenParameter,
                                 s.vararg);
        }

        protected void parse_main(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            s.name = header.@string.parse(buffer, header);
            s.lineBegin = header.integer.parse(buffer, header).asInt();
            s.lineEnd = header.integer.parse(buffer, header).asInt();
            s.lenUpvalues = 0xFF & buffer.ReadByte();
            s.lenParameter = 0xFF & buffer.ReadByte();
            s.vararg = 0xFF & buffer.ReadByte();
            s.maximumStackSize = 0xFF & buffer.ReadByte();

            parse_code(buffer, header, s);
            parse_constants(buffer, header, s);
            parse_upvalues(buffer, header, s);
            parse_debug(buffer, header, s);
        }

        protected void parse_code(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            if (header.debug)
                Console.WriteLine("-- beginning to parse bytecode list");

            s.length = header.integer.parse(buffer, header).asInt();
            s.code = new int[s.length];

            for (int i = 0; i < s.length; i++)
            {
                s.code[i] = buffer.ReadInt32();

                if (header.debug)
                    Console.WriteLine($"-- parsed codepoint {s.code[i]:X}");
            }
        }

        protected void parse_constants(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            if (header.debug)
                Console.WriteLine("-- beginning to parse constants list");

            s.constants = header.constant.parseList(buffer, header);

            if (header.debug)
                Console.WriteLine("-- beginning to parse functions list");

            s.functions = header.function.parseList(buffer, header);
        }

        protected void parse_debug(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            if (header.debug)
                Console.WriteLine("-- beginning to parse source lines list");

            s.lines = header.integer.parseList(buffer, header);

            if (header.debug)
                Console.WriteLine("-- beginning to parse locals list");

            s.locals = header.local.parseList(buffer, header);

            if (header.debug)
                Console.WriteLine("-- beginning to parse upvalues list");

            BList<LString> upvalueNames = header.@string.parseList(buffer, header);

            for (int i = 0; i < upvalueNames.length.asInt(); i++)
                s.upvalues[i].name = upvalueNames.get(i).deref();
        }

        protected void parse_upvalues(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            s.upvalues = new LUpvalue[s.lenUpvalues];

            for (int i = 0; i < s.lenUpvalues; i++)
                s.upvalues[i] = new LUpvalue();
        }
    }

    class LFunctionType52 : LFunctionType
    {
        protected void parse_main(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            s.lineBegin = header.integer.parse(buffer, header).asInt();
            s.lineEnd = header.integer.parse(buffer, header).asInt();
            s.lenParameter = 0xFF & buffer.ReadByte();
            s.vararg = 0xFF & buffer.ReadByte();
            s.maximumStackSize = 0xFF & buffer.ReadByte();
            parse_code(buffer, header, s);
            parse_constants(buffer, header, s);
            parse_upvalues(buffer, header, s);
            parse_debug(buffer, header, s);
        }

        protected void parse_debug(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            s.name = header.@string.parse(buffer, header);
            parse_debug(buffer, header, s);
        }

        protected void parse_upvalues(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            BList<LUpvalue> upvalues = header.upvalue.parseList(buffer, header);

            s.lenUpvalues = upvalues.length.asInt();
            s.upvalues = upvalues.asArray(new LUpvalue[s.lenUpvalues]);
        }
    }

    class LFunctionType50 : LFunctionType
    {
        protected void parse_main(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            s.name = header.@string.parse(buffer, header);
            s.lineBegin = header.integer.parse(buffer, header).asInt();
            s.lineEnd = 0;
            int lenUpvalues = 0xFF & buffer.ReadByte();
            s.upvalues = new LUpvalue[lenUpvalues];

            for (int i = 0; i < lenUpvalues; i++)
                s.upvalues[i] = new LUpvalue();

            s.lenParameter = 0xFF & buffer.ReadByte();
            s.vararg = 0xFF & buffer.ReadByte();
            s.maximumStackSize = 0xFF & buffer.ReadByte();

            parse_debug(buffer, header, s);
            parse_constants(buffer, header, s);
            parse_code(buffer, header, s);
        }

        protected void parse_debug(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
            => parse_debug(buffer, header, s);

        protected void parse_upvalues(ExtendedBinaryReader buffer, BHeader header, LFunctionParseState s)
        {
            BList<LUpvalue> upvalues = header.upvalue.parseList(buffer, header);

            s.lenUpvalues = upvalues.length.asInt();
            s.upvalues = upvalues.asArray(new LUpvalue[s.lenUpvalues]);
        }
    }
}
