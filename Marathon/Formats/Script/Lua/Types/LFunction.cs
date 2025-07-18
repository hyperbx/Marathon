namespace Marathon.Formats.Script.Lua.Types
{
    public class LFunction
    (
        BHeader in_header,
        int[] in_code,
        LLocal[] in_locals,
        LObject[] in_constants,
        LUpvalue[] in_upvalues,
        LFunction[] in_functions,
        int in_maxStackSize,
        int in_upvalueCount,
        int in_paramCount,
        int in_variadicArgs
    )
    : BObject
    {
        public BHeader Header { get; set; } = in_header;

        public int[] Code { get; set; } = in_code;

        public LLocal[] Locals { get; set; } = in_locals;

        public LObject[] Constants { get; set; } = in_constants;

        public LUpvalue[] Upvalues { get; set; } = in_upvalues;

        public LFunction[] Functions { get; set; } = in_functions;

        public int MaximumStackSize { get; set; } = in_maxStackSize;

        public int UpvalueCount { get; set; } = in_upvalueCount;

        public int ParamCount { get; set; } = in_paramCount;

        public int VariadicArgs { get; set; } = in_variadicArgs;
    }
}
