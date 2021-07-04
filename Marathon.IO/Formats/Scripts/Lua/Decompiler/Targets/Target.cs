using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets
{
    public abstract class Target
    {
        public abstract void print(Output @out);

        public abstract void printMethod(Output @out);

        public bool isDeclaration(Declaration decl) => false;

        public bool isLocal() => false;

        public int getIndex() => throw new Exception();

        public bool isFunctionName() => true;
    }
}
