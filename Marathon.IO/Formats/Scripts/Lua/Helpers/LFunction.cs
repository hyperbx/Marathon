namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LFunction : BObject
    {
        public BHeader header;
        public int[] code;
        public LLocal[] locals;
        public LObject[] constants;
        public LUpvalue[] upvalues;
        public LFunction[] functions;
        public int maximumStackSize,
                   numUpvalues,
                   numParams,
                   vararg;

        public LFunction(BHeader header, int[] code, LLocal[] locals, LObject[] constants, LUpvalue[] upvalues, LFunction[] functions, int maximumStackSize, int numUpvalues, int numParams, int vararg)
        {
            this.header = header;
            this.code = code;
            this.locals = locals;
            this.constants = constants;
            this.upvalues = upvalues;
            this.functions = functions;
            this.maximumStackSize = maximumStackSize;
            this.numUpvalues = numUpvalues;
            this.numParams = numParams;
            this.vararg = vararg;
        }
    }
}
