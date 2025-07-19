using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Types;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public class CompareBlock(LFunction in_function, int in_begin, int in_end, int in_target, Branch in_branch) : Block(in_function, in_begin, in_end)
    {
        public int Target { get; set; } = in_target;

        public Branch Branch { get; set; } = in_branch;

        public override bool IsContainer()
        {
            return false;
        }

        public override bool Breakable()
        {
            return false;
        }

        public override void AddStatement(Statement in_statement) { }

        public override bool IsUnprotected()
        {
            return false;
        }

        public override int GetLoopback()
        {
            throw new NotSupportedException();
        }

        public override void Write(Output in_output)
        {
            in_output.Write("-- WARNING: unhandled compare assign!");
        }

        public override Operation Process(Decompiler in_decompiler)
        {
            return new CompareBlockOperation(this);
        }
    }

    file class CompareBlockOperation(CompareBlock in_outerInstance) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return new RegisterSet(in_outerInstance.End - 1, in_outerInstance.Target, in_outerInstance.Branch.AsExpression(in_registers)).Process(in_registers, in_block);
        }
    }
}
