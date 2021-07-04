using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler;

namespace Marathon.IO.Formats.Scripts.Lua
{
    public abstract class Version
    {
        public static readonly Version LUA50 = new Version50();
        public static readonly Version LUA51 = new Version51();
        public static readonly Version LUA52 = new Version52();

        protected readonly int versionNumber;

        protected Version(int versionNumber) => this.versionNumber = versionNumber;

        public abstract bool hasHeaderTail();

        public abstract bool hasFormat();

        public abstract LFunctionType getLFunctionType();

        public OpcodeMap getOpcodeMap() => new OpcodeMap(versionNumber);

        public abstract int getOuterBlockScopeAdjustment();

        public abstract bool usesOldLoadNilEncoding();

        public abstract bool usesInlineUpvalueDeclarations();

        public abstract Op.Opcode getTForTarget();

        public abstract Op.Opcode getForTarget();

        public abstract bool isBreakableLoopEnd(Op.Opcode op);
    }

    public class Version50 : Version
    {
        public Version50() : base(0x50) { }

        public override bool hasHeaderTail() => false;

        public override bool hasFormat() => false;

        public override LFunctionType getLFunctionType() => LFunctionType.TYPE50;

        public override int getOuterBlockScopeAdjustment() => -1;

        public override bool usesOldLoadNilEncoding() => true;

        public override bool usesInlineUpvalueDeclarations() => true;

        public override Op.Opcode getTForTarget() => Op.Opcode.TFORLOOP;

        public override Op.Opcode getForTarget() => Op.Opcode.FORLOOP;

        public override bool isBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP;
    }

    public class Version51 : Version
    {
        public Version51() : base(0x51) { }

        public override bool hasHeaderTail() => false;

        public override bool hasFormat() => true;

        public override LFunctionType getLFunctionType() => LFunctionType.TYPE51;

        public override int getOuterBlockScopeAdjustment() => -1;

        public override bool usesOldLoadNilEncoding() => true;

        public override bool usesInlineUpvalueDeclarations() => true;

        public override Op.Opcode getTForTarget() => Op.Opcode.TFORLOOP;

        public override Op.Opcode getForTarget() => Op.Opcode.NULL;

        public override bool isBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP;
    }

    public class Version52 : Version
    {
        public Version52() : base(0x52) { }

        public override bool hasHeaderTail() => true;

        public override bool hasFormat() => true;

        public override LFunctionType getLFunctionType() => LFunctionType.TYPE52;

        public override int getOuterBlockScopeAdjustment() => 0;

        public override bool usesOldLoadNilEncoding() => false;

        public override bool usesInlineUpvalueDeclarations() => false;

        public override Op.Opcode getTForTarget() => Op.Opcode.TFORCALL;

        public override Op.Opcode getForTarget() => Op.Opcode.NULL;

        public override bool isBreakableLoopEnd(Op.Opcode op) => op == Op.Opcode.JMP || op == Op.Opcode.FORLOOP || op == Op.Opcode.TFORLOOP;
    }
}
