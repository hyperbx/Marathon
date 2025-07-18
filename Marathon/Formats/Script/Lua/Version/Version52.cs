using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Script.Lua.Types;

namespace Marathon.Formats.Script.Lua.Version
{
    public class Version52 : VersionBase
    {
        public Version52() : base(LuaVersion.Lua52) { }

        public override bool HasHeaderTail()
        {
            return true;
        }

        public override bool HasFormat()
        {
            return true;
        }

        public override LFunctionType GetLFunctionType()
        {
            return new LFunctionType52();
        }

        public override int GetOuterBlockScopeAdjustment()
        {
            return 0;
        }

        public override bool UsesOldLoadNilEncoding()
        {
            return false;
        }

        public override bool UsesInlineUpvalueDeclarations()
        {
            return false;
        }

        public override Opcode GetTForTarget()
        {
            return Opcode.TFORCALL;
        }

        public override Opcode GetForTarget()
        {
            return Opcode.NULL;
        }

        public override bool IsBreakableLoopEnd(Opcode in_op)
        {
            return in_op == Opcode.JMP || in_op == Opcode.FORLOOP || in_op == Opcode.TFORLOOP;
        }
    }
}
