using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public abstract class Target
    {
        public abstract void Write(Output @out);

        public abstract void WriteMethod(Output @out);

        public virtual bool IsDeclaration(Declaration decl) => false;

        public virtual bool IsLocal() => false;

        public virtual int GetIndex() => throw new Exception();

        public virtual bool IsFunctionName() => true;
    }
}
