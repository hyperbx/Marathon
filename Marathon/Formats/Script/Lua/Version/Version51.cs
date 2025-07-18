using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Version
{
    public class Version51 : VersionBase
    {
        public Version51() : base(LuaVersion.Lua51) { }

        public override bool HasHeaderTail()
        {
            return false;
        }

        public override bool HasFormat()
        {
            return true;
        }

        public override LFunctionType GetLFunctionType()
        {
            return new();
        }

        public override int GetOuterBlockScopeAdjustment()
        {
            return -1;
        }

        public override bool UsesOldLoadNilEncoding()
        {
            return true;
        }

        public override bool UsesInlineUpvalueDeclarations()
        {
            return true;
        }

        public override Opcode GetTForTarget()
        {
            return Opcode.TFORLOOP;
        }

        public override Opcode GetForTarget()
        {
            return Opcode.NULL;
        }

        public override bool IsBreakableLoopEnd(Opcode in_op)
        {
            return in_op == Opcode.JMP || in_op == Opcode.FORLOOP;
        }
    }
}
