using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler;

namespace Marathon.Formats.Script.Lua.Version
{
    public abstract class VersionBase(LuaVersion in_version)
    {
        public LuaVersion Version => in_version;

        public abstract bool HasHeaderTail();

        public abstract bool HasFormat();

        public abstract LFunctionType GetLFunctionType();

        public OpcodeMap GetOpcodeMap()
        {
            return new(Version);
        }

        public abstract int GetOuterBlockScopeAdjustment();

        public abstract bool UsesOldLoadNilEncoding();

        public abstract bool UsesInlineUpvalueDeclarations();

        public abstract Opcode GetTForTarget();

        public abstract Opcode GetForTarget();

        public abstract bool IsBreakableLoopEnd(Opcode in_op);
    }
}
