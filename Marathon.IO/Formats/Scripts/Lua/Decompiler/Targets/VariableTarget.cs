using System;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets
{
    public class VariableTarget : Target
    {
        public readonly Declaration decl;

        public VariableTarget(Declaration decl) => this.decl = decl;

        public override void print(Output @out) => @out.print(decl.name);

        public override void printMethod(Output @out) => throw new Exception();

        public bool isDeclaration(Declaration decl) => this.decl == decl;

        public bool isLocal() => true;

        public int getIndex() => decl.register;

        public bool equals(object obj)
        {
            if (obj is VariableTarget)
            {
                VariableTarget t = (VariableTarget)obj;

                return decl == t.decl;
            }
            else
                return false;
        }
    }
}
