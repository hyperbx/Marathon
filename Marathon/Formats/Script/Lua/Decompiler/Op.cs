namespace Marathon.Formats.Script.Lua.Decompiler
{
	public sealed class Op
	{
		public enum Opcode
		{
			MOVE,
			LOADK,
			LOADBOOL,
			LOADNIL,
			GETUPVAL,
			GETGLOBAL,
			GETTABLE,
			SETGLOBAL,
			SETUPVAL,
			SETTABLE,
			NEWTABLE,
			SELF,
			ADD,
			SUB,
			MUL,
			DIV,
			MOD,
			POW,
			UNM,
			NOT,
			LEN,
			CONCAT,
			JMP,
			EQ,
			LT,
			LE,
			TEST,
			TESTSET,
			CALL,
			TAILCALL,
			RETURN,
			FORLOOP,
			FORPREP,
			TFORLOOP,
			SETLIST,
			CLOSE,
			CLOSURE,
			VARARG,
			LOADKX,
			GETTABUP,
			SETTABUP,
			TFORCALL,
			EXTRAARG,
			SETLIST50,
			SETLISTO,
			TFORPREP,
			TEST50,
			NULL
		}

		private readonly OpcodeFormat _format;

		private Op(OpcodeFormat format) => _format = format;

        public string CodePointToString(int codepoint, ICodeExtract ex)
        {
            switch (_format)
            {
                case OpcodeFormat.A:
                    return $"{ToString()} {ex.ExtractA(codepoint)}";

                case OpcodeFormat.A_B:
                    return $"{ToString()} {ex.ExtractA(codepoint)} {ex.ExtractB(codepoint)}";

                case OpcodeFormat.A_C:
                    return $"{ToString()} {ex.ExtractA(codepoint)} {ex.ExtractC(codepoint)}";

                case OpcodeFormat.A_B_C:
                    return $"{ToString()} {ex.ExtractA(codepoint)} {ex.ExtractB(codepoint)} {ex.ExtractC(codepoint)}";

                case OpcodeFormat.A_Bx:
                    return $"{ToString()} {ex.ExtractA(codepoint)} {ex.ExtractBx(codepoint)}";

                case OpcodeFormat.A_sBx:
                    return $"{ToString()} {ex.ExtractA(codepoint)} {ex.ExtractsBx(codepoint)}";

                case OpcodeFormat.Ax:
                    return $"{ToString()} <Ax>";

                case OpcodeFormat.sBx:
                    return $"{ToString()} {ex.ExtractsBx(codepoint)}";

                default:
                    return ToString();
            }
        }
    }

}
