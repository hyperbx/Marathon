using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks
{
    public abstract class Block : Statement
    {
        protected readonly LFunction function;
        public int begin, end;
        public bool loopRedirectAdjustment = false;

        public Block(LFunction function, int begin, int end)
        {
            this.function = function;
            this.begin = begin;
            this.end = end;
        }

        public abstract void addStatement(Statement statement);

        public bool contains(Block block) => begin <= block.begin && end >= block.end;

        public bool contains(int line) => begin <= line && line < end;

        public int scopeEnd() => end - 1;

        /// <summary>
        /// An unprotected block is one that ends in a JMP instruction.
        /// If this is the case, any inner statement that tries to jump to the end of this block will be redirected.
        /// One of the Lua compiler's few optimizations is that is changes any JMP that targets another JMP to the ultimate target.
        /// This is what I call redirection.
        /// </summary>
        public abstract bool isUnprotected();

        public abstract int getLoopback();

        public abstract bool breakable();

        public abstract bool isContainer();

        public int compareTo(Block block)
        {
            if (begin < block.begin)
                return -1;

            else if (begin == block.begin)
            {
                if (end < block.end)
                    return -1;

                else if (end == block.end)
                {
                    if (isContainer() && block.isContainer())
                        return 1;

                    else
                        return 0;
                }
                else
                    return -1;
            }
            else
                return 1;
        }

        public virtual Operation process(Decompiler d)
        {
            Statement statement = this;

            return new OperationAnonymousInnerClass(this, statement);
        }

        private class OperationAnonymousInnerClass : Operation
        {
            private readonly Block outerInstance;
            private Statement statement;

            public OperationAnonymousInnerClass(Block outerInstance, Statement statement) : base(outerInstance.end - 1)
            {
                this.outerInstance = outerInstance;
                this.statement = statement;
            }

            public override Statement process(Registers r, Block block) => statement;
        }
    }
}
