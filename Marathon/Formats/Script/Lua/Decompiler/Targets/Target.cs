using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public abstract class Target
    {
        public virtual bool IsLocal()
        {
            return false;
        }

        public virtual bool IsDeclaration(Declaration in_declaration)
        {
            return false;
        }

        public virtual bool IsFunctionName()
        {
            return true;
        }

        public virtual int GetIndex()
        {
            throw new NotSupportedException();
        }

        public virtual string GetOutput()
        {
            throw new NotImplementedException();
        }

        public abstract void Write(Output in_output);

        public abstract void WriteMethod(Output in_output);

        public override string ToString()
        {
            return GetOutput();
        }
    }
}
