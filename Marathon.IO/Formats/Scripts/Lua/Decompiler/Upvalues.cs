using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Upvalues
    {
        private readonly LUpvalue[] upvalues;

        public Upvalues(LUpvalue[] upvalues) => this.upvalues = upvalues;

        public string getName(int index)
        {
            if (index < upvalues.Length && upvalues[index].name != null)
                return upvalues[index].name;

            else
                // TODO: Set error.
                return $"_UPVALUE_{index}_";
        }

        public UpvalueExpression getExpression(int index) => new UpvalueExpression(getName(index));
    }
}
