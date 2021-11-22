using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;

namespace Marathon.Formats.Script.Lua.Decompiler.Blocks
{
    public abstract class Block : Statement, IComparable<Block>
    {
        protected readonly LFunction _function;

        public int Begin, End;
        public bool LoopRedirectAdjustment = false;

        public Block(LFunction function, int begin, int end)
        {
            _function = function;
            Begin = begin;
            End = end;
        }

        public abstract void AddStatement(Statement statement);

        public bool Contains(Block block) => Begin <= block.Begin && End >= block.End;

        public bool Contains(int line) => Begin <= line && line < End;

        public virtual int ScopeEnd() => End - 1;

        /// <summary>
        /// An unprotected block is one that ends in a JMP instruction.
        /// <para>If this is the case, any inner statement that tries to jump to the end of this block will be redirected.</para>
        /// <para>One of the Lua compiler's few optimizations is that is changes any JMP that targets another JMP to the ultimate target.</para>
        /// <para>This is what I call redirection.</para>
        /// </summary>
        public abstract bool IsUnprotected();

        public abstract int GetLoopback();

        public abstract bool Breakable();

        public abstract bool IsContainer();

        public int CompareTo(Block block)
        {
            if (Begin < block.Begin)
            {
                return -1;
            }
            else if (Begin == block.Begin)
            {
                if (End < block.End)
                {
                    return 1;
                }
                else if (End == block.End)
                {
                    if (IsContainer() && !block.IsContainer())
                    {
                        return -1;
                    }
                    else if (!IsContainer() && block.IsContainer())
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
            else
            {
                return 1;
            }
        }

        public virtual Operation Process(Decompiler d)
        {
            Statement statement = this;

            return new OperationAnonymousInnerClass(this, statement);
        }

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly Block _outerInstance;
            private Statement _statement;

            public OperationAnonymousInnerClass(Block outerInstance, Statement statement) : base(outerInstance.End - 1)
            {
                _outerInstance = outerInstance;
                _statement = statement;
            }

            public override Statement Process(Registers r, Block block) => _statement;
        }
    }
}
