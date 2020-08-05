using Marathon.IO.Formats.Scripts.Lua.Helpers;

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

        public abstract Op getTForTarget();

        public abstract Op getForTarget();

        public abstract bool isBreakableLoopEnd(Op op);
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

        public override Op getTForTarget() => Op.TFORLOOP;

        public override Op getForTarget() => Op.FORLOOP;

        public override bool isBreakableLoopEnd(Op op) => op == Op.JMP || op == Op.FORLOOP;
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

        public override Op getTForTarget() => Op.TFORLOOP;

        public override Op getForTarget() => null;

        public override bool isBreakableLoopEnd(Op op) => op == Op.JMP || op == Op.FORLOOP;
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

        public override Op getTForTarget() => Op.TFORCALL;

        public override Op getForTarget() => null;

        public override bool isBreakableLoopEnd(Op op) => op == Op.JMP || op == Op.FORLOOP || op == Op.TFORLOOP;
    }
}
