namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class OpcodeMap
    {
        private static Opcode[] _map =
        [
            Opcode.MOVE,
            Opcode.LOADK,
            Opcode.LOADBOOL,
            Opcode.LOADNIL,
            Opcode.GETUPVAL,
            Opcode.GETGLOBAL,
            Opcode.GETTABLE,
            Opcode.SETGLOBAL,
            Opcode.SETUPVAL,
            Opcode.SETTABLE,
            Opcode.NEWTABLE,
            Opcode.SELF,
            Opcode.ADD,
            Opcode.SUB,
            Opcode.MUL,
            Opcode.DIV,
            Opcode.POW,
            Opcode.UNM,
            Opcode.NOT,
            Opcode.CONCAT,
            Opcode.JMP,
            Opcode.EQ,
            Opcode.LT,
            Opcode.LE,
            Opcode.TEST50,
            Opcode.CALL,
            Opcode.TAILCALL,
            Opcode.RETURN,
            Opcode.FORLOOP,
            Opcode.TFORLOOP,
            Opcode.TFORPREP,
            Opcode.SETLIST50,
            Opcode.SETLISTO,
            Opcode.CLOSE,
            Opcode.CLOSURE
        ];

        public static Opcode Get(int in_opcode)
        {
            if (in_opcode >= 0 && in_opcode < _map.Length)
            {
                return _map[in_opcode];
            }
            else
            {
                return Opcode.NULL;
            }
        }
    }
}
