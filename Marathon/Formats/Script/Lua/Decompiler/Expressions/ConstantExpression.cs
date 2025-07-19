// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler.Expressions
{
    public class ConstantExpression(Constant in_constant, int in_index) : Expression(Precedence.Atomic)
    {
        public override int GetConstantIndex()
        {
            return in_index;
        }

        public override bool IsConstant()
        {
            return true;
        }

        public override bool IsNil()
        {
            return in_constant.IsNil();
        }

        public override bool IsBoolean()
        {
            return in_constant.IsBoolean();
        }

        public override bool IsInteger()
        {
            return in_constant.IsInteger();
        }

        public override int AsInteger()
        {
            return in_constant.AsInteger();
        }

        public override bool IsString()
        {
            return in_constant.IsString();
        }

        public override bool IsIdentifier()
        {
            return in_constant.IsIdentifier();
        }

        public override string AsName()
        {
            return in_constant.AsName();
        }

        public override bool IsBrief()
        {
            return !in_constant.IsString() || in_constant.AsName().Length <= 10;
        }

        public override void Write(Output in_output)
        {
            in_constant.Write(in_output);
        }
    }
}
