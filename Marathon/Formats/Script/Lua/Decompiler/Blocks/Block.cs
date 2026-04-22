using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public abstract class Block(LFunction in_function, int in_begin, int in_end) : Statement, IComparable<Block>
    {
        protected readonly LFunction _function = in_function;

        public int Begin { get; set; } = in_begin;

        public int End { get; set; } = in_end;

        public bool LoopRedirectAdjustment { get; set; } = false;

        public abstract void AddStatement(Statement statement);

        public bool Contains(Block in_block)
        {
            return Begin <= in_block.Begin && End >= in_block.End;
        }

        public bool Contains(int in_line)
        {
            return Begin <= in_line && in_line < End;
        }

        public virtual int ScopeEnd()
        {
            return End - 1;
        }

        /// <summary>
        /// An unprotected block is one that ends in a JMP instruction.
        /// <para>If this is the case, any inner statement that tries to jump to the end of this block will be redirected.</para>
        /// <para>One of the Lua compiler's few optimisations is that it changes any JMP that targets another JMP to the absolute target.</para>
        /// </summary>
        public abstract bool IsUnprotected();

        public abstract int GetLoopback();

        public abstract bool Breakable();

        public abstract bool IsContainer();

        public virtual int CompareTo(Block in_block)
        {
            if (Begin < in_block.Begin)
                return -1;

            if (Begin == in_block.Begin)
            {
                if (End < in_block.End)
                {
                    return 1;
                }
                else if (End == in_block.End)
                {
                    if (IsContainer() && !in_block.IsContainer())
                    {
                        return -1;
                    }
                    else if (!IsContainer() && in_block.IsContainer())
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return 1;
        }

        public virtual Operation Process(Decompiler in_decompiler)
        {
            return new BlockOperation(this, this);
        }
    }

    file class BlockOperation(Block in_outerInstance, Statement in_statement) : Operation(in_outerInstance.End - 1)
    {
        public override Statement Process(Registers in_registers, Block in_block)
        {
            return in_statement;
        }
    }
}
