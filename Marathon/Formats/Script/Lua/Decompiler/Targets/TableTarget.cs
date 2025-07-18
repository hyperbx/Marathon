using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler.Targets
{
    public class TableTarget(Expression in_table, Expression in_index) : Target
    {
        public override void Write(Output in_output)
        {
            new TableReference(in_table, in_index).Write(in_output);
        }

        public override void WriteMethod(Output in_output)
        {
            in_table.Write(in_output);

            in_output.Write(":");
            in_output.Write(in_index.AsName());
        }

        public override bool IsFunctionName()
        {
            if (!in_index.IsIdentifier() || !in_table.IsDotChain())
                return false;

            return true;
        }
    }
}
