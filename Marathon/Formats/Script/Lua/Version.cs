using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler;

namespace Marathon.Formats.Script.Lua
{
    public abstract class Version
    {
        public static readonly Version LUA50 = new Version50();
        public static readonly Version LUA51 = new Version51();
        public static readonly Version LUA52 = new Version52();

        protected readonly int _versionNumber;

        protected Version(int versionNumber) => _versionNumber = versionNumber;

        public abstract bool HasHeaderTail();

        public abstract bool HasFormat();

        public abstract LFunctionType GetLFunctionType();

        public OpcodeMap GetOpcodeMap() => new(_versionNumber);

        public abstract int GetOuterBlockScopeAdjustment();

        public abstract bool UsesOldLoadNilEncoding();

        public abstract bool UsesInlineUpvalueDeclarations();

        public abstract Op.Opcode GetTForTarget();

        public abstract Op.Opcode GetForTarget();

        public abstract bool IsBreakableLoopEnd(Op.Opcode op);
    }

    public class Version50 : Version
    {
        public Version50() : base(0x50) { }

        public override bool HasHeaderTail() => false;

        public override bool HasFormat() => false;

        public override LFunctionType GetLFunctionType() => LFunctionType.TYPE50;

        public override int GetOuterBlockScopeAdjustment() => -1;

        public override bool UsesOldLoadNilEncoding() => true;

        public override bool UsesInlineUpvalueDeclarations() => true;

        public override Op.Opcode GetTForTarget() => Op.Opcode.TFORLOOP;

        public override Op.Opcode GetForTarget() => Op.Opcode.FORLOOP;

        public override bool IsBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP;
    }

    public class Version51 : Version
    {
        public Version51() : base(0x51) { }

        public override bool HasHeaderTail() => false;

        public override bool HasFormat() => true;

        public override LFunctionType GetLFunctionType() => LFunctionType.TYPE51;

        public override int GetOuterBlockScopeAdjustment() => -1;

        public override bool UsesOldLoadNilEncoding() => true;

        public override bool UsesInlineUpvalueDeclarations() => true;

        public override Op.Opcode GetTForTarget() => Op.Opcode.TFORLOOP;

        public override Op.Opcode GetForTarget() => Op.Opcode.NULL;

        public override bool IsBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP;
    }

    public class Version52 : Version
    {
        public Version52() : base(0x52) { }

        public override bool HasHeaderTail() => true;

        public override bool HasFormat() => true;

        public override LFunctionType GetLFunctionType() => LFunctionType.TYPE52;

        public override int GetOuterBlockScopeAdjustment() => 0;

        public override bool UsesOldLoadNilEncoding() => false;

        public override bool UsesInlineUpvalueDeclarations() => false;

        public override Op.Opcode GetTForTarget() => Op.Opcode.TFORCALL;

        public override Op.Opcode GetForTarget() => Op.Opcode.NULL;

        public override bool IsBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP || op == Op.Opcode.TFORLOOP;
    }
}
