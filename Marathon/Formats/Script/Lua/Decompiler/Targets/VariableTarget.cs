using System;

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class VariableTarget : Target
    {
        public readonly Declaration Declaration;

        public VariableTarget(Declaration decl) => Declaration = decl;

        public override void Write(Output @out)
            => @out.Write(Declaration.Name);

        public override void WriteMethod(Output @out)
            => throw new Exception();

        public override bool IsDeclaration(Declaration decl) => Declaration == decl;

        public override bool IsLocal() => true;

        public override int GetIndex() => Declaration.Register;

        public override bool Equals(object obj)
        {
            if (obj is VariableTarget t)
            {
                return Declaration == t.Declaration;
            }
            else
            {
                return false;
            }
        }
    }
}
