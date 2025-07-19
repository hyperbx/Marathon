using System;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class VariableTarget(Declaration in_declaration) : Target
    {
        public Declaration Declaration => in_declaration;

        public override bool IsLocal()
        {
            return true;
        }

        public override void Write(Output in_output)
        {
            in_output.Write(Declaration.Name);
        }

        public override void WriteMethod(Output in_output)
        {
            throw new NotSupportedException();
        }

        public override bool IsDeclaration(Declaration in_declaration)
        {
            return Declaration == in_declaration;
        }

        public override int GetIndex()
        {
            return Declaration.Register;
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is VariableTarget out_target)
            {
                return Declaration == out_target.Declaration;
            }
            else
            {
                return false;
            }
        }
    }
}
