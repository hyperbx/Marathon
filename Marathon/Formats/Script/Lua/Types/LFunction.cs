namespace Marathon.Formats.Script.Lua.Types
{
    public class LFunction : BObject
    {
        public BHeader Header;
        public int[] Code;
        public LLocal[] Locals;
        public LObject[] Constants;
        public LUpvalue[] Upvalues;
        public LFunction[] Functions;

        public int MaximumStackSize,
                   NumUpvalues,
                   NumParams,
                   Vararg;

        public LFunction(BHeader header, int[] code, LLocal[] locals, LObject[] constants, LUpvalue[] upvalues, LFunction[] functions, int maximumStackSize, int numUpvalues, int numParams, int vararg)
        {
            Header = header;
            Code = code;
            Locals = locals;
            Constants = constants;
            Upvalues = upvalues;
            Functions = functions;
            MaximumStackSize = maximumStackSize;
            NumUpvalues = numUpvalues;
            NumParams = numParams;
            Vararg = vararg;
        }
    }
}
