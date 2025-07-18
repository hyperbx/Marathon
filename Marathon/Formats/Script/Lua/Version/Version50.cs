using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Version
{
    public class Version50 : VersionBase
    {
        public Version50() : base(LuaVersion.Lua50) { }

        public override bool HasHeaderTail()
        {
            return false;
        }

        public override bool HasFormat()
        {
            return false;
        }

        public override LFunctionType GetLFunctionType()
        {
            return new LFunctionType50();
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
            return Opcode.FORLOOP;
        }

        public override bool IsBreakableLoopEnd(Opcode in_op)
        {
            return in_op == Opcode.JMP || in_op == Opcode.FORLOOP;
        }
    }
}
